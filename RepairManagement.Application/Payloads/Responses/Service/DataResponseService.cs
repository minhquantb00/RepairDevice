using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Responses.Service
{
    public class DataResponseService
    {
        public int Id { get; set; }
        public string TenDichVu { get; set; }
        public string MoTaDichVu { get; set; }
        public int SoLuotDanhGia { get; set; }
        public int DiemDichVuTrungBinh { get; set; }
        public IQueryable<DataResponseDanhGiaDichVu>? DataResponseDanhGiaDichVus { get; set; }
    }
}
