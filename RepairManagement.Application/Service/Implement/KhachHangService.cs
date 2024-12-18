using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using RepairManagement.Application.Handle.Media;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Customer;
using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Extensions;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Implement
{
    public class KhachHangService : IKhachHangService
    {
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly KhachHangConverter _khachHangConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<ThietBi> _thietBiRepository;
        private readonly IRepository<ThietBiSuaChua> _thietBiSuaChuaRepository;
        private readonly IRepository<LichSuTichDiem> _lichSuTichDiemRepository;
        private readonly LichSuTichDiemConverter _lichSuaChuaConverter;
        public KhachHangService(IRepository<KhachHang> khachHangRepository, KhachHangConverter khachHangConverter, IHttpContextAccessor httpContextAccessor, IRepository<ThietBi> thietBiRepository, IRepository<ThietBiSuaChua> thietBiSuaChuaRepository, IRepository<LichSuTichDiem> lichSuTichDiemRepository, LichSuTichDiemConverter lichSuTichDiemConverter)
        {
            _httpContextAccessor = httpContextAccessor;
            _khachHangConverter = khachHangConverter;
            _khachHangRepository  = khachHangRepository;
            _thietBiRepository = thietBiRepository;
            _thietBiSuaChuaRepository = thietBiSuaChuaRepository;
            _lichSuTichDiemRepository = lichSuTichDiemRepository;
            _lichSuaChuaConverter = lichSuTichDiemConverter;
        }
        public async Task<ResponseObject<DataResponseKhachHang>> CreateKhachHang(Request_CreateKhachHang request)
        {
            var currentUser =  _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Tài khoản chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if(!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Employee"))
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }

            KhachHang khachHang = new KhachHang
            {
                IsActive = true,
                CreateTime = DateTime.Now,
                DiaChi = request.DiaChi,
                Diem = 0,
                Email = request.Email,
                HoVaTen = request.HoVaTen,
                SoDienThoai = request.SoDienThoai,
            };
            khachHang = await _khachHangRepository.CreateAsync(khachHang);
            return new ResponseObject<DataResponseKhachHang>
            {
                Data = _khachHangConverter.EntityToDTO(khachHang),
                Message = "Tạo thông tin khách hàng thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseLichSuTichDiem>> CreateLichSuTichDiem(Request_CreateLichSuTichDiem request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLichSuTichDiem>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseLichSuTichDiem>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var khachHang = await _khachHangRepository.GetByIdAsync(request.KhachHangId);
            if(khachHang == null)
            {
                return new ResponseObject<DataResponseLichSuTichDiem>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            LichSuTichDiem lichSuTichDiem = new LichSuTichDiem
            {
                Action = request.Action,
                CreateTime = DateTime.Now,
                KhachHangId = khachHang.Id,
                Point = 1,
            };
            lichSuTichDiem  = await _lichSuTichDiemRepository.CreateAsync(lichSuTichDiem);
            khachHang.Diem = _lichSuTichDiemRepository.GetAllAsync(item => item.KhachHangId == khachHang.Id).Result.Sum(x => x.Point);
            khachHang = await _khachHangRepository.UpdateAsync(khachHang);
            return new ResponseObject<DataResponseLichSuTichDiem>
            {
                Data = null,
                Message = "Tích điểm thành công",
                Status = StatusCodes.Status200OK
            };
        }


        public async Task<ResponseObject<DataResponseKhachHang>> DeleteKhachHang(int id)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Tài khoản chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Employee"))
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }

            var khachHang = await _khachHangRepository.GetByIdAsync(id);

            await _khachHangRepository.DeleteAsync(khachHang.Id);
            return new ResponseObject<DataResponseKhachHang>
            {
                Data = _khachHangConverter.EntityToDTO(khachHang),
                Message = "Xóa thông tin khách hàng thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseKhachHang>> GetAllKhachHang(FilterCustomer? filter)
        {
            var query = await _khachHangRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                query = query.AsNoTracking().Where(record => record.Email.Trim().ToLower().Equals(filter.Keyword.Trim().ToLower()) || record.HoVaTen.Contains(filter.Keyword) || record.SoDienThoai.Trim().Equals(filter.Keyword.Trim()));
            }

            var result = query.AsNoTracking().Select(item => _khachHangConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseKhachHang> GetKhachHangById(int id)
        {
            var query = await _khachHangRepository.GetByIdAsync(id);
            return _khachHangConverter.EntityToDTO(query);
        }

        public async Task<ResponseObject<DataResponseKhachHang>> UpdateKhachHang(Request_UpdateKhachHang request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Tài khoản chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Employee"))
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var khachHang = await _khachHangRepository.GetByIdAsync(request.Id);
            if(khachHang == null)
            {
                return new ResponseObject<DataResponseKhachHang>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            khachHang.UpdateTime = DateTime.Now;
            khachHang.Email = string.IsNullOrEmpty(request.Email) ? khachHang.Email : request.Email;
            khachHang.SoDienThoai = string.IsNullOrEmpty(request.SoDienThoai) ? khachHang.SoDienThoai : request.SoDienThoai;
            khachHang.HoVaTen = string.IsNullOrEmpty(request.HoVaTen) ? khachHang.HoVaTen : request.HoVaTen;
            khachHang.DiaChi = string.IsNullOrEmpty(request.DiaChi) ? khachHang.DiaChi : request.DiaChi;
            khachHang = await _khachHangRepository.UpdateAsync(khachHang);
            return new ResponseObject<DataResponseKhachHang>
            {
                Data = _khachHangConverter.EntityToDTO(khachHang),
                Message = "Cập nhật thông tin khách hàng thành công",
                Status = StatusCodes.Status200OK
            };
        }
    }
}
