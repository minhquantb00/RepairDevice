using RepairManagement.Commons.Base;
using RepairManagement.Commons.Enums;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class LichSuTichDiem : BaseEntity<int>, IHasCreationTime
    {
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public int Point {  get; set; }
        public Enumerate.Action Action { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
