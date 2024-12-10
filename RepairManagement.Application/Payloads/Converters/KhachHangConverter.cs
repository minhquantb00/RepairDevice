using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class KhachHangConverter
    {
        public DataResponseKhachHang EntityToDTO(KhachHang khachHang)
        {
            return new DataResponseKhachHang
            {
                CreateTime = khachHang.CreateTime,
                DiaChi = khachHang.DiaChi,
                Diem = khachHang.Diem,
                Email = khachHang.Email,
                HoVaTen = khachHang.HoVaTen,
                Id = khachHang.Id,
                SoDienThoai = khachHang.SoDienThoai,
                UpdateTime = khachHang.UpdateTime,
            };
        }
    }
}
