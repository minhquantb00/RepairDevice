using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairManagement.Application.Payloads.Requests.Service;
using RepairManagement.Application.Service.Interface;
using RepairManagement.Commons.Constants;

namespace RepairManagement.Api.Controllers
{
    [Route(Constant.DefaultValue.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class DichVuController : ControllerBase
    {
        private readonly IDichVuService _service;
        public DichVuController(IDichVuService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateService([FromBody]Request_CreateService request)
        {
            return Ok(await _service.CreateService(request));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllServices()
        {
            return Ok(await _service.GetAllServices());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceById([FromRoute]int id)
        {
            return Ok(await _service.GetServiceById(id));
        }
    }
}
