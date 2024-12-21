using Microsoft.AspNetCore.Http;
using RepairManagement.Application.Handle.Email;
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
        private readonly IEmailService _emailService;
        public DatLichService(IRepository<DatLichSuaChua> datLichSuaChuaRepository, DatLichConverter datLichConverter, IHttpContextAccessor httpContextAccessor, IRepository<KhachHang> khachHangRepository, IEmailService emailService)
        {
            _datLichSuaChuaRepository = datLichSuaChuaRepository;
            _datLichConverter = datLichConverter;
            _httpContextAccessor = httpContextAccessor;
            _khachHangRepository = khachHangRepository;
            _emailService = emailService;
        }

        public async Task<ResponseObject<DataResponseDatLich>> DatLichSuaChua(Request_DatLichSuaChua request)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;
            if(!currentUser.Identity.IsAuthenticated)
            {
                var checkEmail = await _khachHangRepository.GetAsync(item => item.Email.Equals(request.Email));
                if(checkEmail != null)
                {
                    return new ResponseObject<DataResponseDatLich>
                    {
                        Data = null,
                        Message = "Email đã tồn tại trong hệ thống vui lòng thử email khác hoặc đăng nhập tài khoản để đặt lịch",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                var checkPhoneNumber = await _khachHangRepository.GetAsync(item => item.SoDienThoai.Equals(request.SoDienThoai));
                if (checkPhoneNumber != null)
                {
                    return new ResponseObject<DataResponseDatLich>
                    {
                        Data = null,
                        Message = "Số điện thoại đã tồn tại trong hệ thống vui lòng thử số điện thoại khác hoặc đăng nhập tài khoản để đặt lịch",
                        Status = StatusCodes.Status400BadRequest
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
                var thoiGianDat = request.ThoiGianDat + TimeSpan.Parse(request.GioDat);
                if(thoiGianDat < DateTime.Now)
                {
                    return new ResponseObject<DataResponseDatLich>
                    {
                        Data = null,
                        Message = "Bạn cần đặt lịch ít nhất là sau ngày hôm nay",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                var datLichCheck = await _datLichSuaChuaRepository.GetAsync(item => item.ThoiGianDat == thoiGianDat && item.KhachHangId == khachHang.Id);
                if(datLichCheck != null)
                {
                    return new ResponseObject<DataResponseDatLich>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Data = null,
                        Message = "Trùng lịch vui lòng thử lại"
                    };
                }
                DatLichSuaChua item = new DatLichSuaChua
                {
                    CreateTime = DateTime.Now,
                    DiaChi = request.DiaChi,
                    DichVuId = request.DichVuId,
                    Email = request.Email,
                    HoVaTen = request.HoVaTen,
                    MoTa = request.MoTa,
                    SoDienThoai = request.SoDienThoai,
                    TenThietBi = request.TenThietBi,
                    ThoiGianDat = request.ThoiGianDat,
                };
                item = await _datLichSuaChuaRepository.CreateAsync(item);

                var messageitem = new Request_Message(new string[] { khachHang.Email }, "Thông tin đặt lịch: ", $"Thông đặt lịch của bạn đã được ghi nhận.");
                _emailService.SendEmail(messageitem);
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
            var thoiGianDatTime = request.ThoiGianDat + TimeSpan.Parse(request.GioDat);
            if (thoiGianDatTime < DateTime.Now)
            {
                return new ResponseObject<DataResponseDatLich>
                {
                    Data = null,
                    Message = "Bạn cần đặt lịch ít nhất là sau ngày hôm nay",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            var datLichCheckItem = await _datLichSuaChuaRepository.GetAsync(item => item.ThoiGianDat == thoiGianDatTime && item.KhachHangId == khachHangItem.Id);
            if (datLichCheckItem != null)
            {
                return new ResponseObject<DataResponseDatLich>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Data = null,
                    Message = "Trùng lịch vui lòng thử lại"
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
            var message = new Request_Message(new string[] { khachHangItem.Email }, "Thông tin đặt lịch: ", $"Thông đặt lịch của bạn đã được ghi nhận.");
            _emailService.SendEmail(message);
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
