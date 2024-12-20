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
    public class DataResponseHoaDon
    {
        public int Id { get; set; }
        public DataResponseKhachHang DataResponseKhachHang { get; set; }
        public double TongTien { get; set; }
        public string BillStatus { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? PayTime { get; set; }
        public IQueryable<DataResponseChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}
