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
        private readonly IRepository<ThietBi> _repository;
        private readonly IRepository<LinhKien> _linhKien;
        public ThietBiSuaChuaConverter( IRepository<LinhKienSuaChuaThietBi> linhKienSuaChua, LinhKienSuaChuaConverter linhKienSuaChuaConverter, IRepository<ThietBi> repository, IRepository<LinhKien> linhKien)
        {
            _linhKienSuaChua = linhKienSuaChua;
            _linhKienSuaChuaConverter = linhKienSuaChuaConverter;
            _repository = repository;
            _linhKien = linhKien;
        }
        public DataResponseThietBiSuaChua EntityToDTO(ThietBiSuaChua thietBiSuaChua)
        {
            var linhKienSuaChua = _linhKienSuaChua.GetAllAsync(item => item.ThietBiSuaChuaId == thietBiSuaChua.Id).Result;
            double? tongTien = 0;
            foreach(var item in linhKienSuaChua)
            {
                var linhKien = _linhKien.GetAsync(x => x.Id == item.LinhKienId).Result;
                tongTien += (double)linhKien.GiaBan * item.SoLuongDung;
            }

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
                TongTien =(double) tongTien,
                DataResponseLinhKienSuaChuas = _linhKienSuaChua.GetAllAsync(x => x.ThietBiSuaChuaId == thietBiSuaChua.Id).Result.Select(item => _linhKienSuaChuaConverter.EntityToDTO(item)),
                AnhThietBi = _repository.GetByIdAsync(thietBiSuaChua.ThietBiId).Result.ImageUrl
            };
        }
    }
}
