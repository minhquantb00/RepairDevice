using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inivv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
                table: "ChiTietHoaDons");

            migrationBuilder.AlterColumn<int>(
                name: "ThietBiId",
                table: "ChiTietHoaDons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ThietBiSuaChuaId",
                table: "ChiTietHoaDons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
                table: "ChiTietHoaDons",
                column: "ThietBiId",
                principalTable: "ThietBi",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
                table: "ChiTietHoaDons");

            migrationBuilder.DropColumn(
                name: "ThietBiSuaChuaId",
                table: "ChiTietHoaDons");

            migrationBuilder.AlterColumn<int>(
                name: "ThietBiId",
                table: "ChiTietHoaDons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
                table: "ChiTietHoaDons",
                column: "ThietBiId",
                principalTable: "ThietBi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
