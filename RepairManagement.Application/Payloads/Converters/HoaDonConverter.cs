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
    public class HoaDonConverter
    {
        private readonly KhachHangConverter _khachHangConverter;
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly IRepository<ChiTietHoaDon> _chiTietHoaDonRepository;
        private readonly ChiTietHoaDonConverter _chiTietHoaDonConverter;
        public HoaDonConverter(KhachHangConverter khachHangConverter, IRepository<KhachHang> khachHangRepository, IRepository<ChiTietHoaDon> chiTietHoaDonRepository, ChiTietHoaDonConverter chiTietHoaDonConverter)
        {
            _chiTietHoaDonConverter = chiTietHoaDonConverter;
            _chiTietHoaDonRepository = chiTietHoaDonRepository;
            _khachHangConverter = khachHangConverter;
            _khachHangRepository = khachHangRepository;
        }
        public DataResponseHoaDon EntityToDTO(HoaDon hoaDon)
        {
            return new DataResponseHoaDon
            {
                BillStatus = hoaDon.BillStatus.ToString(),
                CreateTime = hoaDon.CreateTime,
                Id= hoaDon.Id,
                PayTime= hoaDon.PayTime,
                TongTien = hoaDon.TongTien,
                ChiTietHoaDons = _chiTietHoaDonRepository.GetAllAsync(item => item.HoaDonId == hoaDon.Id).Result.Select(item => _chiTietHoaDonConverter.EntityToDTO(item)),
                DataResponseKhachHang = _khachHangConverter.EntityToDTO(_khachHangRepository.GetAsync(item => item.Id == hoaDon.KhachHangId).Result)
            };
        }
    }
}
