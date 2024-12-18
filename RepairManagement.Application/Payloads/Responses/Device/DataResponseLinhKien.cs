using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseLinhKien
    {
        public int Id { get; set; }
        public string TenLinhKien { get; set; }
        public string LoaiLinhKien { get; set; }
        public double GiaNhap { get; set; }
        public double GiaBan { get; set; }
        public string MoTa { get; set; }
        public string? ImageUrl { get; set; }
    }
}
