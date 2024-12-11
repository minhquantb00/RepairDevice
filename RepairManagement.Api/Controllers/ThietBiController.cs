using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Application.Payloads.Requests.DeviceType;
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoaiThietBiById([FromRoute] int id)
        {
            return Ok(await _loaiThietBiService.GetLoaiThietBiById(id));
        }
    }
}
