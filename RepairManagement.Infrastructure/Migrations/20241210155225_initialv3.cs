using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ThietBi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "LoaiThietBis",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "LinhKiens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DichVuId",
                table: "DatLichSuaChuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DatLichSuaChuas_DichVuId",
                table: "DatLichSuaChuas",
                column: "DichVuId");

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichSuaChuas_DichVus_DichVuId",
                table: "DatLichSuaChuas",
                column: "DichVuId",
                principalTable: "DichVus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatLichSuaChuas_DichVus_DichVuId",
                table: "DatLichSuaChuas");

            migrationBuilder.DropIndex(
                name: "IX_DatLichSuaChuas_DichVuId",
                table: "DatLichSuaChuas");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ThietBi");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "LoaiThietBis");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "LinhKiens");

            migrationBuilder.DropColumn(
                name: "DichVuId",
                table: "DatLichSuaChuas");
        }
    }
}
