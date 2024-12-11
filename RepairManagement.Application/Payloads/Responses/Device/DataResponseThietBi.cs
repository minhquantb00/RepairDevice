using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseThietBi
    {
        public int Id { get; set; }
        public string TenThietBi { get; set; }
        public string? ImageUrl { get; set; }
        public double? Gia  { get; set; }
        public DataResponseKhachHang DataResponseKhachHang { get; set; }
        public string MoTa { get; set; }
        public string Status { get; set; }
    }
}
