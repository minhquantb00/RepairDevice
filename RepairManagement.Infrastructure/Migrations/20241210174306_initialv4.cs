using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhachHangs_NguoiDungs_NguoiDungId",
                table: "KhachHangs");

            migrationBuilder.AlterColumn<int>(
                name: "NguoiDungId",
                table: "KhachHangs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KhachHangs_NguoiDungs_NguoiDungId",
                table: "KhachHangs",
                column: "NguoiDungId",
                principalTable: "NguoiDungs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhachHangs_NguoiDungs_NguoiDungId",
                table: "KhachHangs");

            migrationBuilder.AlterColumn<int>(
                name: "NguoiDungId",
                table: "KhachHangs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KhachHangs_NguoiDungs_NguoiDungId",
                table: "KhachHangs",
                column: "NguoiDungId",
                principalTable: "NguoiDungs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
