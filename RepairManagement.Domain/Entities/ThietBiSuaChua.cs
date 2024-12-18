using RepairManagement.Commons.Base;
using RepairManagement.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class ThietBiSuaChua : BaseEntity<int>
    {
        public string TenThietBiSuaChua { get; set; }
        public int ThietBiId { get; set; }
        public virtual ThietBi? ThietBi { get; set; }
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set;}
        public string MoTaLoi { get; set; }
        public DateTime ThoiGianNhanSua { get; set; }
        public DateTime? ThoiGianGiaoHang { get; set; }
        public DateTime ThoiGianDuKien { get; set; }
        public DateTime?  ThoiGianThucTe { get; set; }
        public DateTime ThoiGianBaoHanh { get; set; }
        public string GhiChuCuaKhachHang { get; set;}
        public Enumerate.ThietBiSuaChuaStatus Status { get; set; } = Enumerate.ThietBiSuaChuaStatus.ChuaSua;
        public virtual ICollection<LinhKienSuaChuaThietBi>? LinhKienSuaChuaThietBis { get; set; }
        public virtual ICollection<PhanCongCongViec>? PhanCongCongViecs { get; set; }
    }
}
