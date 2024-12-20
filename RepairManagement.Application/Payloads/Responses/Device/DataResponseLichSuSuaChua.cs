using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseLichSuSuaChua
    {
        public int Id { get; set; }
        public string MoTaLoi { get; set; }
        public double Gia { get; set; }
        public DateTime ThoiGianSua { get; set; }
        public string GhiChu { get; set; }
        public string Status { get; set; }
        public string NhanVienSua { get; set; }
        public string TenThietBi { get; set; }
    }
}
