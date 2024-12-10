using RepairManagement.Commons.Base;
using RepairManagement.Commons.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class RefreshToken : BaseEntity<int>, IHasCreationTime
    {
        public string Token { get; set; }
        public DateTime ExpiredTime {  get; set; }
        public DateTime CreateTime { get; set; }
        public int UserId { get; set; }
        public virtual NguoiDung? User { get; set; }
    }
}
