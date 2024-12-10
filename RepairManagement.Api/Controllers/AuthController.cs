using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairManagement.Application.Payloads.Requests.Auth;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Constants;

namespace RepairManagement.Api.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(Request_register request)
        {
            return Ok(await _authService.Register(request));
        }

        [HttpPost]
        public async Task<IActionResult> Login(Request_Login request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] Request_ChangePassword input)
        {
            await _authService.ChangePassword(input);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([FromBody] Request_ForgotPassword input)
        {
            await _authService.ForgotPassword(input);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmCreateNewPassword([FromBody] Request_ConfirmCreateNewPassword input)
        {
            await _authService.ConfirmCreateNewPassword(input);
            return Ok();
        }
    }
}
