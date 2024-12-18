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
    public class XuatNhapKhoConverter
    {
        private readonly IRepository<NguoiDung> _nguoiDungRepository;
        private readonly IRepository<LinhKien> _linhKienRepository;
        public XuatNhapKhoConverter(IRepository<NguoiDung> nguoiDungRepository, IRepository<LinhKien> linhKienRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _linhKienRepository = linhKienRepository;
        }
        public DataResponseXuatNhapKho EntityToDTO(XuatNhapKho xuatNhapKho)
        {
            return new DataResponseXuatNhapKho
            {
                GhiChu = xuatNhapKho.GhiChu,
                LoaiGiaoDich = xuatNhapKho.LoaiGiaoDich.ToString(),
                SoLuong = xuatNhapKho.SoLuong,
                TenLinhKien = _linhKienRepository.GetAsync(item => item.Id == xuatNhapKho.LinhKienId).Result.TenLinhKien,
                TenNhanVien = _nguoiDungRepository.GetAsync(item => item.Id == xuatNhapKho.NhanVienId).Result.HoVaTen,
                ThoiGianGiaoDich = xuatNhapKho.ThoiGianGiaoDich
            };
        }
    }
}
