using RepairManagement.Application.Payloads.Requests.Device;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Customer
{
    public class Request_CreateKhachHang
    {
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string? DiaChi { get; set; }
        public List<Request_CreateThietBi>? Request_CreateThietBis { get; set; }
    }
}
