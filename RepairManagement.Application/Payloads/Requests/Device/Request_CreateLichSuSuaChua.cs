using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreateLichSuSuaChua
    {
        public int KhachHangId { get; set; }
        public int ThietBiId { get; set; }
        public int NhanVienId { get; set; }
    }
}
