using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.DeviceType
{
    public class Request_CreateLoaiThietBi
    {
        public string Name { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }
    }
}
