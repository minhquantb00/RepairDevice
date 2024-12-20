using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreateLinhKien
    {
        public string TenLinhKien { get; set; }
        public string LoaiLinhKien { get; set; }
        public double GiaBan { get; set; }
        public string MoTa { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }
    }
}
