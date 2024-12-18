using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using RepairManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class LichSuTichDiemConverter
    {
        private readonly IRepository<KhachHang> _khachHangRepository;
        private readonly KhachHangConverter _khachHangConverter;
        public LichSuTichDiemConverter(IRepository<KhachHang> khachHangRepository, KhachHangConverter khachHangConverter)
        {
            _khachHangRepository = khachHangRepository;
            _khachHangConverter = khachHangConverter;
        }
        public DataResponseLichSuTichDiem EntityToDTO(LichSuTichDiem entity)
        {
            return new DataResponseLichSuTichDiem
            {
                Action = entity.Action.ToString(),
                CreateTime = entity.CreateTime,
                Point = entity.Point,
                DataResponseKhachHang = _khachHangConverter.EntityToDTO(_khachHangRepository.GetByIdAsync(entity.KhachHangId).Result),
                Id = entity.Id,
            };
        }
    }
}
