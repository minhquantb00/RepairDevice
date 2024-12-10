using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class ChiTietHoaDon : BaseEntity<int>
    {
        public int HoaDonId { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
        public int ThietBiId { get; set; }
        public virtual ThietBi? ThietBi { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
    }
}
