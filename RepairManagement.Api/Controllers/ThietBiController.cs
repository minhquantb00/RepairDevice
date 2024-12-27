using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairManagement.Application.Payloads.Requests.Auth;
using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.Requests.DeviceType;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Constants;

namespace RepairManagement.Api.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class ThietBiController : ControllerBase
    {
        private readonly IThietBiService _thietBiService;
        private readonly ILoaiThietBiService _loaiThietBiService;
        public ThietBiController(IThietBiService thietBiService, ILoaiThietBiService loaiThietBiService)
        {
            _thietBiService = thietBiService;
            _loaiThietBiService = loaiThietBiService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> CreateThietBi([FromForm] Request_CreateThietBi request)
        {
            return Ok(await _thietBiService.CreateThietBi(request));
        }
        [HttpGet]  
        public async Task<IActionResult> GetAllThietBis([FromQuery] FilterThietBi filter)
        {
            return Ok(await _thietBiService.GetAllThietBi(filter));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThietBiById([FromRoute]int id)
        {
            return Ok(await _thietBiService.GetThietBiById(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> CreateLoaiThietBi([FromForm] Request_CreateLoaiThietBi request)
        {
            return Ok(await _loaiThietBiService.CreateLoaiThietBi(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoaiThietBis()
        {
            return Ok(await _loaiThietBiService.GetAllLoaiThietBi());
        }
        [HttpPost]
        public async Task<IActionResult> CreateThietBiSuaChua(Request_CreateThietBiSuaChua request)
        {
            return Ok(await _thietBiService.CreateThietBiSuaChua(request));
        }
        [HttpPost]
        public async Task<IActionResult> CreateLichSuSuaChua(Request_CreateLichSuSuaChua request)
        {
            return Ok(await _thietBiService.CreateLichSuSuaChua(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoaiThietBiById([FromRoute] int id)
        {
            return Ok(await _loaiThietBiService.GetLoaiThietBiById(id));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateLinhKienSuaChua(Request_CreateLinhKienSuaChua request)
        {
            return Ok(await _thietBiService.CreateLinhKienSuaChua(request));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreatePhanCongCongViec(Request_CreatePhanCongCongViec request)
        {
            return Ok(await _thietBiService.CreatePhanCongCongViec(request));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateLinhKienSuaChua(Request_UpdateLinhKienSuaChua request)
        {
            return Ok(await _thietBiService.UpdateLinhKienSuaChua(request));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UpdatePhanCongCongViec([FromForm] Request_UpdatePhanCongCongViec request)
        {
            return Ok(await _thietBiService.UpdatePhanCongCongViec(request));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UpdateThietBi([FromForm] Request_UpdateThietBi request)
        {
            return Ok(await _thietBiService.UpdateThietBi(request));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UpdateThietBiSuaChua([FromForm] Request_UpdateThietBiSuaChua request)
        {
            return Ok(await _thietBiService.UpdateThietBiSuaChua(request));
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaThietBi([FromRoute] int id)
        {
            return Ok(await _thietBiService.XoaThietBi(id));
        }
        [HttpPost]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> CreateXuatNhapKho([FromForm] Request_CreateXuatNhapKho request)
        {
            return Ok(await _thietBiService.CreateXuatNhapKho(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllThietBiOfCustomer([FromQuery] FilterThietBiKhachHang filter)
        {
            return Ok(await _thietBiService.GetAllThietBiOfCustomer(filter));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllThietBiSuaChua([FromQuery] FilterThietBiKhachHang filter)
        {
            return Ok(await _thietBiService.GetAllThietBiSuaChua(filter));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPhanCong([FromQuery] int thietBiSuaChuaId)
        {
            return Ok(await _thietBiService.GetAllPhanCong(thietBiSuaChuaId));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser([FromQuery] string? keyword)
        {
            return Ok(await _thietBiService.GetAllUser(keyword));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThietBiSuaChuaById([FromRoute]int id)
        {
            return Ok(await _thietBiService.GetThietBiSuaChuaById(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetPhanCongCongViecByNhanVien(int nhanVienId)
        {
            return Ok(await _thietBiService.GetPhanCongCongViecByNhanVien(nhanVienId));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> XoaLinhKienSuaChua([FromRoute] int id)
        {
            return Ok(await _thietBiService.XoaLinhKienSuaChua(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLinhKien()
        {
            return Ok(await _thietBiService.GetAllLinhKien());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLinhKienSuaChua(int thietBiSuaChuaId)
        {
            return Ok(await _thietBiService.GetAllLinhKienSuaChua(thietBiSuaChuaId));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhanCongCongViecById([FromRoute] int id)
        {
            return Ok(await _thietBiService.GetPhanCongCongViecById(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLichSuSuaChua(int khachHangId)
        {
            return Ok(await _thietBiService.GetAllLichSuSuaChua(khachHangId));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> CreateLinhKien([FromForm] Request_CreateLinhKien request)
        {
            return Ok(await _thietBiService.CreateLinhKien(request));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> UpdateLinhKien([FromForm] Request_UpdateLinhKien request)
        {
            return Ok(await _thietBiService.UpdateLinhKien(request));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> XoaLinhKien([FromRoute] int id)
        {
            return Ok(await _thietBiService.XoaLinhKien(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNhanVien(Request_register request)
        {
            return Ok(await _thietBiService.CreateNhanVien(request));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            return Ok(await _thietBiService.GetUserById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            return Ok(await _thietBiService.DeleteUser(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHieuSuat([FromQuery] int userId)
        {
            return Ok(await _thietBiService.GetAllHieuSuat(userId));
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPhanCongCongViecChoXuLy()
        {
            return Ok(await _thietBiService.GetPhanCongCongViecChoXuLyBy());
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPhanCongCongViecDangXuLy()
        {
            return Ok(await _thietBiService.GetPhanCongCongViecDangXuLy());
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetPhanCongCongViecDaHoanThanh()
        {
            return Ok(await _thietBiService.GetPhanCongCongViecDaHoanThanh());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateVnPayUrl([FromQuery] int billId)
        {
            return Ok(await _thietBiService.CreateUrlPayment(billId, HttpContext));
        }
        [HttpGet]
        public async Task<IActionResult> VNPayReturn()
        {
            var vnpayData = HttpContext.Request.Query;
            return Ok(await _thietBiService.VnPayReturn(vnpayData));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllThongBaoByKhachHang([FromQuery]int khachHangId)
        {
            return Ok(await _thietBiService.GetAllThongBaoByKhachHang(khachHangId));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateHoaDon(Request_CreateHoaDon request)
        {
            return Ok(await _thietBiService.CreateHoaDon(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetDataLinhKien([FromQuery] int billId)
        {
            return Ok(await _thietBiService.GetDataLinhKien(billId));
        }
        [HttpGet]
        public async Task<IActionResult> GetDataLinhKienByNguoiDung([FromQuery] int nguoiDungId)
        {
            return Ok(await _thietBiService.GetDataLinhKienByNguoiDung(nguoiDungId));
        }
    }
}
