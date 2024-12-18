using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Customer
{
    public class Request_CreateLichSuTichDiem
    {
        public int KhachHangId { get; set; }
        [DataType(DataType.Upload)]
        public Enumerate.Action Action { get; set; }
    }
}
