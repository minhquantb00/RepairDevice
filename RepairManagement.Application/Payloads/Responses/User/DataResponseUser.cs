using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.User
{
    public class DataResponseUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public IQueryable<DataResponseKhachHang> DataResponseKhachHangs { get; set; }
    }
}
