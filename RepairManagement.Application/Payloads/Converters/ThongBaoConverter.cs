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
    public class ThongBaoConverter
    {
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly KhachHangConverter _khachHangConverter;
        public ThongBaoConverter(IRepository<KhachHang> khachHangRepository, KhachHangConverter khachHangConverter)
        {
            _khachHangConverter = khachHangConverter;
            _khachHangRepository = khachHangRepository;
        }
        public DataResponseThongBao EntityToDTO(ThongBao thongBao)
        {
            return new DataResponseThongBao
            {
                DataResponseKhachHang = _khachHangConverter.EntityToDTO(_khachHangRepository.GetAsync(item => item.Id == thongBao.KhachHangId).Result),
                DaXem = thongBao.DaXem,
                Id = thongBao.Id,
                NoiDung = thongBao.NoiDung,
                ThoiGianGui = thongBao.ThoiGianGui
            };
        }
    }
}
