using RepairManagement.Commons.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_UpdateThietBiSuaChua
    {
        public int Id { get; set; }
        public string MoTaLoi { get; set; }
        public string GhiChuCuaKhachHang { get; set; }
        [DataType(DataType.Upload)]
        public Enumerate.ThietBiSuaChuaStatus ThietBiSuaChuaStatus { get; set; }
    }
}
