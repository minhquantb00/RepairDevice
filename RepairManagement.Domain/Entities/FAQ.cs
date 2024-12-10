using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class FAQ : BaseEntity<int>
    {
        public string CauHoi { get; set; }
        public string CauTraLoi { get; set; }
        public string Type {  get; set; }
    }
}
