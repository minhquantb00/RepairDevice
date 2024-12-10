using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepairManagement.Application.Handle.Email;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Auth;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.UltilitiesGlobal;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BcryptNet = BCrypt.Net.BCrypt;

namespace RepairManagement.Application.Service.Implement
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly NguoiDungConverter _nguoiDungConverter;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        private readonly IRepository<NguoiDung> _nguoiDungRepository;
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        private readonly IRepository<ConfirmEmail> _confirmEmailRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        public AuthService(IConfiguration configuration, NguoiDungConverter nguoiDungConverter, IRepository<RefreshToken> refreshTokenRepository, IRepository<NguoiDung> nguoiDungRepository, IRepository<KhachHang> khachHangRepository, IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository, IRepository<ConfirmEmail> confirmEmailRepository, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _configuration = configuration;
            _nguoiDungConverter = nguoiDungConverter;
            _refreshTokenRepository = refreshTokenRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _khachHangRepository = khachHangRepository;
            _confirmEmailRepository = confirmEmailRepository;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }
        public async Task AddRoleToUser(int userId, List<string> roles)
        {
            try
            {
                var user = await _nguoiDungRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user), "User không tồn tại");
                }
                await _nguoiDungRepository.AddUserToRoleAsync(user, roles);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task ChangePassword(Request_ChangePassword request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException(nameof(currentUser));
            }
            string userId = currentUser.FindFirst("Id").Value;
            var user = await _nguoiDungRepository.GetByIdAsync(int.Parse(userId));
            try
            {
                var checkOldPass = Utilities.IsPasswordValid(request.MatKhauCu, user.MatKhau);
                if(!checkOldPass)
                {
                    throw new InvalidOperationException("Mật khẩu không chính xác");
                }
                if (!request.MatKhauMoi.Equals(request.XacNhanMatKhauMoi))
                {
                    throw new InvalidOperationException("Mật khẩu không trùng khớp");
                }
                user.MatKhau = Utilities.HashPassword(request.MatKhauMoi);
                user.UpdateTime = DateTime.Now;
                user = await _nguoiDungRepository.UpdateAsync(user);
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ConfirmCreateNewPassword(Request_ConfirmCreateNewPassword request)
        {
            var confirmEmail = await _confirmEmailRepository.GetAsync(item => item.ConfirmCode.Equals(request.MaXacNhan));
            if (confirmEmail == null)
            {
                throw new InvalidOperationException("Mã xác nhận không hợp lệ");
            }
            var user = await _nguoiDungRepository.GetAsync(item => item.Id == confirmEmail.UserId);
            if (user == null)
            {
                throw new ArgumentNullException("Người dùng không tồn tại"); 
            }
            if (!request.MatKhauMoi.Equals(request.XacNhanMatKhauMoi))
            {
                throw new InvalidOperationException("Mật khẩu không trùng khớp");
            }
            try
            {
                user.MatKhau = Utilities.HashPassword(request.MatKhauMoi);
                user.UpdateTime = DateTime.Now;
                user = await _nguoiDungRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ForgotPassword(Request_ForgotPassword request)
        {
            var user = await _nguoiDungRepository.GetAsync(item => item.Email.Equals(request.Email));
            if (user == null)
            {
                throw new ArgumentNullException("Không tìm thấy thông tin người dùng");
            }
            try
            {
                ConfirmEmail confirmEmail = new ConfirmEmail
                {
                    ConfirmCode = "RepairDevice_" + DateTime.Now.Ticks.ToString(),
                    CreateTime = DateTime.Now,
                    ExpiredTime = DateTime.Now.AddMinutes(15),
                    IsConfirm = false,
                    UserId = user.Id,
                };
                confirmEmail = await _confirmEmailRepository.CreateAsync(confirmEmail);
                var message = new Request_Message(new string[] { user.Email }, "Nhận mã xác nhận tại đây: ", $"Mã xác nhận là: {confirmEmail.ConfirmCode}");
                _emailService.SendEmail(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(NguoiDung user)
        {
            var permissions = _userRoleRepository.GetAllAsync(x => x.NguoiDungId == user.Id).Result;
            if (!permissions.Any())
            {
                return null;
            }
            var roles = _roleRepository.GetAllAsync().Result;
            if (!roles.Any())
            {
                return null;
            }

            var authClaims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email", user.Email),
            };

            var permissionClaims = from permission in permissions
                                   join role in roles on permission.RoleId equals role.Id
                                   select new Claim("Permission", role.Name);

            foreach (var claim in permissionClaims)
            {
                authClaims.Add(claim);
            }

            var userRoles = await _nguoiDungRepository.GetRolesOfUserAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwtToken = GetToken(authClaims);
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JWT:RefreshTokenValidity"], out int refreshTokenValidity);

            RefreshToken rf = new RefreshToken
            {
                UserId = user.Id,
                ExpiredTime = DateTime.UtcNow.AddHours(refreshTokenValidity),
                CreateTime = DateTime.Now,
                Token = refreshToken
            };

            rf = await _refreshTokenRepository.CreateAsync(rf).ConfigureAwait(false);

            return new ResponseObject<DataResponseLogin>
            {
                Status = StatusCodes.Status200OK,
                Message = "Token created successfully",
                Data = new DataResponseLogin
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    RefreshToken = refreshToken,
                }
            };
        }

        public async Task<ResponseObject<DataResponseLogin>> Login(Request_Login request)
        {
            try
            {
                var user = await _nguoiDungRepository.GetAsync(item => item.Email.Equals(request.Email));

                if (user == null)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Email không chính xác",
                        Data = null
                    };
                }
                var checkPassword = BcryptNet.Verify(request.MatKhau, user.MatKhau);
                if (!checkPassword)
                {
                    return new ResponseObject<DataResponseLogin>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = "Mật khẩu không chính xác",
                        Data = null
                    };
                }

                var tokenResponse = await GetJwtTokenAsync(user);
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Đăng nhập thành công",
                    Data = new DataResponseLogin
                    {
                        AccessToken = tokenResponse.Data.AccessToken,
                        RefreshToken = tokenResponse.Data.RefreshToken,
                    }
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseLogin>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseObject<DataResponseUser>> Register(Request_register request)
        {
            try
            {
                var checkEmail = await _nguoiDungRepository.GetAsync(item => item.Email.Equals(request.Email));
                if (checkEmail != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Email đã tồn tại trên hệ thống! Vui lòng thử lại"
                    };
                }
                var checkPhoneNumber = await _nguoiDungRepository.GetAsync(item => item.SoDienThoai.Equals(request.SoDienThoai));
                if (checkPhoneNumber != null)
                {
                    return new ResponseObject<DataResponseUser>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Số điện thoại đã tồn tại trên hệ thống! Vui lòng thử lại"
                    };
                }

                NguoiDung user = new NguoiDung
                {
                    Email = request.Email,
                    MatKhau = Utilities.HashPassword(request.MatKhau),
                    SoDienThoai = request.SoDienThoai,
                    CreateTime = DateTime.Now,
                    HoVaTen = request.HoVaTen,
                    IsActive = true,
                };
                user = await _nguoiDungRepository.CreateAsync(user);
                if (user != null)
                {
                    await AddRoleToUser(user.Id, new List<string> { "Customer" });

                    KhachHang khachHang = new KhachHang
                    {
                        IsActive = true,
                        CreateTime = DateTime.Now,
                        DiaChi = user.DiaChi,
                        Diem = 0,
                        Email = user.Email,
                        HoVaTen = user.HoVaTen,
                        NguoiDungId = user.Id,
                        SoDienThoai = user.SoDienThoai,
                    };

                    khachHang = await _khachHangRepository.CreateAsync(khachHang);
                    return new ResponseObject<DataResponseUser>
                    {
                        Data = _nguoiDungConverter.EntityToDTO(user),
                        Message = "Đăng ký tài khoản thành công",
                        Status = StatusCodes.Status200OK
                    };
                }
                return new ResponseObject<DataResponseUser>
                {
                    Data = null,
                    Message = "Đăng ký tài khoản thất bại",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            catch (Exception ex)
            {
                return new ResponseObject<DataResponseUser>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                };
            }
        }

        public Task<ResponseObject<DataResponseLogin>> RenewAccessTokenAsync(Request_RenewAccessToken token)
        {
            throw new NotImplementedException();
        }


        #region PrivateMethods
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            _ = int.TryParse(_configuration["JWT:TokenValidityInHours"], out int tokenValidityInMinutes);
            var expirationTimeUtc = DateTime.UtcNow.AddHours(tokenValidityInMinutes);
            var localTimeZone = TimeZoneInfo.Local;
            var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, localTimeZone);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: expirationTimeInLocalTimeZone,
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new Byte[64];
            var range = RandomNumberGenerator.Create();
            range.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GenerateUniqueAccount(string fullName, List<string> existingAccounts)
        {
            string account = GenerateAccount(fullName);

            int counter = 1;
            string uniqueAccount = account;
            while (existingAccounts.Contains(uniqueAccount))
            {
                uniqueAccount = account + counter.ToString();
                counter++;
            }

            existingAccounts.Add(uniqueAccount);

            return uniqueAccount;
        }

        private string GenerateAccount(string fullName)
        {
            string[] nameParts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string lastName = nameParts.Last();

            string initials = string.Concat(nameParts.Take(nameParts.Length - 1).Select(part => part[0].ToString().ToUpper()));

            return lastName + initials;
        }

        private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);

            return principal;

        }



        #endregion
    }
}
