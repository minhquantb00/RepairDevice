using RepairManagement.Commons.Base;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class ChiPhiPhatSinh : BaseEntity<int>, IHasCreationTime
    {
        public int HoaDonId { get; set; }
        public virtual HoaDon? HoaDon { get; set; }
        public string MoTa {  get; set; }
        public double ChiPhi {  get; set; }
        public DateTime CreateTime { get; set; }
    }
}
