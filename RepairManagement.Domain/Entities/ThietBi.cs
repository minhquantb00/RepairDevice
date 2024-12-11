using RepairManagement.Commons.Base;
using RepairManagement.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class ThietBi : BaseEntity<int>
    {
        public string TenThietBi {  get; set; }
        public string? ImageUrl { get; set; }
        public int LoaiThietBiId { get; set; }
        public virtual LoaiThietBi? LoaiThietBi { get; set; }
        public int? KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public double? Gia {  get; set; }
        public string MoTa {  get; set; }
        public Enumerate.TrangThaiThietBi? Status { get; set; }
        public virtual ICollection<BaoHanh>? BaoHanhs { get; set; }
        public virtual ICollection<ChiTietHoaDon>? ChiTietHoaDons { get; set; }
        public virtual ICollection<DanhGiaDichVu>? DanhGiaDichVus { get; set; }
        public virtual ICollection<LichSuSuaChua>? LichSuSuaChuas { get; set; }
        public virtual ICollection<PhanCongCongViec>? PhanCongCongViecs { get; set; }
        public virtual ICollection<ThietBiSuaChua>? ThietBiSuaChuas { get; set; }
    }
}
