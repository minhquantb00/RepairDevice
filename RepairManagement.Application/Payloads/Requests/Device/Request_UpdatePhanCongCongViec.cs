using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_UpdatePhanCongCongViec
    {
        public int Id { get; set; }
        public int NguoiDungId { get; set; }
        public int ThietBiSuaChuaId { get; set; }
        public string GhiChu { get; set; }
    }
}
