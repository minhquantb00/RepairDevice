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
    public class LichSuSuaChuaConverter
    {
        private readonly IRepository<NguoiDung> _nguoiDungRepository;
        private readonly IRepository<ThietBi> _thietBiRepository;
        public LichSuSuaChuaConverter(IRepository<NguoiDung> nguoiDungRepository, IRepository<ThietBi> thietBiRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _thietBiRepository = thietBiRepository;
        }
        public DataResponseLichSuSuaChua EntityToDTO(LichSuSuaChua lichSuSuaChua)
        {
            return new DataResponseLichSuSuaChua
            {
                GhiChu = lichSuSuaChua.GhiChu,
                Gia = lichSuSuaChua.Gia,
                MoTaLoi = lichSuSuaChua.MoTaLoi,
                NhanVienSua = _nguoiDungRepository.GetByIdAsync(lichSuSuaChua.NhanVienId).Result.HoVaTen,
                Status = lichSuSuaChua.Status,
                ThoiGianSua = lichSuSuaChua.ThoiGianSua,
                Id = lichSuSuaChua.Id,
                TenThietBi = _thietBiRepository.GetByIdAsync(lichSuSuaChua.ThietBiId).Result.TenThietBi,
            };
        }
    }
}
