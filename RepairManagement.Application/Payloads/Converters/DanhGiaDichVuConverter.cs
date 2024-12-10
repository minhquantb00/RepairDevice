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
    public class DanhGiaDichVuConverter
    {
        private readonly KhachHangConverter _khachHangConverter;
        private readonly IRepository<KhachHang> _khachHangRepository;
        public DanhGiaDichVuConverter(KhachHangConverter khachHangConverter, IRepository<KhachHang> khachHangRepository)
        {
            _khachHangConverter = khachHangConverter;
            _khachHangRepository = khachHangRepository;
        }
        public DataResponseDanhGiaDichVu EntityToDTO(DanhGiaDichVu entity)
        {
            return new DataResponseDanhGiaDichVu
            {
                DataResponseKhachHang = _khachHangConverter.EntityToDTO(_khachHangRepository.GetByIdAsync(entity.KhachHangId).Result),
                DiemChamSocKhachHang = entity.DiemChamSocKhachHang,
                DiemDichVu = entity.DiemDichVu,
                Id = entity.Id,
                PhanHoi = entity.PhanHoi,
                ThoiGianDanhGia = entity.ThoiGianDanhGia,
            };
        }
    }
}
