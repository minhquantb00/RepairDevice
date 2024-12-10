using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class LinhKien : BaseEntity<int>
    {
        public string TenLinhKien {  get; set; }
        public string LoaiLinhKien { get; set; }
        public double GiaNhap { get; set; }
        public double GiaBan { get; set; }
        public string MoTa {  get; set; }
        public string? ImageUrl { get; set; }
        public virtual ICollection<LinhKienSuaChuaThietBi>? LinhKienSuaChuaThietBis { get; set; }
        public virtual ICollection<HangTonKho>? HangTonKhos { get; set; }
        public virtual ICollection<XuatNhapKho>? XuatNhapKhos { get; set; }
    }
}
