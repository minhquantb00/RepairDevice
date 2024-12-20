using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_UpdateUser
    {
        public int Id { get; set; }
        public string HoVaTen { get; set; }
        public string MatKhau { get; set; }
        public string SoDienThoai { get; set; }
    }
}
