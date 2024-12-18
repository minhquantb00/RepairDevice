using RepairManagement.Application.Payloads.Responses.Device;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Converters
{
    public class LinhKienConverter
    {
        public DataResponseLinhKien EntityToDTO(LinhKien linhKien)
        {
            return new DataResponseLinhKien
            {
                GiaBan = linhKien.GiaBan,
                GiaNhap = linhKien.GiaNhap,
                Id = linhKien.Id,
                ImageUrl = linhKien.ImageUrl,
                LoaiLinhKien = linhKien.LoaiLinhKien,
                MoTa = linhKien.MoTa,
                TenLinhKien = linhKien.TenLinhKien
            };
        }
    }
}
