using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseXuatNhapKho
    {
        public string TenLinhKien { get; set; }
        public string LoaiGiaoDich { get; set; }
        public int SoLuong { get; set; }
        public DateTime ThoiGianGiaoDich { get; set; }
        public string TenNhanVien {  get; set; }
        public string GhiChu { get; set; }
    }
}
