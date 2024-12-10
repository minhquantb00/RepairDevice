using RepairManagement.Commons.Base;
using RepairManagement.Commons.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class XuatNhapKho : BaseEntity<int>
    { 
        public int LinhKienId { get; set; }
        public virtual LinhKien? LinhKien { get; set; }
        public Enumerate.LoaiGiaoDichEnum? LoaiGiaoDich {  get; set; }
        public int SoLuong {  get; set; }
        public DateTime ThoiGianGiaoDich { get; set; }
        public int NhanVienId { get; set; }
        public virtual NguoiDung? NhanVien { get; set; }
        public string GhiChu {  get; set; }
    }
}
