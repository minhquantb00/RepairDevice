using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Device
{
    public class DataResponseThietBiSuaChua
    {
        public int Id { get; set; }
        public string TenThietBiSuaChua { get; set; }
        public string MoTaLoi { get; set; }
        public string AnhThietBi {  get; set; }
        public double TongTien { get; set; }
        public DateTime ThoiGianNhanSua { get; set; }
        public DateTime? ThoiGianGiaoHang { get; set; }
        public DateTime ThoiGianDuKien { get; set; }
        public DateTime? ThoiGianThucTe { get; set; }
        public DateTime ThoiGianBaoHanh { get; set; }
        public string GhiChuCuaKhachHang { get; set; }
        public string Status { get; set; } 
        public IQueryable<DataResponseLinhKienSuaChua> DataResponseLinhKienSuaChuas { get; set; }
    }
}
