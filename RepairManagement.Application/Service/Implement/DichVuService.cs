using Microsoft.AspNetCore.Http;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Service;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Service;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Service.Implement
{
    public class DichVuService : IDichVuService
    {
        private readonly IRepository<DichVu> _dichVuRepository;
        private readonly DichVuConverter _dichVuConverter;
        private readonly IHttpContextAccessor _contextAccessor;
        public DichVuService(IRepository<DichVu> dichVuRepository, DichVuConverter dichVuConverter, IHttpContextAccessor contextAccessor)
        {
            _dichVuRepository = dichVuRepository;
            _dichVuConverter = dichVuConverter;
            _contextAccessor = contextAccessor;
        }

        public async Task<ResponseObject<DataResponseService>> CreateService(Request_CreateService request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseService>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseService>
                {
                    Data= null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            DichVu dichVu = new DichVu
            {
                DiemDichVuTrungBinh = 0,
                MoTaDichVu = request.MoTaDichVu,
                SoLuotDanhGia = 0,
                TenDichVu = request.TenDichVu
            };
            dichVu = await _dichVuRepository.CreateAsync(dichVu);
            return new ResponseObject<DataResponseService>
            {
                Data = _dichVuConverter.EntityToDTO(dichVu),
                Message = "Tạo dịch vụ thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseService>> GetAllServices()
        {
            var query = await _dichVuRepository.GetAllAsync();
            var result = query.Select(item => _dichVuConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseService> GetServiceById(int id)
        {
            var query = await _dichVuRepository.GetByIdAsync(id);
            return _dichVuConverter.EntityToDTO(query);
        }
    }
}
