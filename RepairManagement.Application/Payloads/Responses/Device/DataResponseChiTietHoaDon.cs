using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseChiTietHoaDon
    {
        public int Id { get; set; }
        public DataResponseThietBiSuaChua DataResponseThietBiSuaChua { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
    }
}
