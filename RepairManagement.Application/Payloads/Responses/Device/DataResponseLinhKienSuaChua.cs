using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseLinhKienSuaChua
    {
        public int Id { get; set; }
        public DataResponseLinhKien DataResponseLinhKien { get; set; }
        public int? SoLuongDung { get; set; }
    }
}
