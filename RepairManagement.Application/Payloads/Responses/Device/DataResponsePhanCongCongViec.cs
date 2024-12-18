using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponsePhanCongCongViec
    {
        public int Id { get; set; }
        public DataResponseUser NhanVien { get; set; }
        public DataResponseThietBiSuaChua DataResponseThietBiSuaChua { get; set; }
        public DateTime ThoiGianPhanCong { get; set; }
        public DateTime? ThoiGianHoanThanh { get; set; }
        public string GhiChu { get; set; }
        public string Status { get; set; }
    }
}
