using RepairManagement.Commons.Base;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class HieuSuatNhanVien : BaseEntity<int>, IHasModificationTime
    {
        public int NguoiDungId { get; set; }
        public virtual NguoiDung? NguoiDung { get; set; }
        public int SoThietBiDaSua { get; set; } = 0;
        public int TongThoiGianXuLy {  get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
