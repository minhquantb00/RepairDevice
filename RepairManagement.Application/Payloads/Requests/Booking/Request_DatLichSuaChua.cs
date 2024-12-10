using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Booking
{
    public class Request_DatLichSuaChua
    {
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public int DichVuId { get; set; }
        public DateTime ThoiGianDat { get; set; }
        public string MoTa { get; set; }
        public string TenThietBi { get; set; }
    }
}
