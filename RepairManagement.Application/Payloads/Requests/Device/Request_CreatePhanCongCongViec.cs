﻿using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Application.Payloads.Requests.Device
{
    public class Request_CreatePhanCongCongViec
    {
        public int NguoiDungId { get; set; }
        public int ThietBiSuaChuaId { get; set; }
        public string GhiChu { get; set; }
    }
}