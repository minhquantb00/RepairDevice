using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> UpdatePhanCongCongViec(Request_UpdatePhanCongCongViec request)
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
        public async Task<IActionResult> CreateXuatNhapKho(Request_CreateXuatNhapKho request)
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
        [HttpGet]
        public async Task<IActionResult> GetAllLinhKien()
        {
            return Ok(await _thietBiService.GetAllLinhKien());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLinhKienSuaChua()
        {
            return Ok(await _thietBiService.GetAllLinhKienSuaChua());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhanCongCongViecById([FromRoute] int id)
        {
            return Ok(await _thietBiService.GetPhanCongCongViecById(id));
        }
    }
}
