using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class DichVu : BaseEntity<int>
    {
        public string TenDichVu {  get; set; }
        public string MoTaDichVu { get; set; }
        public int SoLuotDanhGia {  get; set; }
        public int DiemDichVuTrungBinh {  get; set; }
        public virtual ICollection<DanhGiaDichVu>? DanhGiaDichVus {  get; set; }
    }
}
