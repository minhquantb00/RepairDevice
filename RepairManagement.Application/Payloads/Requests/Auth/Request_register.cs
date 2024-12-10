using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Auth
{
    public class Request_register
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Ho va ten is required")]
        public string HoVaTen { get; set; }
        [Required(ErrorMessage = "Mat khau is required")]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "So dien thoai is required")]
        public string SoDienThoai { get; set; }
    }
}
