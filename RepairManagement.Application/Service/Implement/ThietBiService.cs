using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepairManagement.Application.Handle.Media;
using RepairManagement.Application.Payloads.Converters;
using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Application.Payloads.Responses.User;
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
        
            
        public ThietBiService(IRepository<ThietBi> thietBiRepository, ThietBiConverter thietBiConverter, IHttpContextAccessor contextAccessor, IRepository<LoaiThietBi> loaiThietBiRepository, IRepository<ThietBiSuaChua> thietBiSuaChuaRepository, IRepository<LichSuSuaChua> lichSuSuaChuaRepository, IRepository<KhachHang> khachHangRepository, LichSuSuaChuaConverter lichSuSuaChuaConverter, ThietBiSuaChuaConverter thietBiSuaChuaConverter, LinhKienSuaChuaConverter linhKienSuaChuaConverter, IRepository<LinhKienSuaChuaThietBi> linhKienSuaChuaThietBiRepository, IRepository<LinhKien> linhKienRepository, IRepository<NguoiDung> nguoiDungRepository, NguoiDungConverter nguoiDungConverter, IRepository<PhanCongCongViec> phanCongCongViecRepository, PhanCongCongViecConverter phanCongViecConverter, IRepository<HangTonKho> hangTonKhoRepository, IRepository<XuatNhapKho> xuatNhapKhoRepository, XuatNhapKhoConverter xuatNhapKhoConverter, LinhKienConverter linhKienConverter)
        {
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
        }

        public async Task<ResponseObject<DataResponseLichSuSuaChua>> CreateLichSuSuaChua(Request_CreateLichSuSuaChua request)
        {
            var khachHang = await _khachHangRepository.GetByIdAsync(request.KhachHangId);
            if(khachHang == null)
            {
                return new ResponseObject<DataResponseLichSuSuaChua>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var thietBi = await _thietBiRepository.GetByIdAsync(request.ThietBiId);
            if(thietBi == null)
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
            LinhKienSuaChuaThietBi item = new LinhKienSuaChuaThietBi
            {
                LinhKienId = request.LinhKienId,
                ThietBiSuaChuaId = request.ThietBiSuaChuaId,
                SoLuongDung = request.SoLuongDung,
            };
            item = await _linhKienSuaChuaThietBiRepository.CreateAsync(item);
            return new ResponseObject<DataResponseLinhKienSuaChua>
            {
                Data = _linhKienSuaChuaConverter.EntityToDTO(item),
                Message = "Thêm thông tin linh kiện sửa chữa thành công",
                Status = StatusCodes.Status200OK
            };
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
            if(nguoiDung == null)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Người dùng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }

            var thietBiSuaChua = await _thietBiSuaChuaRepository.GetByIdAsync(request.ThietBiSuaChuaId);
            if(thietBiSuaChua == null)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Thông tin thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }

            var listPhanCongCongViec = await _phanCongCongViecRepository.GetAllAsync(item => item.ThietBiSuaChuaId == thietBiSuaChua.Id);
            if(listPhanCongCongViec.Count() > 0)
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
                Status = "",
                ThietBiSuaChuaId = request.ThietBiSuaChuaId,
                ThoiGianPhanCong = DateTime.Now
            };
            phanCongCongViec = await _phanCongCongViecRepository.CreateAsync(phanCongCongViec);

            thietBiSuaChua.Status = Commons.Enums.Enumerate.ThietBiSuaChuaStatus.DangSua;
            thietBiSuaChua = await _thietBiSuaChuaRepository.UpdateAsync(thietBiSuaChua);
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
            if(khachHang == null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Khách hàng không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var thietBi = await _thietBiRepository.GetByIdAsync(request.ThietBiId);
            if(thietBi == null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data = null,
                    Message = "Thiết bị không tồn tại",
                    Status = StatusCodes.Status404NotFound
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
            if(linhKien == null)
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
            if(request.LoaiGiaoDich == Commons.Enums.Enumerate.LoaiGiaoDichEnum.XuatHang)
            {
                if(hangTonKho.SoLuong >= request.SoLuong)
                {
                    hangTonKho.SoLuong -=request.SoLuong;
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
            var result = query.AsNoTracking().Select(item => _linhKienConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponseLinhKienSuaChua>> GetAllLinhKienSuaChua()
        {
            var query = await _linhKienSuaChuaThietBiRepository.GetAllAsync();
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
            if(filter.KhachHangId.HasValue)
            {
                query = query.AsNoTracking().Where(record => record.KhachHangId == filter.KhachHangId);
            }

            var result = query.AsNoTracking().Select(item => _thietBiConverter.EntityToDTO(item));
            return result;
        }

        public async Task<IQueryable<DataResponseThietBi>> GetAllThietBiOfCustomer(FilterThietBiKhachHang filter)
        {
            
            var query = await _thietBiRepository.GetAllAsync(x => x.KhachHangId != null);
            if (!string.IsNullOrEmpty(filter.Keyword))
            {
                var khachHang = await _khachHangRepository.GetAsync(x => x.Email.Contains(filter.Keyword) || x.SoDienThoai.Contains(filter.Keyword));
                if(khachHang != null)
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

        public async Task<IQueryable<DataResponseUser>> GetAllUser(string? keyword)
        {
            var query = await _nguoiDungRepository.GetAllAsync(x => !_nguoiDungRepository.GetRolesOfUserAsync(x).Result.Contains("Customer"));
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.AsNoTracking().Where(x => x.Email.Contains(keyword) || x.SoDienThoai.Contains(keyword) || x.HoVaTen.Contains(keyword)); 
            }
            var result = query.AsNoTracking().Select(x => _nguoiDungConverter.EntityToDTO(x));
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
            if(nhanVien == null)
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
            if(linhKienSuaChua == null)
            {
                return new ResponseObject<DataResponseLinhKienSuaChua>
                {
                    Data = null,
                    Message = "Linh kiện sửa chữa không tồn tại",
                    Status = StatusCodes.Status404NotFound
                };
            }
            var linhKien = await _linhKienRepository.GetByIdAsync((int)request.LinhKienId);
            linhKienSuaChua.ThietBiSuaChuaId = request.ThietBiSuaChuaId.HasValue ? (int) request.ThietBiSuaChuaId : linhKienSuaChua.ThietBiSuaChuaId;
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
            if (!currentUser.IsInRole("Admin"))
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Bạn không có quyền thực hiện chức năng này",
                    Status = StatusCodes.Status403Forbidden

                };
            }
            var phanCongCongViec = await _phanCongCongViecRepository.GetByIdAsync(request.Id);
            if(phanCongCongViec == null)
            {
                return new ResponseObject<DataResponsePhanCongCongViec>
                {
                    Data = null,
                    Message = "Thông tin phân công không tìm thấy",
                    Status = StatusCodes.Status404NotFound
                };
            }
            phanCongCongViec.NguoiDungId = request.NguoiDungId;
            phanCongCongViec.ThietBiSuaChuaId = request.ThietBiSuaChuaId;
            phanCongCongViec.GhiChu = request.GhiChu;
            phanCongCongViec = await _phanCongCongViecRepository.UpdateAsync(phanCongCongViec);
            return new ResponseObject<DataResponsePhanCongCongViec>
            {
                Data = _phanCongViecConverter.EntityToDTO(phanCongCongViec),
                Message = "Cập nhật thông tin phân công thành công"
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
            if(thietBi == null)
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
            thietBi.LoaiThietBiId = (int) request.LoaiThietBiId;
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
            if(thietBiSuaChua == null)
            {
                return new ResponseObject<DataResponseThietBiSuaChua>
                {
                    Data= null,
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
            if(thietBi == null)
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
    }
}
