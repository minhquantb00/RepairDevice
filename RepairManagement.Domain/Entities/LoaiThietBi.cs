using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class LoaiThietBi : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<ThietBi>? ThietBis {  get; set; } 
    }
}
