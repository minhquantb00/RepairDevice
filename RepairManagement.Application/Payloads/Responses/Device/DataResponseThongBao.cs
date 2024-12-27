using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseThongBao
    {
        public int Id { get; set; }
        public DataResponseKhachHang DataResponseKhachHang { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGianGui { get; set; }
        public bool DaXem { get; set; }
    }
}
