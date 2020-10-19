using Microsoft.EntityFrameworkCore.Migrations;

namespace ALD.LibFiscalCode.Persistence.Migrations.SqlServerData
{
    public partial class ProvinceFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_province_mapping_region_mapping_region_id",
                table: "province_mapping");

            migrationBuilder.DropIndex(
                name: "IX_province_mapping_region_id",
                table: "province_mapping");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "province_mapping");

            migrationBuilder.AddColumn<int>(
                name: "RegionId",
                table: "province_mapping",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 1,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 2,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 3,
                column: "RegionId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 4,
                column: "RegionId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 5,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 6,
                column: "RegionId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 7,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 8,
                column: "RegionId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 9,
                column: "RegionId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 10,
                column: "RegionId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 11,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 12,
                column: "RegionId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 13,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 14,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 15,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 16,
                column: "RegionId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 17,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 18,
                column: "RegionId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 19,
                column: "RegionId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 20,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 21,
                column: "RegionId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 22,
                column: "RegionId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 23,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 24,
                column: "RegionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 25,
                column: "RegionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 26,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 27,
                column: "RegionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 28,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 29,
                column: "RegionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 30,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 31,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 32,
                column: "RegionId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 33,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 34,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 35,
                column: "RegionId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 36,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 37,
                column: "RegionId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 38,
                column: "RegionId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 39,
                column: "RegionId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 40,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 41,
                column: "RegionId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 42,
                column: "RegionId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 43,
                column: "RegionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 44,
                column: "RegionId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 45,
                column: "RegionId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 46,
                column: "RegionId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 47,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 48,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 49,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 50,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 51,
                column: "RegionId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 52,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 53,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 54,
                column: "RegionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 55,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 56,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 57,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 58,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 59,
                column: "RegionId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 60,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 61,
                column: "RegionId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 62,
                column: "RegionId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 63,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 64,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 65,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 66,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 67,
                column: "RegionId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 68,
                column: "RegionId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 69,
                column: "RegionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 70,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 71,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 72,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 73,
                column: "RegionId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 74,
                column: "RegionId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 75,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 76,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 77,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 78,
                column: "RegionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 79,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 80,
                column: "RegionId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 81,
                column: "RegionId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 82,
                column: "RegionId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 83,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 84,
                column: "RegionId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 85,
                column: "RegionId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 86,
                column: "RegionId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 87,
                column: "RegionId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 88,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 89,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 90,
                column: "RegionId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 91,
                column: "RegionId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 92,
                column: "RegionId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 93,
                column: "RegionId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 94,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 95,
                column: "RegionId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 96,
                column: "RegionId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 97,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 98,
                column: "RegionId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 99,
                column: "RegionId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 100,
                column: "RegionId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 101,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 102,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 103,
                column: "RegionId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 104,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 105,
                column: "RegionId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 106,
                column: "RegionId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 107,
                column: "RegionId",
                value: 7);

            migrationBuilder.CreateIndex(
                name: "IX_province_mapping_RegionId",
                table: "province_mapping",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_province_mapping_region_mapping_RegionId",
                table: "province_mapping",
                column: "RegionId",
                principalTable: "region_mapping",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_province_mapping_region_mapping_RegionId",
                table: "province_mapping");

            migrationBuilder.DropIndex(
                name: "IX_province_mapping_RegionId",
                table: "province_mapping");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "province_mapping");

            migrationBuilder.AddColumn<int>(
                name: "region_id",
                table: "province_mapping",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_province_mapping_region_id",
                table: "province_mapping",
                column: "region_id");

            migrationBuilder.AddForeignKey(
                name: "FK_province_mapping_region_mapping_region_id",
                table: "province_mapping",
                column: "region_id",
                principalTable: "region_mapping",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
