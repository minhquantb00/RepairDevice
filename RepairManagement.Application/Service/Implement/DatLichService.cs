using Microsoft.AspNetCore.Http;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Booking;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Booking;
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
    public class DatLichService : IDatLichService
    {
        private readonly IRepository<DatLichSuaChua> _datLichSuaChuaRepository;
        private readonly DatLichConverter _datLichConverter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<KhachHang> _khachHangRepository;
        public DatLichService(IRepository<DatLichSuaChua> datLichSuaChuaRepository, DatLichConverter datLichConverter, IHttpContextAccessor httpContextAccessor, IRepository<KhachHang> khachHangRepository)
        {
            _datLichSuaChuaRepository = datLichSuaChuaRepository;
            _datLichConverter = datLichConverter;
            _httpContextAccessor = httpContextAccessor;
            _khachHangRepository = khachHangRepository;
        }

        public async Task<ResponseObject<DataResponseDatLich>> DatLichSuaChua(Request_DatLichSuaChua request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if(!currentUser.Identity.IsAuthenticated)
            {
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
                if(request.ThoiGianDat < DateTime.Now)
                {
                    return new ResponseObject<DataResponseDatLich>
                    {
                        Data = null,
                        Message = "Bạn cần đặt lịch ít nhất là sau ngày hôm nay",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                DatLichSuaChua item = new DatLichSuaChua
                {
                    CreateTime = DateTime.Now,
                    DiaChi = request.DiaChi,
                    DichVuId = request.DichVuId,
                    Email = request.Email,
                    HoVaTen = request.Email,
                    MoTa = request.MoTa,
                    SoDienThoai = request.SoDienThoai,
                    TenThietBi = request.TenThietBi,
                    ThoiGianDat = request.ThoiGianDat,
                };
                item = await _datLichSuaChuaRepository.CreateAsync(item);
                return new ResponseObject<DataResponseDatLich>
                {
                    Data = _datLichConverter.EntityToDTO(item),
                    Message = "Đặt lịch thành công",
                    Status = StatusCodes.Status200OK
                };
            }
            string userId = currentUser.FindFirst("Id")?.Value;
            var khachHangItem = await _khachHangRepository.GetAsync(item => item.NguoiDungId == int.Parse(userId));
            if(khachHangItem == null)
            {
                return new ResponseObject<DataResponseDatLich>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            if (request.ThoiGianDat < DateTime.Now)
            {
                return new ResponseObject<DataResponseDatLich>
                {
                    Data = null,
                    Message = "Bạn cần đặt lịch ít nhất là sau ngày hôm nay",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            DatLichSuaChua datLich = new DatLichSuaChua
            {
                CreateTime = DateTime.Now,
                DiaChi = request.DiaChi,
                DichVuId = request.DichVuId,
                Email= request.Email,
                HoVaTen= request.HoVaTen,
                KhachHangId = khachHangItem.Id,
                MoTa = request.MoTa,
                SoDienThoai = request.SoDienThoai,
                TenThietBi = request.TenThietBi,
                ThoiGianDat = request.ThoiGianDat,
            };
            datLich = await _datLichSuaChuaRepository.CreateAsync(datLich);
            return new ResponseObject<DataResponseDatLich>
            {
                Data = _datLichConverter.EntityToDTO(datLich),
                Message = "Đặt lịch thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseDatLich>> GetAllBookings(int khachHangId)
        {
            var khachHang = await _khachHangRepository.GetAsync(record => record.Id == khachHangId);
            if (khachHang == null) return null;
            var query = await _datLichSuaChuaRepository.GetAllAsync(item => item.KhachHangId == khachHang.Id && item.ThoiGianDat >= DateTime.Now);

            return query.OrderByDescending(x => x.ThoiGianDat).Select(item => _datLichConverter.EntityToDTO(item));
        }
    }
}
