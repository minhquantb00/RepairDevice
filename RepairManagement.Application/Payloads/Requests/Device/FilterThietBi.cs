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
    public class FilterThietBi
    {
        public string? TenThietBi { get; set; }
        public int? LoaiThietBiId { get; set; }
    }
}
