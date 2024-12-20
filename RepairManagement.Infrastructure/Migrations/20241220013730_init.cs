using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "DichVus",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MoTaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SoLuotDanhGia = table.Column<int>(type: "int", nullable: false),
            //        DiemDichVuTrungBinh = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DichVus", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "FAQs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CauHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CauTraLoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FAQs", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LinhKiens",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TenLinhKien = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LoaiLinhKien = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        GiaNhap = table.Column<double>(type: "float", nullable: true),
            //        GiaBan = table.Column<double>(type: "float", nullable: false),
            //        MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LinhKiens", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LoaiThietBis",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LoaiThietBis", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "NguoiDungs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_NguoiDungs", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Roles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Roles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HangTonKhos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LinhKienId = table.Column<int>(type: "int", nullable: false),
            //        SoLuong = table.Column<int>(type: "int", nullable: false),
            //        WarningLevel = table.Column<int>(type: "int", nullable: false),
            //        UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HangTonKhos", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_HangTonKhos_LinhKiens_LinhKienId",
            //            column: x => x.LinhKienId,
            //            principalTable: "LinhKiens",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ConfirmEmails",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ConfirmCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsConfirm = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ConfirmEmails", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ConfirmEmails_NguoiDungs_UserId",
            //            column: x => x.UserId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HieuSuatNhanViens",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        NguoiDungId = table.Column<int>(type: "int", nullable: false),
            //        SoThietBiDaSua = table.Column<int>(type: "int", nullable: false),
            //        TongThoiGianXuLy = table.Column<int>(type: "int", nullable: false),
            //        UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HieuSuatNhanViens", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_HieuSuatNhanViens_NguoiDungs_NguoiDungId",
            //            column: x => x.NguoiDungId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "KhachHangs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        NguoiDungId = table.Column<int>(type: "int", nullable: true),
            //        Diem = table.Column<int>(type: "int", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_KhachHangs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_KhachHangs_NguoiDungs_NguoiDungId",
            //            column: x => x.NguoiDungId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "RefreshTokens",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ExpiredTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        UserId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_RefreshTokens", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_RefreshTokens_NguoiDungs_UserId",
            //            column: x => x.UserId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "XuatNhapKhos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LinhKienId = table.Column<int>(type: "int", nullable: false),
            //        LoaiGiaoDich = table.Column<int>(type: "int", nullable: true),
            //        SoLuong = table.Column<int>(type: "int", nullable: false),
            //        ThoiGianGiaoDich = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        NhanVienId = table.Column<int>(type: "int", nullable: false),
            //        GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_XuatNhapKhos", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_XuatNhapKhos_LinhKiens_LinhKienId",
            //            column: x => x.LinhKienId,
            //            principalTable: "LinhKiens",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_XuatNhapKhos_NguoiDungs_NhanVienId",
            //            column: x => x.NhanVienId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "UserRoles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        NguoiDungId = table.Column<int>(type: "int", nullable: false),
            //        RoleId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UserRoles", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_UserRoles_NguoiDungs_NguoiDungId",
            //            column: x => x.NguoiDungId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_UserRoles_Roles_RoleId",
            //            column: x => x.RoleId,
            //            principalTable: "Roles",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DatLichSuaChuas",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KhachHangId = table.Column<int>(type: "int", nullable: true),
            //        HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DichVuId = table.Column<int>(type: "int", nullable: false),
            //        ThoiGianDat = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TenThietBi = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DatLichSuaChuas", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_DatLichSuaChuas_DichVus_DichVuId",
            //            column: x => x.DichVuId,
            //            principalTable: "DichVus",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_DatLichSuaChuas_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "HoaDons",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        TongTien = table.Column<double>(type: "float", nullable: false),
            //        BillStatus = table.Column<int>(type: "int", nullable: true),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        PayTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_HoaDons", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_HoaDons_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LichSuChats",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        NoiDungKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        NoiDungChatBot = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ChuyenChoNhanVien = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LichSuChats", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LichSuChats_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LichSuTichDiems",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        Point = table.Column<int>(type: "int", nullable: false),
            //        Action = table.Column<int>(type: "int", nullable: false),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LichSuTichDiems", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LichSuTichDiems_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ThietBi",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TenThietBi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        LoaiThietBiId = table.Column<int>(type: "int", nullable: false),
            //        KhachHangId = table.Column<int>(type: "int", nullable: true),
            //        Gia = table.Column<double>(type: "float", nullable: true),
            //        MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ThietBi", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ThietBi_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_ThietBi_LoaiThietBis_LoaiThietBiId",
            //            column: x => x.LoaiThietBiId,
            //            principalTable: "LoaiThietBis",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ThongBaos",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ThoiGianGui = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        DaXem = table.Column<bool>(type: "bit", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ThongBaos", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ThongBaos_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ChiPhiPhatSinhs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        HoaDonId = table.Column<int>(type: "int", nullable: false),
            //        MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ChiPhi = table.Column<double>(type: "float", nullable: false),
            //        CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ChiPhiPhatSinhs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ChiPhiPhatSinhs_HoaDons_HoaDonId",
            //            column: x => x.HoaDonId,
            //            principalTable: "HoaDons",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BaoHanhs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        ThietBiId = table.Column<int>(type: "int", nullable: false),
            //        ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BaoHanhs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_BaoHanhs_ThietBi_ThietBiId",
            //            column: x => x.ThietBiId,
            //            principalTable: "ThietBi",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DanhGiaDichVus",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DichVuId = table.Column<int>(type: "int", nullable: false),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        DiemDichVu = table.Column<int>(type: "int", nullable: false),
            //        DiemChamSocKhachHang = table.Column<int>(type: "int", nullable: false),
            //        PhanHoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ThoiGianDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ThietBiId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DanhGiaDichVus", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_DanhGiaDichVus_DichVus_DichVuId",
            //            column: x => x.DichVuId,
            //            principalTable: "DichVus",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_DanhGiaDichVus_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_DanhGiaDichVus_ThietBi_ThietBiId",
            //            column: x => x.ThietBiId,
            //            principalTable: "ThietBi",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LichSuSuaChuas",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        ThietBiId = table.Column<int>(type: "int", nullable: false),
            //        MoTaLoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Gia = table.Column<double>(type: "float", nullable: false),
            //        ThoiGianSua = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        NhanVienId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LichSuSuaChuas", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LichSuSuaChuas_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LichSuSuaChuas_ThietBi_ThietBiId",
            //            column: x => x.ThietBiId,
            //            principalTable: "ThietBi",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ThietBiSuaChuas",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        TenThietBiSuaChua = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ThietBiId = table.Column<int>(type: "int", nullable: false),
            //        KhachHangId = table.Column<int>(type: "int", nullable: false),
            //        MoTaLoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ThoiGianNhanSua = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ThoiGianGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ThoiGianDuKien = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ThoiGianThucTe = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        ThoiGianBaoHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        GhiChuCuaKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ThietBiSuaChuas", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ThietBiSuaChuas_KhachHangs_KhachHangId",
            //            column: x => x.KhachHangId,
            //            principalTable: "KhachHangs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ThietBiSuaChuas_ThietBi_ThietBiId",
            //            column: x => x.ThietBiId,
            //            principalTable: "ThietBi",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ChiTietHoaDons",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        HoaDonId = table.Column<int>(type: "int", nullable: false),
            //        ThietBiSuaChuaId = table.Column<int>(type: "int", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        UnitPrice = table.Column<double>(type: "float", nullable: false),
            //        ThietBiId = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ChiTietHoaDons", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ChiTietHoaDons_HoaDons_HoaDonId",
            //            column: x => x.HoaDonId,
            //            principalTable: "HoaDons",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ChiTietHoaDons_ThietBiSuaChuas_ThietBiSuaChuaId",
            //            column: x => x.ThietBiSuaChuaId,
            //            principalTable: "ThietBiSuaChuas",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
            //            column: x => x.ThietBiId,
            //            principalTable: "ThietBi",
            //            principalColumn: "Id");
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LinhKienSuaChuaThietBis",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LinhKienId = table.Column<int>(type: "int", nullable: false),
            //        ThietBiSuaChuaId = table.Column<int>(type: "int", nullable: false),
            //        SoLuongDung = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LinhKienSuaChuaThietBis", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_LinhKienSuaChuaThietBis_LinhKiens_LinhKienId",
            //            column: x => x.LinhKienId,
            //            principalTable: "LinhKiens",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_LinhKienSuaChuaThietBis_ThietBiSuaChuas_ThietBiSuaChuaId",
            //            column: x => x.ThietBiSuaChuaId,
            //            principalTable: "ThietBiSuaChuas",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PhanCongCongViecs",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        NguoiDungId = table.Column<int>(type: "int", nullable: false),
            //        ThietBiSuaChuaId = table.Column<int>(type: "int", nullable: false),
            //        ThoiGianPhanCong = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ThoiGianHoanThanh = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Status = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PhanCongCongViecs", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_PhanCongCongViecs_NguoiDungs_NguoiDungId",
            //            column: x => x.NguoiDungId,
            //            principalTable: "NguoiDungs",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_PhanCongCongViecs_ThietBiSuaChuas_ThietBiSuaChuaId",
            //            column: x => x.ThietBiSuaChuaId,
            //            principalTable: "ThietBiSuaChuas",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_BaoHanhs_ThietBiId",
            //    table: "BaoHanhs",
            //    column: "ThietBiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ChiPhiPhatSinhs_HoaDonId",
            //    table: "ChiPhiPhatSinhs",
            //    column: "HoaDonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ChiTietHoaDons_HoaDonId",
            //    table: "ChiTietHoaDons",
            //    column: "HoaDonId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ChiTietHoaDons_ThietBiId",
            //    table: "ChiTietHoaDons",
            //    column: "ThietBiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ChiTietHoaDons_ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons",
            //    column: "ThietBiSuaChuaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ConfirmEmails_UserId",
            //    table: "ConfirmEmails",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DanhGiaDichVus_DichVuId",
            //    table: "DanhGiaDichVus",
            //    column: "DichVuId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DanhGiaDichVus_KhachHangId",
            //    table: "DanhGiaDichVus",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DanhGiaDichVus_ThietBiId",
            //    table: "DanhGiaDichVus",
            //    column: "ThietBiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DatLichSuaChuas_DichVuId",
            //    table: "DatLichSuaChuas",
            //    column: "DichVuId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DatLichSuaChuas_KhachHangId",
            //    table: "DatLichSuaChuas",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_HangTonKhos_LinhKienId",
            //    table: "HangTonKhos",
            //    column: "LinhKienId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_HieuSuatNhanViens_NguoiDungId",
            //    table: "HieuSuatNhanViens",
            //    column: "NguoiDungId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_HoaDons_KhachHangId",
            //    table: "HoaDons",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_KhachHangs_NguoiDungId",
            //    table: "KhachHangs",
            //    column: "NguoiDungId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LichSuChats_KhachHangId",
            //    table: "LichSuChats",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LichSuSuaChuas_KhachHangId",
            //    table: "LichSuSuaChuas",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LichSuSuaChuas_ThietBiId",
            //    table: "LichSuSuaChuas",
            //    column: "ThietBiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LichSuTichDiems_KhachHangId",
            //    table: "LichSuTichDiems",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LinhKienSuaChuaThietBis_LinhKienId",
            //    table: "LinhKienSuaChuaThietBis",
            //    column: "LinhKienId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_LinhKienSuaChuaThietBis_ThietBiSuaChuaId",
            //    table: "LinhKienSuaChuaThietBis",
            //    column: "ThietBiSuaChuaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PhanCongCongViecs_NguoiDungId",
            //    table: "PhanCongCongViecs",
            //    column: "NguoiDungId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_PhanCongCongViecs_ThietBiSuaChuaId",
            //    table: "PhanCongCongViecs",
            //    column: "ThietBiSuaChuaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_RefreshTokens_UserId",
            //    table: "RefreshTokens",
            //    column: "UserId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThietBi_KhachHangId",
            //    table: "ThietBi",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThietBi_LoaiThietBiId",
            //    table: "ThietBi",
            //    column: "LoaiThietBiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThietBiSuaChuas_KhachHangId",
            //    table: "ThietBiSuaChuas",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThietBiSuaChuas_ThietBiId",
            //    table: "ThietBiSuaChuas",
            //    column: "ThietBiId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ThongBaos_KhachHangId",
            //    table: "ThongBaos",
            //    column: "KhachHangId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoles_NguoiDungId",
            //    table: "UserRoles",
            //    column: "NguoiDungId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserRoles_RoleId",
            //    table: "UserRoles",
            //    column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_XuatNhapKhos_LinhKienId",
            //    table: "XuatNhapKhos",
            //    column: "LinhKienId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_XuatNhapKhos_NhanVienId",
            //    table: "XuatNhapKhos",
            //    column: "NhanVienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "BaoHanhs");

            //migrationBuilder.DropTable(
            //    name: "ChiPhiPhatSinhs");

            //migrationBuilder.DropTable(
            //    name: "ChiTietHoaDons");

            //migrationBuilder.DropTable(
            //    name: "ConfirmEmails");

            //migrationBuilder.DropTable(
            //    name: "DanhGiaDichVus");

            //migrationBuilder.DropTable(
            //    name: "DatLichSuaChuas");

            //migrationBuilder.DropTable(
            //    name: "FAQs");

            //migrationBuilder.DropTable(
            //    name: "HangTonKhos");

            //migrationBuilder.DropTable(
            //    name: "HieuSuatNhanViens");

            //migrationBuilder.DropTable(
            //    name: "LichSuChats");

            //migrationBuilder.DropTable(
            //    name: "LichSuSuaChuas");

            //migrationBuilder.DropTable(
            //    name: "LichSuTichDiems");

            //migrationBuilder.DropTable(
            //    name: "LinhKienSuaChuaThietBis");

            //migrationBuilder.DropTable(
            //    name: "PhanCongCongViecs");

            //migrationBuilder.DropTable(
            //    name: "RefreshTokens");

            //migrationBuilder.DropTable(
            //    name: "ThongBaos");

            //migrationBuilder.DropTable(
            //    name: "UserRoles");

            //migrationBuilder.DropTable(
            //    name: "XuatNhapKhos");

            //migrationBuilder.DropTable(
            //    name: "HoaDons");

            //migrationBuilder.DropTable(
            //    name: "DichVus");

            //migrationBuilder.DropTable(
            //    name: "ThietBiSuaChuas");

            //migrationBuilder.DropTable(
            //    name: "Roles");

            //migrationBuilder.DropTable(
            //    name: "LinhKiens");

            //migrationBuilder.DropTable(
            //    name: "ThietBi");

            //migrationBuilder.DropTable(
            //    name: "KhachHangs");

            //migrationBuilder.DropTable(
            //    name: "LoaiThietBis");

            //migrationBuilder.DropTable(
            //    name: "NguoiDungs");
        }
    }
}
