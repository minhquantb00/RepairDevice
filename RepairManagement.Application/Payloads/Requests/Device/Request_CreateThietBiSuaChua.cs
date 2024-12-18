﻿using RepairManagement.Commons.Enums;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreateThietBiSuaChua
    {
        public int KhachHangId { get; set; }
        public int ThietBiId { get; set; }
        public string MoTaLoi { get; set; }
        public string GhiChuCuaKhachHang { get; set; }
    }
}