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
    public class HoaDon : BaseEntity<int>, IHasCreationTime
    {
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public double TongTien { get; set; } = 0;
        public Enumerate.BillStatus? BillStatus { get; set; } = Enumerate.BillStatus.ChuaThanhToan;
        public DateTime CreateTime { get; set; }
        public DateTime? PayTime { get; set; }
        public virtual ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
        public virtual ICollection<ChiPhiPhatSinh>? ChiPhiPhatSinhs { get; set; }
    }
}
