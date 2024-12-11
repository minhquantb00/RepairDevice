using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepairManagement.Application.Handle.Media;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.DeviceType;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
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
    public class LoaiThietBiService : ILoaiThietBiService
    {
        private readonly IRepository<LoaiThietBi> _loaiThietBiRepository;
        private readonly LoaiThietBiConverter _loaiThietBiConverter;
        private readonly IHttpContextAccessor _contextAccessor;
        public LoaiThietBiService(IRepository<LoaiThietBi> loaiThietBiRepository, LoaiThietBiConverter loaiThietBiConverter, IHttpContextAccessor contextAccessor)
        {
            _loaiThietBiRepository = loaiThietBiRepository;
            _loaiThietBiConverter = loaiThietBiConverter;
            _contextAccessor = contextAccessor;
        }

        public async Task<ResponseObject<DataResponseLoaiThietBi>> CreateLoaiThietBi(Request_CreateLoaiThietBi request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLoaiThietBi>
                {
                    Data = null,
                    Message = "Tài khoản chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Employee"))
            {
                return new ResponseObject<DataResponseLoaiThietBi>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            LoaiThietBi loaiThietBi = new LoaiThietBi
            {
                ImageUrl = await HandleUploadImage.Upfile(request.ImageUrl),
                Name = request.Name,
            };
            loaiThietBi = await _loaiThietBiRepository.CreateAsync(loaiThietBi);
            return new ResponseObject<DataResponseLoaiThietBi>
            {
                Data = _loaiThietBiConverter.EntityToDTO(loaiThietBi),
                Message = "Thêm loại thiết bị thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseLoaiThietBi>> GetAllLoaiThietBi()
        {
            var query = await _loaiThietBiRepository.GetAllAsync();
            return query.AsNoTracking().Select(item => _loaiThietBiConverter.EntityToDTO(item));
        }

        public async Task<DataResponseLoaiThietBi> GetLoaiThietBiById(int id)
        {
            var query = await _loaiThietBiRepository.GetByIdAsync(id);
            return _loaiThietBiConverter.EntityToDTO(query);
        }
    }
}
