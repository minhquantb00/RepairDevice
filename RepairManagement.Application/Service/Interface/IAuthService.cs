using RepairManagement.Application.Payloads.Requests.Auth;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Interface
{
    public interface IAuthService
    {
        Task<ResponseObject<DataResponseUser>> Register(Request_register request);
        Task AddRoleToUser(int userId, List<string> roles);
        Task<ResponseObject<DataResponseLogin>> GetJwtTokenAsync(NguoiDung user);
        Task<ResponseObject<DataResponseLogin>> RenewAccessTokenAsync(Request_RenewAccessToken token);
        Task<ResponseObject<DataResponseLogin>> Login(Request_Login request);
        Task ChangePassword(Request_ChangePassword request);
        Task ForgotPassword(Request_ForgotPassword request);
        Task ConfirmCreateNewPassword(Request_ConfirmCreateNewPassword request);
    }
}
