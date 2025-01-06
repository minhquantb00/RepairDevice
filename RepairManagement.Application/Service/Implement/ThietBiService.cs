using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using RepairManagement.Application.Handle.Email;
using RepairManagement.Application.Handle.Media;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Auth;
using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.HandleEmail;
using RepairManagement.Commons.HandleVnPay;
using RepairManagement.Commons.UltilitiesGlobal;
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
    {//
        private readonly IRepository<ThietBi> _thietBiRepository;
        private readonly ThietBiConverter _thietBiConverter;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository<LoaiThietBi> _loaiThietBiRepository;
        private readonly IRepository<ThietBiSuaChua> _thietBiSuaChuaRepository;
        private readonly IRepository<LichSuSuaChua> _lichSuSuaChuaRepository;
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly LichSuSuaChuaConverter _lichSuaChuaConverter;
        private readonly ThietBiSuaChuaConverter _thietBiSuaChuaConverter;
        private readonly LinhKienSuaChuaConverter _linhKienSuaChuaConverter;
        private readonly IRepository<LinhKien> _linhKienRepository;
        private readonly IRepository<LinhKienSuaChuaThietBi> _linhKienSuaChuaThietBiRepository;
        private readonly IRepository<NguoiDung> _nguoiDungRepository;
        private readonly NguoiDungConverter _nguoiDungConverter;
        private readonly IRepository<PhanCongCongViec> _phanCongCongViecRepository;
        private readonly PhanCongCongViecConverter _phanCongViecConverter;
        private readonly IRepository<HangTonKho> _hangTonKhoRepository;
        private readonly IRepository<XuatNhapKho> _xuatNhapKhoRepository;
        private readonly XuatNhapKhoConverter _xuatNhapKhoConverter;
        private readonly LinhKienConverter _linhKienConverter;
        private readonly IAuthService _authService;
        private readonly IRepository<ThongBao> _thongBaoRepository;
        private readonly IRepository<HieuSuatNhanVien> _hieuSuatNhanVienRepository;
        private readonly IEmailService _emailService;
        private readonly HieuSuatConverter _hieuSuatConverter;
        private readonly IRepository<HoaDon> _hoaDonRepository;
        private readonly IRepository<ChiTietHoaDon> _chiTietHoaDonRepository;
        private readonly IConfiguration _configuration;
        private readonly HoaDonConverter _hoaDonConverter;
        private readonly IRepository<LichSuTichDiem> _lichSuTichDiemRepository;
        private readonly ThongBaoConverter _thongBaoConverter;



        public ThietBiService(IRepository<ThietBi> thietBiRepository, ThietBiConverter thietBiConverter, IHttpContextAccessor contextAccessor, IRepository<LoaiThietBi> loaiThietBiRepository, IRepository<ThietBiSuaChua> thietBiSuaChuaRepository, IRepository<LichSuSuaChua> lichSuSuaChuaRepository, IRepository<KhachHang> khachHangRepository, LichSuSuaChuaConverter lichSuSuaChuaConverter, ThietBiSuaChuaConverter thietBiSuaChuaConverter, LinhKienSuaChuaConverter linhKienSuaChuaConverter, IRepository<LinhKienSuaChuaThietBi> linhKienSuaChuaThietBiRepository, IRepository<LinhKien> linhKienRepository, IRepository<NguoiDung> nguoiDungRepository, NguoiDungConverter nguoiDungConverter, IRepository<PhanCongCongViec> phanCongCongViecRepository, PhanCongCongViecConverter phanCongViecConverter, IRepository<HangTonKho> hangTonKhoRepository, IRepository<XuatNhapKho> xuatNhapKhoRepository, XuatNhapKhoConverter xuatNhapKhoConverter, LinhKienConverter linhKienConverter, IAuthService authService, IRepository<ThongBao> thongBaoRepository, IRepository<HieuSuatNhanVien> hieuSuatNhanVienRepository, IEmailService emailService, HieuSuatConverter hieuSuatConverter, IRepository<HoaDon> hoaDonRepository, IRepository<ChiTietHoaDon> chiTietHoaDonRepository, IConfiguration configuration, HoaDonConverter hoaDonConverter, IRepository<LichSuTichDiem> lichSuTichDiemRepository, ThongBaoConverter thongBaoConverter)
        {
            _lichSuTichDiemRepository = lichSuTichDiemRepository;
            _hoaDonConverter = hoaDonConverter;
            _configuration = configuration;
            _hoaDonRepository = hoaDonRepository;
            _chiTietHoaDonRepository = chiTietHoaDonRepository;
            _hieuSuatConverter = hieuSuatConverter;
            _thongBaoRepository = thongBaoRepository;
            _thietBiRepository = thietBiRepository;
            _thietBiConverter = thietBiConverter;
            _contextAccessor = contextAccessor;
            _loaiThietBiRepository = loaiThietBiRepository;
            _thietBiSuaChuaRepository = thietBiSuaChuaRepository;
            _lichSuSuaChuaRepository = lichSuSuaChuaRepository;
            _khachHangRepository = khachHangRepository;
            _lichSuaChuaConverter = lichSuSuaChuaConverter;
            _thietBiSuaChuaConverter = thietBiSuaChuaConverter;
            _linhKienSuaChuaThietBiRepository = linhKienSuaChuaThietBiRepository;
            _linhKienSuaChuaConverter = linhKienSuaChuaConverter;
            _linhKienRepository = linhKienRepository;
            _nguoiDungRepository = nguoiDungRepository;
            _nguoiDungConverter = nguoiDungConverter;
            _phanCongCongViecRepository = phanCongCongViecRepository;
            _phanCongViecConverter = phanCongViecConverter;
            _hangTonKhoRepository = hangTonKhoRepository;
            _xuatNhapKhoConverter = xuatNhapKhoConverter;
            _xuatNhapKhoRepository = xuatNhapKhoRepository;
            _linhKienConverter = linhKienConverter;
            _authService = authService;
            _hieuSuatNhanVienRepository = hieuSuatNhanVienRepository;
            _emailService = emailService;
            _thongBaoConverter = thongBaoConverter;
        }

        public async Task<ResponseObject<DataResponseLichSuSuaChua>> CreateLichSuSuaChua(Request_CreateLichSuSuaChua request)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(request.KhachHangId);
            if (khachHang == null)
            {
                return new ResponseObject<DataResponseLichSuSuaChua>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var thietBi = await _thietBiRepository.GetByIdAsync(request.ThietBiId);
            if (thietBi == null)
            {
                return new ResponseObject<DataResponseLichSuSuaChua>
                {
                    Data = null,
                    Message = "Thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            LichSuSuaChua lichSuSuaChua = new LichSuSuaChua
            {
                GhiChu = "",
                Gia = 0,
                KhachHangId = request.KhachHangId,
                MoTaLoi = "",
                NhanVienId = request.NhanVienId,
                Status = "",
                ThietBiId = request.ThietBiId,
                ThoiGianSua = DateTime.Now,
            };
            lichSuSuaChua = await _lichSuSuaChuaRepository.CreateAsync(lichSuSuaChua);
            return new ResponseObject<DataResponseLichSuSuaChua>
            {
                Data = _lichSuaChuaConverter.EntityToDTO(lichSuSuaChua),
                Message = "Tạo lịch sử thiết bị thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseLinhKienSuaChua>> CreateLinhKienSuaChua(Request_CreateLinhKienSuaChua request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden,
                };
            }
            var linhKien = await _linhKienRepository.GetByIdAsync(request.LinhKienId);
            var hangTonKho = await _hangTonKhoRepository.GetAsync(item => item.LinhKienId == request.LinhKienId);
            if (linhKien == null || hangTonKho.SoLuong == 0 || hangTonKho.SoLuong < request.SoLuongDung)
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Linh kiện không tồn tại hoặc đã hết hàng",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var thietBiSuaChua = await _thietBiSuaChuaRepository.GetByIdAsync(request.ThietBiSuaChuaId);
            if (thietBiSuaChua == null)
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var linhKienSuaChua = await _linhKienSuaChuaThietBiRepository.GetAsync(item => item.LinhKienId == request.LinhKienId && item.ThietBiSuaChuaId == thietBiSuaChua.Id);
            if (linhKienSuaChua != null)
            {
                linhKienSuaChua.SoLuongDung += request.SoLuongDung;
                linhKienSuaChua = await _linhKienSuaChuaThietBiRepository.UpdateAsync(linhKienSuaChua);
                hangTonKho.SoLuong -= request.SoLuongDung;
                hangTonKho = await _hangTonKhoRepository.UpdateAsync(hangTonKho);
                if (hangTonKho.SoLuong < 3)
                {
                    hangTonKho.WarningLevel = Commons.Enums.Enumerate.WarningLevelEnum.SapHetHang;
                    hangTonKho = await _hangTonKhoRepository.UpdateAsync(hangTonKho);
                    ThongBao thongBao = new ThongBao
                    {
                        DaXem = false,
                        NoiDung = $"Số lượng linh kiện {linhKien.TenLinhKien} sắp hết",
                        ThoiGianGui = DateTime.Now,
                        KhachHangId = thietBiSuaChua.KhachHangId,
                    };
                    thongBao = await _thongBaoRepository.CreateAsync(thongBao);
                }
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = _linhKienSuaChuaConverter.EntityToDTO(linhKienSuaChua),
                    Message = "Thêm thông tin linh kiện sửa chữa thành công",
                    Status = StatusCodes.Status200OK
                };
            }
            LinhKienSuaChuaThietBi item = new LinhKienSuaChuaThietBi
            {
                LinhKienId = request.LinhKienId,
                ThietBiSuaChuaId = request.ThietBiSuaChuaId,
                SoLuongDung = request.SoLuongDung,
            };
            item = await _linhKienSuaChuaThietBiRepository.CreateAsync(item);
            hangTonKho.SoLuong -= request.SoLuongDung;
            hangTonKho = await _hangTonKhoRepository.UpdateAsync(hangTonKho);
            if (hangTonKho.SoLuong < 3)
            {
                hangTonKho.WarningLevel = Commons.Enums.Enumerate.WarningLevelEnum.SapHetHang;
                hangTonKho = await _hangTonKhoRepository.UpdateAsync(hangTonKho);
                ThongBao thongBao = new ThongBao
                {
                    DaXem = false,
                    NoiDung = $"Số lượng linh kiện {linhKien.TenLinhKien} sắp hết",
                    ThoiGianGui = DateTime.Now,
                    KhachHangId = thietBiSuaChua.KhachHangId,
                };
                thongBao = await _thongBaoRepository.CreateAsync(thongBao);
            }
            return new ResponseObject<DataResponseLinhKienSuaChua>
            {
                Data = _linhKienSuaChuaConverter.EntityToDTO(item),
                Message = "Thêm thông tin linh kiện sửa chữa thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseLichSuSuaChua>> GetAllLichSuSuaChua(int khachHangId)
        {
            var query = await _lichSuSuaChuaRepository.GetAllAsync(item => item.KhachHangId == khachHangId);
            var result = query.Select(item => _lichSuaChuaConverter.EntityToDTO(item));
            return result;
        }

        public async Task<ResponseObject<DataResponsePhanCongCongViec>> CreatePhanCongCongViec(Request_CreatePhanCongCongViec request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(request.NguoiDungId);
            if (nguoiDung == null)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Người dùng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }

            var thietBiSuaChua = await _thietBiSuaChuaRepository.GetByIdAsync(request.ThietBiSuaChuaId);
            if (thietBiSuaChua == null)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Thông tin thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }

            var listPhanCongCongViec = await _phanCongCongViecRepository.GetAllAsync(item => item.ThietBiSuaChuaId == thietBiSuaChua.Id);
            if (listPhanCongCongViec.Count() > 0)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Thiết bị này đã được phân công cho nhân viên khác",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            PhanCongCongViec phanCongCongViec = new PhanCongCongViec
            {
                NguoiDungId = request.NguoiDungId,
                GhiChu = request.GhiChu,
                Status = Commons.Enums.Enumerate.ThietBiSuaChuaStatus.DangSua,
                ThietBiSuaChuaId = request.ThietBiSuaChuaId,
                ThoiGianPhanCong = DateTime.Now
            };
            phanCongCongViec = await _phanCongCongViecRepository.CreateAsync(phanCongCongViec);

            thietBiSuaChua.Status = Commons.Enums.Enumerate.ThietBiSuaChuaStatus.DangSua;
            thietBiSuaChua = await _thietBiSuaChuaRepository.UpdateAsync(thietBiSuaChua);
            var thietBi = await _thietBiRepository.GetByIdAsync(thietBiSuaChua.ThietBiId);
            LichSuSuaChua lichSuSuaChua = new LichSuSuaChua
            {
                GhiChu = thietBiSuaChua.GhiChuCuaKhachHang,
                Gia = 0,
                KhachHangId = (int)thietBi.KhachHangId,
                MoTaLoi = "",
                NhanVienId = phanCongCongViec.NguoiDungId,
                Status = thietBiSuaChua.Status.ToString(),
                ThietBiId = thietBiSuaChua.ThietBiId,
                ThoiGianSua = DateTime.Now,
            };

            lichSuSuaChua = await _lichSuSuaChuaRepository.CreateAsync(lichSuSuaChua);

            ThongBao thongBao = new ThongBao
            {
                DaXem = false,
                KhachHangId = thietBiSuaChua.KhachHangId,
                NoiDung = "Thiết bị của bạn sẽ được tiến hành sửa ngay bây giờ",
                ThoiGianGui = DateTime.Now
            };
            thongBao = await _thongBaoRepository.CreateAsync(thongBao);
            return new ResponseObject<DataResponsePhanCongCongViec>
            {
                Data = _phanCongViecConverter.EntityToDTO(phanCongCongViec),
                Message = "Phân công công việc thành công",
                Status = StatusCodes.Status200OK
            };
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
            if (!currentUser.IsInRole("Admin") && !currentUser.IsInRole("Employee"))
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }

            var loaiThietBi = await _loaiThietBiRepository.GetByIdAsync(request.LoaiThietBiId);
            if (loaiThietBi == null)
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
            if (request.KhachHangId.HasValue)
            {
                thietBi.KhachHangId = request.KhachHangId.Value;
            }

            thietBi = await _thietBiRepository.CreateAsync(thietBi);
            return new ResponseObject<DataResponseThietBi>
            {
                Data = _thietBiConverter.EntityToDTO(thietBi),
                Message = "Tạo thông tin thiết bị thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseThietBiSuaChua>> CreateThietBiSuaChua(Request_CreateThietBiSuaChua request)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(request.KhachHangId);
            if (khachHang == null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var thietBi = await _thietBiRepository.GetByIdAsync(request.ThietBiId);
            if (thietBi == null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var item = await _thietBiSuaChuaRepository.GetAsync(item => item.ThietBiId == request.ThietBiId);
            if(item != null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Thiết bị này đã được tạo thông tin sửa chữa",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            ThietBiSuaChua thietBiSuaChua = new ThietBiSuaChua
            {
                GhiChuCuaKhachHang = request.GhiChuCuaKhachHang,
                KhachHangId = request.KhachHangId,
                MoTaLoi = request.MoTaLoi,
                Status = Commons.Enums.Enumerate.ThietBiSuaChuaStatus.ChuaSua,
                TenThietBiSuaChua = thietBi.TenThietBi,
                ThietBiId = request.ThietBiId,
                ThoiGianBaoHanh = DateTime.Now.AddDays(15),
                ThoiGianDuKien = DateTime.Now.AddDays(1),
                ThoiGianNhanSua = DateTime.Now,
            };
            thietBiSuaChua = await _thietBiSuaChuaRepository.CreateAsync(thietBiSuaChua);
            return new ResponseObject<DataResponseThietBiSuaChua>
            {
                Data = _thietBiSuaChuaConverter.EntityToDTO(thietBiSuaChua),
                Message = "Tạo thông tin thiết bị cần sửa thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseXuatNhapKho>> CreateXuatNhapKho(Request_CreateXuatNhapKho request)
        {
            var linhKien = await _linhKienRepository.GetByIdAsync(request.LinhKienId);
            if (linhKien == null)
            {
                return new ResponseObject<DataResponseXuatNhapKho>
                {
                    Data = null,
                    Message = "Không tìm thấy dữ liệu",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var nhanVien = await _nguoiDungRepository.GetByIdAsync(request.NhanVienId);
            if (nhanVien == null)
            {
                return new ResponseObject<DataResponseXuatNhapKho>
                {
                    Data = null,
                    Message = "Không tìm thấy dữ liệu",
                    Status = StatusCodes.Status404NotFound
                };
            }
            XuatNhapKho xuatNhapKho = new XuatNhapKho
            {
                GhiChu = request.GhiChu,
                LinhKienId = request.LinhKienId,
                LoaiGiaoDich = request.LoaiGiaoDich,
                NhanVienId = request.NhanVienId,
                SoLuong = request.SoLuong,
                ThoiGianGiaoDich = DateTime.Now,
            };

            xuatNhapKho = await _xuatNhapKhoRepository.CreateAsync(xuatNhapKho);
            var hangTonKho = await _hangTonKhoRepository.GetAsync(item => item.LinhKienId == request.LinhKienId);
            if (request.LoaiGiaoDich == Commons.Enums.Enumerate.LoaiGiaoDichEnum.XuatHang)
            {
                if (hangTonKho.SoLuong >= request.SoLuong)
                {
                    hangTonKho.SoLuong -= request.SoLuong;
                }
                else
                {
                    return new ResponseObject<DataResponseXuatNhapKho>
                    {
                        Data = null,
                        Message = "Số lượng hàng trong kho không đủ",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
            }
            else
            {
                hangTonKho.SoLuong += request.SoLuong;
            }
            hangTonKho = await _hangTonKhoRepository.UpdateAsync(hangTonKho);
            return new ResponseObject<DataResponseXuatNhapKho>
            {
                Data = _xuatNhapKhoConverter.EntityToDTO(xuatNhapKho),
                Message = "Tạo thông tin thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseLinhKien>> GetAllLinhKien()
        {
            var query = await _linhKienRepository.GetAllAsync();
            var result = query.Select(item => _linhKienConverter.EntityToDTO(item));
            return result;
        }


        public async Task<IQueryable<DataResponseLinhKienSuaChua>> GetAllLinhKienSuaChua(int thietBiSuaChuaId)
        {
            var query = await _linhKienSuaChuaThietBiRepository.GetAllAsync(item => item.ThietBiSuaChuaId == thietBiSuaChuaId);
            var result = query.AsNoTracking().Select(item => _linhKienSuaChuaConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponsePhanCongCongViec>> GetAllPhanCong(int thietBiSuaChuaId)
        {
            var query = await _phanCongCongViecRepository.GetAllAsync(x => x.ThietBiSuaChuaId == thietBiSuaChuaId);
            return query.AsNoTracking().Select(item => _phanCongViecConverter.EntityToDTO(item));

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
            if (filter.KhachHangId.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.KhachHangId == filter.KhachHangId);
            }

            var result = query.Select(item => _thietBiConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponseThietBi>> GetAllThietBiOfCustomer(FilterThietBiKhachHang filter)
        {

            var query = await _thietBiRepository.GetAllAsync(x => x.KhachHangId != null);
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                var khachHang = await _khachHangRepository.GetAsync(x => x.Email.Contains(filter.Keyword) || x.SoDienThoai.Contains(filter.Keyword));
                if (khachHang != null)
                {
                    query = query.AsNoTracking().Where(record => record.KhachHangId == khachHang.Id);
                }
            }
            var result = query.AsNoTracking().Select(item => _thietBiConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponseThietBiSuaChua>> GetAllThietBiSuaChua(FilterThietBiKhachHang filter)
        {
            var query = await _thietBiSuaChuaRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                var khachHang = await _khachHangRepository.GetAsync(x => x.Email.Contains(filter.Keyword) || x.SoDienThoai.Contains(filter.Keyword));
                if (khachHang != null)
                {
                    query = query.AsNoTracking().Where(record => record.KhachHangId == khachHang.Id);
                }
            }
            var result = query.AsNoTracking().OrderBy(x => x.ThoiGianNhanSua).Select(item => _thietBiSuaChuaConverter.EntityToDTO(item));
            return result;
        }
        public async Task<ResponseObject<DataResponseUser>> CreateNhanVien(Request_register request)
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
                await _authService.AddRoleToUser(user.Id, new List<string> { "Employee" });

                return new ResponseObject<DataResponseUser>
                {
                    Data = _nguoiDungConverter.EntityToDTO(user),
                    Message = "Tạo tài khoản nhân viên thành công thành công",
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
        public async Task<IQueryable<DataResponseUser>> GetAllUser(string? keyword)
        {
            // Lấy tất cả người dùng từ cơ sở dữ liệu trước.
            var allUsers = await _nguoiDungRepository.GetAllAsync();

            // Lọc ra những người dùng không có role "Customer".
            var filteredUsers = new List<NguoiDung>();
            foreach (var user in allUsers)
            {
                var roles = await _nguoiDungRepository.GetRolesOfUserAsync(user);
                if (!roles.Contains("Customer"))
                {
                    filteredUsers.Add(user);
                }
            }

            // Thực hiện lọc theo từ khóa nếu có.
            if (!string.IsNullOrEmpty(keyword))
            {
                filteredUsers = filteredUsers
                    .Where(x => x.Email.Contains(keyword) ||
                                x.SoDienThoai.Contains(keyword) ||
                                x.HoVaTen.Contains(keyword))
                    .ToList();
            }

            // Ánh xạ sang DTO.
            var result = filteredUsers
                .AsQueryable()
                .Select(x => _nguoiDungConverter.EntityToDTO(x));

            return result;
        }


        public async Task<DataResponsePhanCongCongViec> GetPhanCongCongViecById(int id)
        {
            var query = await _phanCongCongViecRepository.GetByIdAsync(id);
            var result = _phanCongViecConverter.EntityToDTO(query);
            return result;
        }

        public async Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecByNhanVien(int nhanVienId)
        {
            var nhanVien = await _nguoiDungRepository.GetByIdAsync(nhanVienId);
            if (nhanVien == null)
            {
                return null;
            }
            var query = await _phanCongCongViecRepository.GetAllAsync(x => x.NguoiDungId == nhanVienId);
            var result = query.AsNoTracking().Select(item => _phanCongViecConverter.EntityToDTO(item));
            return result;
        }

        public async Task<DataResponseThietBi> GetThietBiById(int id)
        {
            var query = await _thietBiRepository.GetByIdAsync(id);
            return _thietBiConverter.EntityToDTO(query);
        }

        public async Task<DataResponseThietBiSuaChua> GetThietBiSuaChuaById(int id)
        {
            var query = await _thietBiSuaChuaRepository.GetByIdAsync(id);
            return _thietBiSuaChuaConverter.EntityToDTO(query);
        }

        public async Task<ResponseObject<DataResponseLinhKienSuaChua>> UpdateLinhKienSuaChua(Request_UpdateLinhKienSuaChua request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden

                };
            }
            var linhKienSuaChua = await _linhKienSuaChuaThietBiRepository.GetByIdAsync(request.Id);
            if (linhKienSuaChua == null)
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Linh kiện sửa chữa không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var linhKien = await _linhKienRepository.GetByIdAsync((int)request.LinhKienId);
            linhKienSuaChua.ThietBiSuaChuaId = request.ThietBiSuaChuaId.HasValue ? (int)request.ThietBiSuaChuaId : linhKienSuaChua.ThietBiSuaChuaId;
            linhKienSuaChua.LinhKienId = request.LinhKienId.HasValue ? (int)request.LinhKienId : linhKienSuaChua.LinhKienId;
            linhKienSuaChua.SoLuongDung = request.SoLuongDung.HasValue ? (int)request.SoLuongDung : linhKienSuaChua.SoLuongDung;

            linhKienSuaChua = await _linhKienSuaChuaThietBiRepository.UpdateAsync(linhKienSuaChua);
            return new ResponseObject<DataResponseLinhKienSuaChua>
            {
                Data = _linhKienSuaChuaConverter.EntityToDTO(linhKienSuaChua),
                Message = "Cập nhật thông tin linh kiện sửa chữa thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseLinhKienSuaChua>> XoaLinhKienSuaChua(int id)
        {
            var query = await _linhKienSuaChuaThietBiRepository.GetByIdAsync(id);
            await _linhKienSuaChuaThietBiRepository.DeleteAsync(query.Id);
            return new ResponseObject<DataResponseLinhKienSuaChua>
            {
                Data = null,
                Message = "Xóa linh kiện sửa chữa thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponsePhanCongCongViec>> UpdatePhanCongCongViec(Request_UpdatePhanCongCongViec request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            var phanCongCongViec = await _phanCongCongViecRepository.GetByIdAsync(request.Id);
            if (phanCongCongViec == null)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Thông tin phân công không tìm thấy",
                    Status = StatusCodes.Status404NotFound
                };
            }
            phanCongCongViec.GhiChu = request.GhiChu;
            phanCongCongViec.Status = request.Status;
            phanCongCongViec = await _phanCongCongViecRepository.UpdateAsync(phanCongCongViec);
            if (phanCongCongViec.Status == Commons.Enums.Enumerate.ThietBiSuaChuaStatus.HoanThanh)
            {
                phanCongCongViec.ThoiGianHoanThanh = DateTime.Now;
                phanCongCongViec = await _phanCongCongViecRepository.UpdateAsync(phanCongCongViec);


                var thietBiSuaChua = await _thietBiSuaChuaRepository.GetByIdAsync(phanCongCongViec.ThietBiSuaChuaId);
                if (thietBiSuaChua != null)
                {
                    thietBiSuaChua.Status = Commons.Enums.Enumerate.ThietBiSuaChuaStatus.HoanThanh;
                    thietBiSuaChua = await _thietBiSuaChuaRepository.UpdateAsync(thietBiSuaChua);
                    ThongBao thongBao = new ThongBao
                    {
                        DaXem = false,
                        KhachHangId = thietBiSuaChua.KhachHangId,
                        NoiDung = "Thiết bị của bạn đã được sửa xong",
                        ThoiGianGui = DateTime.Now,
                    };
                    thongBao = await _thongBaoRepository.CreateAsync(thongBao);
                    var thietBi = await _thietBiRepository.GetByIdAsync(thietBiSuaChua.ThietBiId);
                    if (thietBi != null)
                    {
                        thietBi.Status = Commons.Enums.Enumerate.TrangThaiThietBi.DaSuaXong;
                        thietBi = await _thietBiRepository.UpdateAsync(thietBi);
                        var lichSuSuaChua = await _lichSuSuaChuaRepository.GetAsync(item => item.ThietBiId == thietBi.Id);
                        if (lichSuSuaChua != null)
                        {
                            lichSuSuaChua.Status = thietBiSuaChua.Status.ToString();
                            lichSuSuaChua = await _lichSuSuaChuaRepository.UpdateAsync(lichSuSuaChua);
                        }
                    }

                    var hieuSuatNhanVien = await _hieuSuatNhanVienRepository.GetByIdAsync(phanCongCongViec.NguoiDungId);
                    var danhSachPhanCong = await _phanCongCongViecRepository.GetAllAsync(item => item.NguoiDungId == phanCongCongViec.NguoiDungId);

                    var tongThoiGianXuLy = danhSachPhanCong
                        .AsEnumerable()
                        .Sum(x => (x.ThoiGianHoanThanh - x.ThoiGianPhanCong)?.TotalHours ?? 0);
                    if (hieuSuatNhanVien == null)
                    {
                        HieuSuatNhanVien item = new HieuSuatNhanVien
                        {
                            NguoiDungId = phanCongCongViec.NguoiDungId,
                            SoThietBiDaSua = 1,
                            TongThoiGianXuLy = (int)tongThoiGianXuLy,
                        };
                        item = await _hieuSuatNhanVienRepository.CreateAsync(item);
                    }
                    else
                    {
                        hieuSuatNhanVien.SoThietBiDaSua += 1;
                        hieuSuatNhanVien.TongThoiGianXuLy = (int)tongThoiGianXuLy;
                        hieuSuatNhanVien.UpdateTime = DateTime.Now;
                        hieuSuatNhanVien = await _hieuSuatNhanVienRepository.UpdateAsync(hieuSuatNhanVien);
                    };
                    var khachHang = await _khachHangRepository.GetAsync(item => item.Id == thietBiSuaChua.KhachHangId);
                    if(khachHang != null)
                    {
                        var message = new Request_Message(new string[] { khachHang.Email }, "Thông báo tình trạng thiết bị: ", $"Thiết bị của bạn đã hoàn thành! Chúng tôi sẽ giao đến trong thời gian sớm nhất");
                        _emailService.SendEmail(message);
                    }
                    
                }
            }
            return new ResponseObject<DataResponsePhanCongCongViec>
            {
                Data = _phanCongViecConverter.EntityToDTO(phanCongCongViec),
                Message = "Cập nhật thông tin phân công thành công",
                Status = StatusCodes.Status200OK
            };

        }

        public async Task<ResponseObject<DataResponseThietBi>> UpdateThietBi(Request_UpdateThietBi request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var thietBi = await _thietBiRepository.GetByIdAsync(request.Id);
            if (thietBi == null)
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            thietBi.MoTa = request.MoTa;
            thietBi.Status = request.Status;
            thietBi.ImageUrl = request.ImageUrl != null ? await HandleUploadImage.Upfile(request.ImageUrl) : thietBi.ImageUrl;
            thietBi.LoaiThietBiId = (int)request.LoaiThietBiId;
            thietBi.TenThietBi = request.TenThietBi;
            thietBi = await _thietBiRepository.UpdateAsync(thietBi);
            return new ResponseObject<DataResponseThietBi>
            {
                Data = _thietBiConverter.EntityToDTO(thietBi),
                Message = "Cập nhật thông tin thiết bị thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseThietBiSuaChua>> UpdateThietBiSuaChua(Request_UpdateThietBiSuaChua request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var thietBiSuaChua = await _thietBiSuaChuaRepository.GetByIdAsync(request.Id);
            if (thietBiSuaChua == null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Không tìm thấy thông tin",
                    Status = StatusCodes.Status404NotFound
                };
            }
            thietBiSuaChua.MoTaLoi = request.MoTaLoi;
            thietBiSuaChua.GhiChuCuaKhachHang = request.GhiChuCuaKhachHang;
            thietBiSuaChua.Status = request.ThietBiSuaChuaStatus;
            thietBiSuaChua = await _thietBiSuaChuaRepository.UpdateAsync(thietBiSuaChua);
            return new ResponseObject<DataResponseThietBiSuaChua>
            {
                Data = _thietBiSuaChuaConverter.EntityToDTO(thietBiSuaChua),
                Message = "Cập nhật thông tin thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseThietBi>> XoaThietBi(int id)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var thietBi = await _thietBiRepository.GetByIdAsync(id);
            if (thietBi == null)
            {
                return new ResponseObject<DataResponseThietBi>
                {
                    Data = null,
                    Message = "Không tìm thấy thiết bị",
                    Status = StatusCodes.Status404NotFound
                };
            }
            await _thietBiRepository.DeleteAsync(thietBi.Id);
            return new ResponseObject<DataResponseThietBi>
            {
                Data = null,
                Message = "Xóa thông tin thiết bị thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseLinhKien>> CreateLinhKien(Request_CreateLinhKien request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLinhKien>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseLinhKien>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            LinhKien linhKien = new LinhKien
            {
                GiaBan = request.GiaBan,
                ImageUrl = request.ImageUrl != null ? await HandleUploadImage.Upfile(request.ImageUrl) : "",
                LoaiLinhKien = request.LoaiLinhKien,
                MoTa = request.MoTa,
                TenLinhKien = request.TenLinhKien,
                HangTonKhos = null
            };
            linhKien = await _linhKienRepository.CreateAsync(linhKien);
            List<HangTonKho> list = new List<HangTonKho>();
            if (linhKien != null)
            {
                HangTonKho hangTonKho = new HangTonKho
                {
                    LinhKienId = linhKien.Id,
                    SoLuong = 0,
                    WarningLevel = Commons.Enums.Enumerate.WarningLevelEnum.HetHang
                };
                hangTonKho = await _hangTonKhoRepository.CreateAsync(hangTonKho);
                list.Add(hangTonKho);
                linhKien.HangTonKhos = list;
            }

            return new ResponseObject<DataResponseLinhKien>
            {
                Data = _linhKienConverter.EntityToDTO(linhKien),
                Message = "Tạo thông tin linh kiện thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseLinhKien>> UpdateLinhKien(Request_UpdateLinhKien request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                return new ResponseObject<DataResponseLinhKien>
                {
                    Data = null,
                    Message = "Người dùng chưa được xác thực",
                    Status = StatusCodes.Status401Unauthorized
                };
            }
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponseLinhKien>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden
                };
            }
            var linhKien = await _linhKienRepository.GetByIdAsync(request.Id);
            linhKien.GiaBan = request.GiaBan;
            linhKien.ImageUrl = request.ImageUrl != null ? await HandleUploadImage.Upfile(request.ImageUrl) : linhKien.ImageUrl;
            linhKien.LoaiLinhKien = request.LoaiLinhKien;
            linhKien.MoTa = request.MoTa;
            linhKien.TenLinhKien = request.TenLinhKien;
            linhKien = await _linhKienRepository.UpdateAsync(linhKien);

            return new ResponseObject<DataResponseLinhKien>
            {
                Data = _linhKienConverter.EntityToDTO(linhKien),
                Message = "Cập nhật thông tin linh kiện thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<ResponseObject<DataResponseLinhKien>> XoaLinhKien(int id)
        {
            var linhKien = await _linhKienRepository.GetByIdAsync(id);
            await _linhKienRepository.DeleteAsync(linhKien.Id);
            return new ResponseObject<DataResponseLinhKien>
            {
                Data = null,
                Message = "Xóa linh kiện thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<DataResponseUser> GetUserById(int id)
        {
            var query = await _nguoiDungRepository.GetByIdAsync(id);
            return  _nguoiDungConverter.EntityToDTO(query);
        }


        public async Task<ResponseObject<DataResponseUser>> DeleteUser(int id)
        {
            var query = await _nguoiDungRepository.GetByIdAsync(id);
            await _nguoiDungRepository.DeleteAsync(query.Id);
            return new ResponseObject<DataResponseUser>
            {
                Data = null,
                Message = "Xóa người dùng thành công",
                Status = StatusCodes.Status200OK
            };
        }

        public async Task<IQueryable<DataResponseHieuSuatNhanVien>> GetAllHieuSuat(int userId)
        {
            var query = await _hieuSuatNhanVienRepository.GetAllAsync(item => item.NguoiDungId == userId);
            var result = query.Select(item => _hieuSuatConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecChoXuLyBy()
        {
            var currentUser = _contextAccessor.HttpContext.User;
            string userId = currentUser?.FindFirst("Id")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User ID not found in the current context.");
            }

            int parsedUserId = int.Parse(userId);

            // Lấy khách hàng
            var khachHang = await _khachHangRepository.GetAsync(item => item.NguoiDungId == parsedUserId);
            if (khachHang == null)
            {
                throw new InvalidOperationException("No customer found for the given user ID.");
            }

            // Lấy danh sách thiết bị sửa chữa
            var listThietBiSuaChua = await _thietBiSuaChuaRepository.GetAllAsync(item => item.KhachHangId == khachHang.Id);
            var thietBiIds = listThietBiSuaChua.Select(item => item.Id).Distinct().ToList();

            // Lấy danh sách phân công
            var listPhanCong = await _phanCongCongViecRepository.GetAllAsync(x =>
                thietBiIds.Contains(x.ThietBiSuaChuaId) &&
                x.Status == Commons.Enums.Enumerate.ThietBiSuaChuaStatus.ChuaSua);

            var phanCongIds = listPhanCong.Select(item => item.Id).Distinct().ToList();

            // Lấy danh sách phân công cuối cùng
            var query = await _phanCongCongViecRepository.GetAllAsync(x => phanCongIds.Contains(x.Id));
            var result = query.Select(item => _phanCongViecConverter.EntityToDTO(item));

            return result;
        }


        public async Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecDangXuLy()
        {
            var currentUser = _contextAccessor.HttpContext.User;
            string userId = currentUser.FindFirst("Id").Value;
            var khachHang = await _khachHangRepository.GetAsync(item => item.NguoiDungId == int.Parse(userId));
            var listThietBiSuaChua = await _thietBiSuaChuaRepository.GetAllAsync(item => item.KhachHangId == khachHang.Id).Result.Select(item => item.Id).Distinct().ToListAsync();
            var listPhanCongId = _phanCongCongViecRepository.GetAllAsync(x => listThietBiSuaChua.Contains(x.ThietBiSuaChuaId) && x.Status == Commons.Enums.Enumerate.ThietBiSuaChuaStatus.DangSua).Result.Select(item => item.Id).Distinct().ToList();

            var query = await _phanCongCongViecRepository.GetAllAsync(x => listPhanCongId.Contains(x.Id));
            var result = query.Select(item => _phanCongViecConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponsePhanCongCongViec>> GetPhanCongCongViecDaHoanThanh()
        {
            var currentUser = _contextAccessor.HttpContext.User;
            string userId = currentUser.FindFirst("Id").Value;
            var khachHang = await _khachHangRepository.GetAsync(item => item.NguoiDungId == int.Parse(userId));
            var listThietBiSuaChua = await _thietBiSuaChuaRepository.GetAllAsync(item => item.KhachHangId == khachHang.Id).Result.Select(item => item.Id).Distinct().ToListAsync();
            var listPhanCongId = _phanCongCongViecRepository.GetAllAsync(x => listThietBiSuaChua.Contains(x.ThietBiSuaChuaId) && x.Status == Commons.Enums.Enumerate.ThietBiSuaChuaStatus.HoanThanh).Result.Select(item => item.Id).Distinct().ToList();
            //var listBillId = _chiTietHoaDonRepository.GetAllAsync(item => listThietBiSuaChua.Contains(item.ThietBiSuaChuaId)).Result.Select(x => x.HoaDonId).ToHashSet();
            var query = await _phanCongCongViecRepository.GetAllAsync(x => listPhanCongId.Contains(x.Id));
            var result = query.Select(item => _phanCongViecConverter.EntityToDTO(item)).ToList();

            //foreach(var item in listBillId)
            //{
            //    var bill = await _hoaDonRepository.GetByIdAsync(item);
            //    if(bill.BillStatus == Commons.Enums.Enumerate.BillStatus.DaThanhToan)
            //    {
            //        result.Remove()
            //    }
            //}
            return result.AsQueryable();
        }

        public async Task<ResponseObject<string>> CreateUrlPayment(int billId, HttpContext httpContext)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            string userId = currentUser.FindFirst("Id").Value;
            
            var user = await _nguoiDungRepository.GetByIdAsync(int.Parse(userId));

            
            var bill = await _hoaDonRepository.GetByIdAsync(billId);
            var khachHang = await _khachHangRepository.GetByIdAsync(bill.KhachHangId);
            if (user.Id == khachHang.NguoiDungId)
            {
                if (bill.BillStatus == Commons.Enums.Enumerate.BillStatus.DaThanhToan)
                {
                    return new ResponseObject<string>
                    {
                        Data = null,
                        Message = "Hóa đơn đã được thanh toán trước đó",
                        Status = StatusCodes.Status400BadRequest
                    };
                }
                if (bill.BillStatus == Commons.Enums.Enumerate.BillStatus.ChuaThanhToan && bill.TongTien != 0 && bill.TongTien != null)
                {
                    VNPayLibrary pay = new VNPayLibrary();

                    pay.AddRequestData("vnp_Version", "2.1.0");
                    pay.AddRequestData("vnp_Command", "pay");
                    pay.AddRequestData("vnp_TmnCode", "ZENP933A");
                    pay.AddRequestData("vnp_Amount", (double.Parse((bill.TongTien * 100).ToString()).ToString()));
                    pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    pay.AddRequestData("vnp_CurrCode", "VND");
                    pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContext) ?? "127.0.0.1");
                    pay.AddRequestData("vnp_Locale", "vn");
                    pay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang {bill.Id}");
                    pay.AddRequestData("vnp_OrderType", "other");
                    pay.AddRequestData("vnp_ReturnUrl", _configuration.GetSection("VnPay:ReturnUrl").Value);
                    pay.AddRequestData("vnp_TxnRef", bill.Id.ToString());

                    string url = pay.CreateRequestUrl(_configuration.GetSection("VnPay:vnp_Url").Value, _configuration.GetSection("VnPay:vnp_HashSecret").Value);
                    return new ResponseObject<string>
                    {
                        Data = url,
                        Message ="Lấy dữ liệu thành công",
                        Status = 200    
                    };
                }
                return new ResponseObject<string>
                {
                    Data = null,
                    Message = "Vui lòng kiểm tra lại hóa đơn",
                    Status = StatusCodes.Status400BadRequest
                };
            }
            return new ResponseObject<string>
            {
                Data = null,
                Message = "Vui lòng kiểm tra lại thông tin người thanh toán",
                Status = StatusCodes.Status400BadRequest
            };
        }

        public async Task<string> VnPayReturn(IQueryCollection vnpayData)
        {
            string vnp_TmnCode = _configuration.GetSection("VnPay:vnp_TmnCode").Value;
            string vnp_HashSecret = _configuration.GetSection("VnPay:vnp_HashSecret").Value;

            VNPayLibrary vnPayLibrary = new VNPayLibrary();
            foreach (var (key, value) in vnpayData)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnPayLibrary.AddResponseData(key, value);
                }
            }

            string hoaDonId = vnPayLibrary.GetResponseData("vnp_TxnRef");
            string vnp_ResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnPayLibrary.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = vnPayLibrary.GetResponseData("vnp_SecureHash");
            double vnp_Amount = Convert.ToDouble(vnPayLibrary.GetResponseData("vnp_Amount"));
            bool check = vnPayLibrary.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            if (check)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    var bill = await _hoaDonRepository.GetByIdAsync(int.Parse(hoaDonId));

                    if (bill == null)
                    {
                        return "Không tìm thấy hóa đơn";
                    }

                    bill.BillStatus = Commons.Enums.Enumerate.BillStatus.DaThanhToan;
                    bill.PayTime = DateTime.Now;
                    bill = await _hoaDonRepository.UpdateAsync(bill);
                    var khachHang = await _khachHangRepository.GetByIdAsync(bill.KhachHangId);
                    var user = await _nguoiDungRepository.GetAsync(item => item.Id == khachHang.NguoiDungId);
                    if (user != null)
                    {
                        string email = user.Email;
                        string mss = HandleSendEmail.SendEmail(new EmailTo
                        {
                            To = email,
                            Subject = $"Thanh Toán đơn hàng: {bill.Id}",
                            Content = "Bạn đã thanh toán thành công! Xin chân thành cảm ơn"
                        });
                    }
                    LichSuTichDiem record = new LichSuTichDiem
                    {
                        Action  = Commons.Enums.Enumerate.Action.TichDiem,
                        CreateTime = DateTime.Now,
                        KhachHangId = khachHang.Id,
                        Point = 1
                    };
                    record = await _lichSuTichDiemRepository.CreateAsync(record);

                    khachHang.Diem =  _lichSuTichDiemRepository.GetAllAsync(record => record.KhachHangId == khachHang.Id).Result.Sum(x => x.Point);
                    khachHang = await _khachHangRepository.UpdateAsync(khachHang);

                    var listCTHoaDon = await _chiTietHoaDonRepository.GetAllAsync(record => record.HoaDonId == bill.Id);
                    foreach(var item in listCTHoaDon)
                    {
                        var thietBiSuaChua = await _thietBiSuaChuaRepository.GetAsync(x => x.Id == item.ThietBiSuaChuaId);
                        if(thietBiSuaChua != null)
                        {
                            var phanCongCongViec = await _phanCongCongViecRepository.GetAllAsync(x => x.ThietBiSuaChuaId == thietBiSuaChua.Id);
                            if (phanCongCongViec.Any())
                            {
                                await _phanCongCongViecRepository.DeleteRangeAsync(phanCongCongViec);
                            }
                        }
                    }
                    return "Giao dịch thành công đơn hàng " + bill.Id;
                }
                else
                {
                    return $"Lỗi trong khi thực hiện giao dịch. Mã lỗi: {vnp_ResponseCode}";
                }
            }
            else
            {
                return "Có lỗi trong quá trình xử lý";
            }
        }

        public async Task<DataResponseGetDataLinhKien> GetDataLinhKien(int billId)
        {
            var bill = await _hoaDonRepository.GetByIdAsync(billId);
            if (bill == null)
            {
                throw new ArgumentNullException(nameof(bill));
            }

            var billDetails = await _chiTietHoaDonRepository.GetAllAsync(x => x.HoaDonId == billId);
            if (!billDetails.Any())
            {
                return null;
            }

            List<DataResponseLinhKien> listDataLinhKien = new List<DataResponseLinhKien>();
            var tongTien = 0;

            foreach (var billDetail in billDetails)
            {
                var thietBiSuaChua = await _thietBiSuaChuaRepository.GetByIdAsync(billDetail.ThietBiSuaChuaId);
                if (thietBiSuaChua != null)
                {
                    var listLinhKienSuaChua = await _linhKienSuaChuaThietBiRepository
                        .GetAllAsync(x => x.ThietBiSuaChuaId == thietBiSuaChua.Id);

                    var linhKienIds = listLinhKienSuaChua.Select(item => item.LinhKienId).ToHashSet();

                    if (linhKienIds.Any())
                    {
                        var listLinhKien = await _linhKienRepository
                            .GetAllAsync(item => linhKienIds.Contains(item.Id));

                        var resultLinhKien = listLinhKien
                            .Select(x => _linhKienConverter.EntityToDTO(x))
                            .ToList();

                        listDataLinhKien.AddRange(resultLinhKien);

                        tongTien += (int) resultLinhKien.Sum(x => x.GiaBan);
                    }
                }
            }

            return new DataResponseGetDataLinhKien
            {
                DataResponseLinhKiens = listDataLinhKien.AsQueryable(),
                TongTien = tongTien
            };
        }

        public async Task<DataResponseGetDataLinhKien> GetDataLinhKienByNguoiDung(int nguoiDungId)
        {
            // Lấy thông tin người dùng
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(nguoiDungId);
            if (nguoiDung == null)
            {
                throw new ArgumentNullException(nameof(nguoiDung));
            }

            // Lấy thông tin khách hàng của người dùng
            var khachHang = await _khachHangRepository.GetAsync(x => x.NguoiDungId == nguoiDungId);
            if (khachHang == null)
            {
                throw new ArgumentNullException(nameof(khachHang));
            }

            // Lấy danh sách thiết bị sửa chữa đã hoàn thành của khách hàng
            var listThietBiSuaChuaId = (await _thietBiSuaChuaRepository
                .GetAllAsync(record => record.KhachHangId == khachHang.Id && record.Status == Commons.Enums.Enumerate.ThietBiSuaChuaStatus.HoanThanh))
                .Select(item => item.Id)
                .ToHashSet();

            if (!listThietBiSuaChuaId.Any())
            {
                throw new ArgumentNullException(nameof(listThietBiSuaChuaId));
            }

            // Lấy chi tiết hóa đơn của các thiết bị sửa chữa
            var listBillDetail = await _chiTietHoaDonRepository.GetAllAsync(x => listThietBiSuaChuaId.Contains(x.ThietBiSuaChuaId));

            // Khởi tạo danh sách Linh Kiện Id
            var listLinhKienId = new List<int>();

            // Duyệt qua chi tiết hóa đơn để lấy thông tin Linh Kiện
            foreach (var item in listBillDetail)
            {
                var bill = await _hoaDonRepository.GetAsync(x => x.KhachHangId == khachHang.Id);
                if (bill == null || bill.BillStatus == Commons.Enums.Enumerate.BillStatus.ChuaThanhToan)
                {
                    // Lấy Linh Kiện nếu hóa đơn chưa thanh toán
                    var linhKienIds = await _linhKienSuaChuaThietBiRepository
                        .GetAllAsync(x => x.ThietBiSuaChuaId == item.ThietBiSuaChuaId).Result
                        .Select(y => y.LinhKienId)
                        .ToListAsync();
                    listLinhKienId.AddRange(linhKienIds);
                }
                else
                {
                    // Lấy Linh Kiện nếu hóa đơn đã thanh toán
                    var linhKienIds = await _linhKienSuaChuaThietBiRepository
                        .GetAllAsync(x => x.ThietBiSuaChuaId != item.ThietBiSuaChuaId).Result
                        .Select(y => y.LinhKienId)
                        .ToListAsync();
                    listLinhKienId.AddRange(linhKienIds);
                }
            }

            // Kiểm tra nếu không có Linh Kiện nào
            if (!listLinhKienId.Any())
            {
                return new DataResponseGetDataLinhKien
                {
                    DataResponseLinhKiens = null,
                    TongTien = 0
                };
            }

            // Lấy thông tin các Linh Kiện từ ID đã lọc
            var listLinhKien = await _linhKienRepository.GetAllAsync(record => listLinhKienId.Contains(record.Id));
            if (listLinhKien == null || !listLinhKien.Any())
            {
                return new DataResponseGetDataLinhKien
                {
                    DataResponseLinhKiens = null,
                    TongTien = 0
                };
            }

            // Tính tổng tiền của các Linh Kiện
            var tongTien = (int)listLinhKien.Sum(x => x.GiaBan);

            // Trả về kết quả
            return new DataResponseGetDataLinhKien
            {
                DataResponseLinhKiens = listLinhKien.Select(item => _linhKienConverter.EntityToDTO(item)),
                TongTien = tongTien
            };
        }





        public async Task<IQueryable<DataResponseThongBao>> GetAllThongBaoByKhachHang(int khachHangId)
        {
            var query = await _thongBaoRepository.GetAllAsync(item => item.KhachHangId == khachHangId);
            if(query == null)
            {
                return null;
            }
            var result = query.Select(item => _thongBaoConverter.EntityToDTO(item));
            return result;
        }

        public async Task<ResponseObject<DataResponseHoaDon>> CreateHoaDon(Request_CreateHoaDon request)
        {
            var currentUser = _contextAccessor.HttpContext.User;
            string userId = currentUser.FindFirst("Id").Value;

            var khachHang = await _khachHangRepository.GetAsync(item => item.NguoiDungId == int.Parse(userId));

            HoaDon hoaDon = new HoaDon
            {
                BillStatus = Commons.Enums.Enumerate.BillStatus.ChuaThanhToan,
                CreateTime = DateTime.Now,
                KhachHangId = khachHang.Id,
                TongTien = 0,
                ChiTietHoaDons = null,

            };
            hoaDon = await _hoaDonRepository.CreateAsync(hoaDon);

            var chiTiet = await CreateListChiTietHoaDon(hoaDon.Id, request.CreateChiTietHoaDons);
            hoaDon.ChiTietHoaDons = chiTiet;
            hoaDon = await _hoaDonRepository.UpdateAsync(hoaDon);
            
            double tong = 0;
            foreach(var item in hoaDon.ChiTietHoaDons)
            {
                tong += item.UnitPrice;
            }
            hoaDon.TongTien = tong;
            hoaDon = await _hoaDonRepository.UpdateAsync(hoaDon);
            return new ResponseObject<DataResponseHoaDon>
            {
                Data = _hoaDonConverter.EntityToDTO(hoaDon),
                Message = "Tạo hóa đơn thành công",
                Status = StatusCodes.Status200OK
            };
        }

        private async Task<List<ChiTietHoaDon>> CreateListChiTietHoaDon(int hoaDonId, List<Request_CreateChiTietHoaDon> request_CreateChiTietHoaDons)
        {
            var hoaDon = await _hoaDonRepository.GetByIdAsync(hoaDonId);

            List<ChiTietHoaDon> result = new List<ChiTietHoaDon>();
            foreach(var item in request_CreateChiTietHoaDons)
            {
                var thietBi = await _thietBiSuaChuaRepository.GetByIdAsync(item.ThietBiSuaChuaId);
                ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon
                {
                    Description = "",
                    HoaDonId = hoaDonId,
                    ThietBiSuaChuaId = item.ThietBiSuaChuaId,
                    UnitPrice = _thietBiSuaChuaConverter.EntityToDTO(thietBi).TongTien
                };
                chiTietHoaDon = await _chiTietHoaDonRepository.CreateAsync(chiTietHoaDon);
                result.Add(chiTietHoaDon );
            }
            return result;
        }

        public async Task<IQueryable<DataResponseStatistics>> GetStatistics()
        {
            var bills = await _hoaDonRepository.GetAllAsync(record => record.BillStatus == Commons.Enums.Enumerate.BillStatus.DaThanhToan);

            var listResult = bills.GroupBy(x => x.CreateTime.Month).Select(group => new DataResponseStatistics
            {
                Thang = group.Key,
                DoanhSo = bills.Sum(item => item.TongTien)
            });

            var allMonths = Enumerable.Range(1, 12);

            var result = allMonths
                .Select(month => listResult.FirstOrDefault(stat => stat.Thang == month) ?? new DataResponseStatistics
                {
                    Thang = month,
                    DoanhSo = 0
                })
                .OrderBy(stat => stat.Thang)
                .ToList();

            return result.AsQueryable();
        }
    }
}
