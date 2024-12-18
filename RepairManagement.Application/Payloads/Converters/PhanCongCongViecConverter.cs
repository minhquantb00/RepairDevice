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
    public class PhanCongCongViecConverter
    {
        private readonly IRepository<ThietBiSuaChua> _thietBiSuaChuaRepository;
        private readonly ThietBiSuaChuaConverter _thietBiSuaChuaConverter;
        private readonly IRepository<NguoiDung> _nguoiDungRepository;
        private readonly NguoiDungConverter _nguoiDungConverter;
        public PhanCongCongViecConverter(IRepository<ThietBiSuaChua> thietBiSuaChuaRepository, ThietBiSuaChuaConverter thietBiSuaChuaConverter, IRepository<NguoiDung> nguoiDungRepository, NguoiDungConverter nguoiDungConverter)
        {
            _thietBiSuaChuaRepository = thietBiSuaChuaRepository;
            _thietBiSuaChuaConverter = thietBiSuaChuaConverter;
            _nguoiDungRepository = nguoiDungRepository;
            _nguoiDungConverter = nguoiDungConverter;
        }
        public DataResponsePhanCongCongViec EntityToDTO(PhanCongCongViec entity)
        {
            return new DataResponsePhanCongCongViec
            {
                GhiChu = entity.GhiChu,
                Id = entity.Id,
                Status = entity.Status,
                ThoiGianHoanThanh = entity.ThoiGianHoanThanh,
                ThoiGianPhanCong = entity.ThoiGianPhanCong,
                DataResponseThietBiSuaChua = _thietBiSuaChuaConverter.EntityToDTO(_thietBiSuaChuaRepository.GetByIdAsync(entity.ThietBiSuaChuaId).Result),
                NhanVien = _nguoiDungConverter.EntityToDTO(_nguoiDungRepository.GetByIdAsync(entity.NguoiDungId).Result),
            };
        }
    }
}
