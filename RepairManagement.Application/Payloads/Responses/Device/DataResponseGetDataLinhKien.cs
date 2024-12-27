using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseGetDataLinhKien
    {
        public IQueryable<DataResponseLinhKien> DataResponseLinhKiens { get; set; }
        public int TongTien { get; set; }
    }
}
