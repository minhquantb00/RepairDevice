using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class HieuSuatConverter
    {
        private readonly IRepository<NguoiDung> _repository;
        private readonly NguoiDungConverter _converter;
        public HieuSuatConverter(IRepository<NguoiDung> repository, NguoiDungConverter converter)
        {
            _repository = repository;
            _converter = converter;
        }
        public DataResponseHieuSuatNhanVien EntityToDTO(HieuSuatNhanVien hieuSuatNhanVien)
        {
            return new DataResponseHieuSuatNhanVien
            {
                Id = hieuSuatNhanVien.Id,
                DataResponseUser = _converter.EntityToDTO(_repository.GetByIdAsync(hieuSuatNhanVien.NguoiDungId).Result),
                SoThietBiDaSua = hieuSuatNhanVien.SoThietBiDaSua,
                TongThoiGianXuLy = hieuSuatNhanVien.TongThoiGianXuLy
            };
        } 
    }
}
