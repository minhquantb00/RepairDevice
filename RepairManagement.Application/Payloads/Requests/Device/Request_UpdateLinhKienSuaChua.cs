using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_UpdateLinhKienSuaChua
    {
        public int Id { get; set; }
        public int? LinhKienId { get; set; }
        public int? ThietBiSuaChuaId { get; set; }
        public int? SoLuongDung { get; set; }
    }
}
