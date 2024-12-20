using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseHieuSuatNhanVien
    {
        public int Id { get; set; }
        public DataResponseUser DataResponseUser { get; set; }
        public int SoThietBiDaSua { get; set; }
        public int TongThoiGianXuLy { get; set; }
    }
}
