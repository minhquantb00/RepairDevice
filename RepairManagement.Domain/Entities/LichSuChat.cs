using RepairManagement.Commons.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Domain.Entities
{
    public class LichSuChat : BaseEntity<int>
    {
        public int KhachHangId { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public string NoiDungKhachHang { get; set; }
        public string NoiDungChatBot {  get; set; }
        public bool ChuyenChoNhanVien {  get; set; } = false;
    }
}
