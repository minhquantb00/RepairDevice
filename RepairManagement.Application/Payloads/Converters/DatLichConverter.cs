using RepairManagement.Application.Payloads.Responses.Booking;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class DatLichConverter
    {
        private readonly KhachHangConverter _khachHangConverter;
        private readonly DichVuConverter _dichVuConverter;
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly IRepository<DichVu> _dichVuRepository;
        public DatLichConverter(KhachHangConverter khachHangConverter, DichVuConverter dichVuConverter, IRepository<KhachHang> khachHangRepository, IRepository<DichVu> dichVuRepository)
        {
            _khachHangConverter = khachHangConverter;
            _dichVuConverter = dichVuConverter;
            _khachHangRepository = khachHangRepository;
            _dichVuRepository = dichVuRepository;
        }
        public DataResponseDatLich EntityToDTO(DatLichSuaChua datLichSuaChua)
        {
            var khachHang = (datLichSuaChua.KhachHangId != null && datLichSuaChua.KhachHangId != 0) ? _khachHangRepository.GetByIdAsync((int)datLichSuaChua.KhachHangId).Result : null;
            return new DataResponseDatLich
            {
                CreateTime = datLichSuaChua.CreateTime,
                DataResponseKhachHang = khachHang != null ?  _khachHangConverter.EntityToDTO(khachHang) : null,
                DataResponseService = _dichVuConverter.EntityToDTO(_dichVuRepository.GetByIdAsync(datLichSuaChua.DichVuId).Result),
                DiaChi = datLichSuaChua.DiaChi,
                Email = datLichSuaChua.Email,
                HoVaTen = datLichSuaChua.HoVaTen,
                Id = datLichSuaChua.Id,
                MoTa = datLichSuaChua.MoTa,
                SoDienThoai = datLichSuaChua.SoDienThoai,
                TenThietBi = datLichSuaChua.TenThietBi,
                ThoiGianDat = datLichSuaChua.ThoiGianDat,
            };
        }
    }
}
