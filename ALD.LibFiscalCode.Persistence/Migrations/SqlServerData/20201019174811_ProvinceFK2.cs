using Microsoft.EntityFrameworkCore.Migrations;

namespace ALD.LibFiscalCode.Persistence.Migrations.SqlServerData
{
    public partial class ProvinceFK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_province_mapping_region_mapping_RegionId",
                table: "province_mapping");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "province_mapping",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_province_mapping_region_mapping_RegionId",
                table: "province_mapping",
                column: "RegionId",
                principalTable: "region_mapping",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_province_mapping_region_mapping_RegionId",
                table: "province_mapping");

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "province_mapping",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_province_mapping_region_mapping_RegionId",
                table: "province_mapping",
                column: "RegionId",
                principalTable: "region_mapping",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
