using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class ThongBao : BaseEntity<int>
    {
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public string NoiDung {  get; set; }
        public DateTime ThoiGianGui {  get; set; }
        public bool DaXem {  get; set; } = false;
    }
}
