using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreateHoaDon
    {
        public List<Request_CreateChiTietHoaDon> CreateChiTietHoaDons { get; set; }
    }
}
