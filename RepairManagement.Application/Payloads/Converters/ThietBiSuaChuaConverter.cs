using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class ThietBiSuaChuaConverter
    {
        private readonly IRepository<LinhKienSuaChuaThietBi> _linhKienSuaChua;
        private readonly LinhKienSuaChuaConverter _linhKienSuaChuaConverter;
        public ThietBiSuaChuaConverter( IRepository<LinhKienSuaChuaThietBi> linhKienSuaChua, LinhKienSuaChuaConverter linhKienSuaChuaConverter)
        {
            _linhKienSuaChua = linhKienSuaChua;
            _linhKienSuaChuaConverter = linhKienSuaChuaConverter;
        }
        public DataResponseThietBiSuaChua EntityToDTO(ThietBiSuaChua thietBiSuaChua)
        {
            return new DataResponseThietBiSuaChua
            {
                GhiChuCuaKhachHang = thietBiSuaChua.GhiChuCuaKhachHang,
                MoTaLoi = thietBiSuaChua.MoTaLoi,
                Status = thietBiSuaChua.Status.ToString(),
                TenThietBiSuaChua = thietBiSuaChua.TenThietBiSuaChua,
                ThoiGianBaoHanh = thietBiSuaChua.ThoiGianBaoHanh,
                ThoiGianDuKien = thietBiSuaChua.ThoiGianDuKien,
                ThoiGianGiaoHang = thietBiSuaChua.ThoiGianGiaoHang,
                ThoiGianNhanSua = thietBiSuaChua.ThoiGianNhanSua,
                ThoiGianThucTe = thietBiSuaChua.ThoiGianThucTe,
                Id = thietBiSuaChua.Id,
                DataResponseLinhKienSuaChuas = _linhKienSuaChua.GetAllAsync(x => x.ThietBiSuaChuaId == thietBiSuaChua.Id).Result.Select(item => _linhKienSuaChuaConverter.EntityToDTO(item)),
            };
        }
    }
}
