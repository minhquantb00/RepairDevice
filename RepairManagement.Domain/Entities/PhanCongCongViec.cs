using RepairManagement.Commons.Base;
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
        public int ThietBiId { get; set; }
        public virtual ThietBi? ThietBi { get; set; }
        public DateTime ThoiGianPhanCong {  get; set; }
        public DateTime? ThoiGianHoanThanh {  get; set; }
        public string GhiChu {  get; set; }
        public string Status { get; set; }
    }
}
