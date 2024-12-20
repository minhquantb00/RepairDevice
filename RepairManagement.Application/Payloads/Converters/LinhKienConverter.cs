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
    public class LinhKienConverter
    {
        private readonly IRepository<HangTonKho> _hangTonKhoRepository;
        public LinhKienConverter(IRepository<HangTonKho> hangTonKhoRepository)
        {
            _hangTonKhoRepository = hangTonKhoRepository;
        }

        public DataResponseLinhKien EntityToDTO(LinhKien linhKien)
        {
            return new DataResponseLinhKien
            {
                GiaBan = linhKien.GiaBan,
                GiaNhap = linhKien.GiaNhap ?? 0.0, // Xử lý trường hợp null
                Id = linhKien.Id,
                ImageUrl = linhKien.ImageUrl,
                LoaiLinhKien = linhKien.LoaiLinhKien,
                MoTa = linhKien.MoTa,
                TenLinhKien = linhKien.TenLinhKien,
                SoLuong = _hangTonKhoRepository.GetAsync(item => item.LinhKienId == linhKien.Id).Result.SoLuong,
            };
        }
    }
}
