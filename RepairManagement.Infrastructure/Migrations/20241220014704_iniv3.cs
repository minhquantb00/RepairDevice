using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class iniv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChiTietHoaDons_ThietBiSuaChuas_ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons");

            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
                table: "ChiTietHoaDons");

            //migrationBuilder.DropIndex(
            //    name: "IX_ChiTietHoaDons_ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons");

            //migrationBuilder.DropColumn(
            //    name: "ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.AddColumn<int>(
            //    name: "ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ChiTietHoaDons_ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons",
            //    column: "ThietBiSuaChuaId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ChiTietHoaDons_ThietBiSuaChuas_ThietBiSuaChuaId",
            //    table: "ChiTietHoaDons",
            //    column: "ThietBiSuaChuaId",
            //    principalTable: "ThietBiSuaChuas",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietHoaDons_ThietBi_ThietBiId",
                table: "ChiTietHoaDons",
                column: "ThietBiId",
                principalTable: "ThietBi",
                principalColumn: "Id");
        }
    }
}
