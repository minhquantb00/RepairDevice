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
        public LichSuSuaChuaConverter(IRepository<NguoiDung> nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
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
            };
        }
    }
}
