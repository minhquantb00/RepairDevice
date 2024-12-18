using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class BaoHanh : BaseEntity<int>
    {
        public int ThietBiId { get; set; }
        public virtual ThietBi? ThietBi { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc {  get; set; }
    }
}
 