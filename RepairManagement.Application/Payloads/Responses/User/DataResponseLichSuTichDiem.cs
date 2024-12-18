using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.User
{
    public class DataResponseLichSuTichDiem
    {
        public int Id { get; set; }
        public DataResponseKhachHang DataResponseKhachHang { get; set; }
        public int Point { get; set; }
        public string Action { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
