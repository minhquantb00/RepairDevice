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
    public class LoaiThietBiConverter
    {
        private readonly ThietBiConverter _converter;
        private readonly IRepository<ThietBi> _thietBiRepository;
        public LoaiThietBiConverter(ThietBiConverter converter, IRepository<ThietBi> thietBiRepository)
        {
            _converter = converter;
            _thietBiRepository = thietBiRepository;
        }
        public DataResponseLoaiThietBi EntityToDTO(LoaiThietBi entity)
        {
            return new DataResponseLoaiThietBi
            {
                DataResponseThietBis = _thietBiRepository.GetAllAsync(record => record.LoaiThietBiId == entity.Id).Result.Select(item => _converter.EntityToDTO(item)),
                Id = entity.Id,
                ImageUrl = entity.ImageUrl,
                Name = entity.Name,
            };
        }
    }
}
