using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class LichSuSuaChua : BaseEntity<int>
    {
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public int ThietBiId { get; set; }
        public virtual ThietBi? ThietBi { get; set; }
        public string MoTaLoi { get; set; }
        public double Gia {  get; set; }
        public DateTime ThoiGianSua { get; set; }
        public string GhiChu { get; set; }
        public string Status { get; set; }
        public int NhanVienId { get; set; }
    }
}
