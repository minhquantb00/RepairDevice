using Microsoft.AspNetCore.Http;
using RepairManagement.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreateThietBi
    {
        public string TenThietBi { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? ImageUrl { get; set; }
        public int LoaiThietBiId { get; set; }
        public double? Gia {  get; set; }
        public string MoTa { get; set; }
        [DataType(DataType.Upload)]
        public Enumerate.TrangThaiThietBi? Status { get; set; }
        public int? KhachHangId { get; set; }
    }
}
