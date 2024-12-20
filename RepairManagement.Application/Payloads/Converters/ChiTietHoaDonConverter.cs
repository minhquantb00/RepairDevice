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
    public class ChiTietHoaDonConverter
    {
        private readonly IRepository<ThietBiSuaChua> _repository;
        private readonly ThietBiSuaChuaConverter _thietBiConverter;
        public ChiTietHoaDonConverter(IRepository<ThietBiSuaChua> repository, ThietBiSuaChuaConverter thietBiSuaChua)
        {
            _repository = repository;
            _thietBiConverter = thietBiSuaChua;
        }
        public DataResponseChiTietHoaDon EntityToDTO(ChiTietHoaDon chiTietHoaDon)
        {
            var thietBi = _repository.GetByIdAsync(chiTietHoaDon.ThietBiSuaChuaId).Result;
            var query = _thietBiConverter.EntityToDTO(thietBi);
            return new DataResponseChiTietHoaDon
            {
                DataResponseThietBiSuaChua = query,
                Description = chiTietHoaDon.Description,
                UnitPrice = chiTietHoaDon.UnitPrice,
                Id = chiTietHoaDon.Id
            };
        }
    }
}
