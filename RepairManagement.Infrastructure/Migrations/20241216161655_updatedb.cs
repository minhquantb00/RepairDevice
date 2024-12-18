using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepairManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongCongViecs_ThietBi_ThietBiId",
                table: "PhanCongCongViecs");

            migrationBuilder.RenameColumn(
                name: "ThietBiId",
                table: "PhanCongCongViecs",
                newName: "ThietBiSuaChuaId");

            migrationBuilder.RenameIndex(
                name: "IX_PhanCongCongViecs_ThietBiId",
                table: "PhanCongCongViecs",
                newName: "IX_PhanCongCongViecs_ThietBiSuaChuaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongCongViecs_ThietBiSuaChuas_ThietBiSuaChuaId",
                table: "PhanCongCongViecs",
                column: "ThietBiSuaChuaId",
                principalTable: "ThietBiSuaChuas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhanCongCongViecs_ThietBiSuaChuas_ThietBiSuaChuaId",
                table: "PhanCongCongViecs");

            migrationBuilder.RenameColumn(
                name: "ThietBiSuaChuaId",
                table: "PhanCongCongViecs",
                newName: "ThietBiId");

            migrationBuilder.RenameIndex(
                name: "IX_PhanCongCongViecs_ThietBiSuaChuaId",
                table: "PhanCongCongViecs",
                newName: "IX_PhanCongCongViecs_ThietBiId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhanCongCongViecs_ThietBi_ThietBiId",
                table: "PhanCongCongViecs",
                column: "ThietBiId",
                principalTable: "ThietBi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
