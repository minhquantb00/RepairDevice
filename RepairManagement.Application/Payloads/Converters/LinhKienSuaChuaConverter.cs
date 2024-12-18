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
    public class LinhKienSuaChuaConverter
    {
        private readonly IRepository<LinhKien> _linhKienRepository;
        private readonly LinhKienConverter _linhKienConverter;
        public LinhKienSuaChuaConverter(IRepository<LinhKien> linhKienRepository, LinhKienConverter linhKienConverter)
        {
            _linhKienRepository = linhKienRepository;
            _linhKienConverter = linhKienConverter;
        }

        public DataResponseLinhKienSuaChua EntityToDTO(LinhKienSuaChuaThietBi linhKienSuaChuaThietBi)
        {
            return new DataResponseLinhKienSuaChua
            {
                Id = linhKienSuaChuaThietBi.Id,
                DataResponseLinhKien= _linhKienConverter.EntityToDTO(_linhKienRepository.GetByIdAsync(linhKienSuaChuaThietBi.LinhKienId).Result),
                SoLuongDung = linhKienSuaChuaThietBi.SoLuongDung,
            };
        }
    }
}
