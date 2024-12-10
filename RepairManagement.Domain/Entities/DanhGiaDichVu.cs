using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class DanhGiaDichVu : BaseEntity<int>
    {
        public int DichVuId { get; set; }
        public virtual DichVu? DichVu { get; set; }
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public int DiemDichVu {  get; set; }
        public int DiemChamSocKhachHang { get; set; }
        public string PhanHoi {  get; set; }
        public DateTime ThoiGianDanhGia {  get; set; }
    }
}
