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
    public class ThietBiConverter
    {
        private readonly KhachHangConverter _khachHangConverter;
        private readonly IRepository<KhachHang> _khachHangRepository;


        public ThietBiConverter(KhachHangConverter khachHangConverter, IRepository<KhachHang> khachHangRepository)
        {
            _khachHangConverter = khachHangConverter;
            _khachHangRepository = khachHangRepository;
        }

        public DataResponseThietBi EntityToDTO(ThietBi entity)
        {
            var khachHang = (entity.KhachHangId != null && entity.KhachHangId != 0) ? _khachHangRepository.GetByIdAsync((int)entity.KhachHangId).Result : null;
            return new DataResponseThietBi
            {
                DataResponseKhachHang = khachHang != null ? _khachHangConverter.EntityToDTO(khachHang) : null,
                Id = entity.Id,
                ImageUrl = entity.ImageUrl,
                MoTa = entity.MoTa,
                Status = entity.Status.ToString(),
                TenThietBi = entity.TenThietBi,
                Gia = entity.Gia,
            };
        }
    }
}
