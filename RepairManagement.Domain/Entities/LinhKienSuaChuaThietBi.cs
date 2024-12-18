using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class LinhKienSuaChuaThietBi : BaseEntity<int>
    {
        public int LinhKienId { get; set; }
        public virtual LinhKien? LinhKien { get; set; }
        public int ThietBiSuaChuaId { get; set; }
        public virtual ThietBiSuaChua? ThietBiSuaChua { get; set; }
        public int? SoLuongDung { get; set; } = 0;
    }
}
