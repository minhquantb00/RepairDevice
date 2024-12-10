using RepairManagement.Application.Payloads.Responses.Service;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Booking
{
    public class DataResponseDatLich
    {
        public int Id { get; set; }
        public DataResponseKhachHang DataResponseKhachHang { get; set; }
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DataResponseService DataResponseService { get; set; }
        public DateTime ThoiGianDat { get; set; }
        public DateTime CreateTime { get; set; }
        public string MoTa { get; set; }
        public string TenThietBi { get; set; }
    }
}
