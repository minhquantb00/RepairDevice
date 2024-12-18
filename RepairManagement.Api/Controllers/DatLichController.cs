using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairManagement.Application.Payloads.Requests.Booking;
using RepairManagement.Application.Payloads.Responses.Booking;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Constants;

namespace RepairManagement.Api.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class DatLichController : ControllerBase
    {
        private readonly IDatLichService _datLichService;
        public DatLichController(IDatLichService datLichService)
        {
            _datLichService = datLichService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> DatLichSuaChua(Request_DatLichSuaChua request)
        {
            return Ok(await _datLichService.DatLichSuaChua(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBookings(int khachHangId)
        {
            return Ok(await _datLichService.GetAllBookings(khachHangId));
        }
    }
}
