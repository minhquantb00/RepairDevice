using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Auth
{
    public class Request_Login
    {
        public string Email { get; set; }
        public string MatKhau { get; set; }
    }
}
