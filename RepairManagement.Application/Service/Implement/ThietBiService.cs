using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepairManagement.Application.Handle.Media;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Device;
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
    public class ThietBiService : IThietBiService
    {
        private readonly IRepository<ThietBi> _thietBiRepository;
        private readonly ThietBiConverter _thietBiConverter;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<LoaiThietBi> _loaiThietBiRepository;
        public ThietBiService(IRepository<ThietBi> thietBiRepository, ThietBiConverter thietBiConverter, IHttpContextAccessor contextAccessor, IRepository<LoaiThietBi> loaiThietBiRepository)
        {
            _thietBiRepository = thietBiRepository;
            _thietBiConverter = thietBiConverter;
            _contextAccessor = contextAccessor;
            _loaiThietBiRepository = loaiThietBiRepository;
        }

        public async Task<ResponseObject<DataResponseThietBi>> CreateThietBi(Request_CreateThietBi request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Tài khoản chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if(!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Employee"))
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }

            var loaiThietBi = await _loaiThietBiRepository.GetByIdAsync(request.LoaiThietBiId);
            if(loaiThietBi == null)
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Loại thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            ThietBi thietBi = new ThietBi
            {
                ImageUrl = await HandleUploadImage.Upfile(request.ImageUrl),
                LoaiThietBiId = request.LoaiThietBiId,
                MoTa = request.MoTa,
                Status = request.Status,
                TenThietBi = request.TenThietBi,
                Gia = request.Gia,
            };

            thietBi = await _thietBiRepository.CreateAsync(thietBi);
            return new ResponseObject<DataResponseThietBi>
            {
                Data = _thietBiConverter.EntityToDTO(thietBi),
                Message = "Tạo thông tin thiết bị thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseThietBi>> GetAllThietBi(FilterThietBi filter)
        {
            var query = await _thietBiRepository.GetAllAsync();
            if (filter.LoaiThietBiId.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.LoaiThietBiId == filter.LoaiThietBiId);
            }
            if (!string.IsNullOrEmpty(filter.TenThietBi))
            {
                query = query.AsNoTracking().Where(record => record.TenThietBi.Contains(filter.TenThietBi));
            }

            var result = query.AsNoTracking().Select(item => _thietBiConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseThietBi> GetThietBiById(int id)
        {
            var query = await _thietBiRepository.GetByIdAsync(id);
            return _thietBiConverter.EntityToDTO(query);
        }

        public Task<ResponseObject<DataResponseThietBi>> UpdateThietBi(Request_UpdateThietBi request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<DataResponseThietBi>> XoaThietBi(int id)
        {
            throw new NotImplementedException();
        }
    }
}
