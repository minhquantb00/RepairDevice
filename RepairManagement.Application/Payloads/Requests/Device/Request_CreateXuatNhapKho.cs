using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreateXuatNhapKho
    {
        public int LinhKienId { get; set; }
        [DataType(DataType.Upload)]
        public Enumerate.LoaiGiaoDichEnum? LoaiGiaoDich { get; set; }
        public int SoLuong { get; set; }
        public int NhanVienId { get; set; }
        public string GhiChu { get; set; }
    }
}
