using RepairManagement.Commons.Base;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class NguoiDung : BaseEntity<int>, IActivatable, IHasCreationTime, IHasModificationTime
    {
        public string Email { get; set; }
        public string HoVaTen {  get; set; }
        public string MatKhau { get; set; }
        public string SoDienThoai { get; set; }
        public string? DiaChi { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public virtual ICollection<ConfirmEmail>? ConfirmEmails { get; set; }
        public virtual ICollection<HieuSuatNhanVien>? HieuSuatNhanViens { get; set; }
        public virtual ICollection<KhachHang>? KhachHangs { get; set; }
        public virtual ICollection<PhanCongCongViec>? PhanCongCongViecs { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; set; }
        public virtual ICollection<XuatNhapKho>? XuatNhapKhos { get; set; }
        
    }
}
