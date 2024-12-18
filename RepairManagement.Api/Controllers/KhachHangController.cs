using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairManagement.Application.Payloads.Requests.Customer;
using RepairManagement.Application.Payloads.ResponseDatas;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Constants;

namespace RepairManagement.Api.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangService _khachHangService;
        public KhachHangController(IKhachHangService khachHangService)
        {
            _khachHangService = khachHangService;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateKhachHang(Request_CreateKhachHang request)
        {
            return Ok(await _khachHangService.CreateKhachHang(request));
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateKhachHang(Request_UpdateKhachHang request)
        {
            return Ok(await _khachHangService.UpdateKhachHang(request));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteKhachHang([FromRoute] int id)
        {
            return Ok(await _khachHangService.DeleteKhachHang(id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllKhachHang([FromQuery]FilterCustomer? filter)
        {
            return Ok(await _khachHangService.GetAllKhachHang(filter));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKhachHangById([FromRoute] int id)
        {
            return Ok(await _khachHangService.GetKhachHangById(id));
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateLichSuTichDiem([FromForm] Request_CreateLichSuTichDiem request)
        {
            return Ok(await _khachHangService.CreateLichSuTichDiem(request));
        }
    }
}
