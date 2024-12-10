using RepairManagement.Commons.Base;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class KhachHang : BaseEntity<int>, IHasCreationTime, IHasModificationTime, IActivatable
    {
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public int Diem { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public virtual ICollection<DanhGiaDichVu>? DanhGiaDichVus {  get; set; }
        public virtual ICollection<DatLichSuaChua>? DatLichSuaChuas { get; set; }
        public virtual ICollection<HoaDon>? HoaDons { get; set; }
        public virtual ICollection<LichSuChat>? LichSuChats { get; set; }
        public virtual ICollection<LichSuSuaChua>? LichSuSuaChuas { get; set; }
        public virtual ICollection<LichSuTichDiem>? LichSuTichDiems { get; set; }
        public virtual ICollection<ThietBi>? ThietBis {  get; set; }
        public virtual ICollection<ThietBiSuaChua>? ThietBiSuaChuas { get; set; }
        public virtual ICollection<ThongBao>? ThongBaos { get; set; }
    }
}
