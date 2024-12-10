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
    public class NguoiDungConverter
    {
        private readonly KhachHangConverter _khachHangConverter;
        private readonly IRepository<KhachHang> _repository;
        public NguoiDungConverter(KhachHangConverter knachHangConverter, IRepository<KhachHang> repository)
        {
            _khachHangConverter = knachHangConverter;
            _repository = repository;
        }
        public DataResponseUser EntityToDTO(NguoiDung nguoiDung)
        {
            return new DataResponseUser
            {
                CreateTime = nguoiDung.CreateTime,
                DataResponseKhachHangs = _repository.GetAllAsync(record => record.NguoiDungId == nguoiDung.Id).Result.Select(item => _khachHangConverter.EntityToDTO(item)),
                DiaChi = nguoiDung.DiaChi,
                Email = nguoiDung.Email,
                HoVaTen = nguoiDung.HoVaTen,
                Id = nguoiDung.Id,
                SoDienThoai = nguoiDung.SoDienThoai,
                UpdateTime = nguoiDung.UpdateTime,
            };
        }
    }
}
