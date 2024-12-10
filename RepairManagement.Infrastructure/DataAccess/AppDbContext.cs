using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using RepairManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairManagement.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<BaoHanh> BaoHanhs { get; set; }
        public DbSet<ChiPhiPhatSinh> ChiPhiPhatSinhs { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<ConfirmEmail> ConfirmEmails { get; set; }
        public DbSet<DanhGiaDichVu> DanhGiaDichVus {  get; set; }
        public DbSet<DatLichSuaChua> DatLichSuaChuas { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<HangTonKho> HangTonKhos { get; set; }
        public DbSet<HieuSuatNhanVien> HieuSuatNhanViens { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<LichSuChat> LichSuChats { get; set; }
        public DbSet<LichSuSuaChua> LichSuSuaChuas{ get; set; }
        public DbSet<LichSuTichDiem> LichSuTichDiems { get; set; }
        public DbSet<LinhKien> LinhKiens { get; set; }
        public DbSet<LinhKienSuaChuaThietBi> LinhKienSuaChuaThietBis {  get; set; }
        public DbSet<LoaiThietBi> LoaiThietBis{ get; set; }
        public DbSet<NguoiDung> NguoiDungs{ get; set; }
        public DbSet<PhanCongCongViec> PhanCongCongViecs { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ThietBi> ThietBi { get; set; }
        public DbSet<ThietBiSuaChua> ThietBiSuaChuas { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<XuatNhapKho> XuatNhapKhos { get; set; }
        public DbSet<DichVu> DichVus { get; set; }

        public async Task<int> CommitChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}
