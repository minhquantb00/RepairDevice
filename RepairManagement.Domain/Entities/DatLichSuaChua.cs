using RepairManagement.Commons.Base;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class DatLichSuaChua : BaseEntity<int>, IHasCreationTime
    {
        public int? KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public string HoVaTen { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public int DichVuId { get; set; }
        public virtual DichVu? DichVu { get; set; }
        public DateTime ThoiGianDat { get; set; }
        public DateTime CreateTime { get; set; }
        public string MoTa {  get; set; }
        public string TenThietBi { get; set; }
    }
}
