using RepairManagement.Commons.Base;
using RepairManagement.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class PhanCongCongViec : BaseEntity<int>
    {
        public int NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public int ThietBiSuaChuaId { get; set; }
        public virtual ThietBiSuaChua? ThietBiSuaChua { get; set; }
        public DateTime ThoiGianPhanCong {  get; set; }
        public DateTime? ThoiGianHoanThanh {  get; set; }
        public string GhiChu {  get; set; }
        public Enumerate.ThietBiSuaChuaStatus Status { get; set; } = Enumerate.ThietBiSuaChuaStatus.DangSua;
    }
}
