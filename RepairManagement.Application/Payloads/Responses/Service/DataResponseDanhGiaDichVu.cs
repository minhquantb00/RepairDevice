using RepairManagement.Application.Payloads.Responses.User;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Service
{
    public class DataResponseDanhGiaDichVu
    {
        public int Id { get; set; }
        public DataResponseKhachHang DataResponseKhachHang { get; set; }
        public int DiemDichVu { get; set; }
        public int DiemChamSocKhachHang { get; set; }
        public string PhanHoi { get; set; }
        public DateTime ThoiGianDanhGia { get; set; }
    }
}
