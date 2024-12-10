using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhGiaDichVus_ThietBi_ThietBiId",
                table: "DanhGiaDichVus");

            migrationBuilder.AlterColumn<int>(
                name: "ThietBiId",
                table: "DanhGiaDichVus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DichVuId",
                table: "DanhGiaDichVus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DichVus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTaDichVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuotDanhGia = table.Column<int>(type: "int", nullable: false),
                    DiemDichVuTrungBinh = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DanhGiaDichVus_DichVuId",
                table: "DanhGiaDichVus",
                column: "DichVuId");

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGiaDichVus_DichVus_DichVuId",
                table: "DanhGiaDichVus",
                column: "DichVuId",
                principalTable: "DichVus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGiaDichVus_ThietBi_ThietBiId",
                table: "DanhGiaDichVus",
                column: "ThietBiId",
                principalTable: "ThietBi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DanhGiaDichVus_DichVus_DichVuId",
                table: "DanhGiaDichVus");

            migrationBuilder.DropForeignKey(
                name: "FK_DanhGiaDichVus_ThietBi_ThietBiId",
                table: "DanhGiaDichVus");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropIndex(
                name: "IX_DanhGiaDichVus_DichVuId",
                table: "DanhGiaDichVus");

            migrationBuilder.DropColumn(
                name: "DichVuId",
                table: "DanhGiaDichVus");

            migrationBuilder.AlterColumn<int>(
                name: "ThietBiId",
                table: "DanhGiaDichVus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DanhGiaDichVus_ThietBi_ThietBiId",
                table: "DanhGiaDichVus",
                column: "ThietBiId",
                principalTable: "ThietBi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
