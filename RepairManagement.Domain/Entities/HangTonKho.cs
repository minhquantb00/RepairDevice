using RepairManagement.Commons.Base;
using RepairManagement.Commons.Enums;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class HangTonKho : BaseEntity<int>, IHasModificationTime
    {
        public int LinhKienId { get; set; }
        public virtual LinhKien? LinhKien { get; set; }
        public int SoLuong { get; set; } = 0;
        public Enumerate.WarningLevelEnum WarningLevel { get; set; } = Enumerate.WarningLevelEnum.HetHang;
        public DateTime? UpdateTime { get; set; }
    }
}
