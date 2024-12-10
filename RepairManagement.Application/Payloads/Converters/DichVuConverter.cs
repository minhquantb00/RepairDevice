using RepairManagement.Application.Payloads.Responses.Service;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class DichVuConverter
    {
        private readonly IRepository<DanhGiaDichVu> _danhGiaDichVuRepository;
        private readonly DanhGiaDichVuConverter _danhGiaDichVuConverter;
        public DichVuConverter(DanhGiaDichVuConverter danhGiaDichVuConverter, IRepository<DanhGiaDichVu> danhGiaDichVuRepository)
        {
            _danhGiaDichVuConverter = danhGiaDichVuConverter;
            _danhGiaDichVuRepository = danhGiaDichVuRepository;
        }
        public DataResponseService EntityToDTO(DichVu dichVu)
        {
            return new DataResponseService
            {
                DataResponseDanhGiaDichVus = _danhGiaDichVuRepository.GetAllAsync(record => record.DichVuId == dichVu.Id).Result.Select(item => _danhGiaDichVuConverter.EntityToDTO(item)),
                DiemDichVuTrungBinh = dichVu.DiemDichVuTrungBinh,
                Id = dichVu.Id,
                MoTaDichVu = dichVu.MoTaDichVu,
                SoLuotDanhGia = dichVu.SoLuotDanhGia,
                TenDichVu = dichVu.TenDichVu
            };
        }
    }
}
