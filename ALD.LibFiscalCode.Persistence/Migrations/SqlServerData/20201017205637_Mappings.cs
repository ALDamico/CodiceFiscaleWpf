using System;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ALD.LibFiscalCode.Persistence.Migrations.SqlServerData
{
    public partial class Mappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvinceMapping",
                table: "ProvinceMapping");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "ProvinceMapping",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "region_id",
                table: "ProvinceMapping",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvinceMapping",
                table: "ProvinceMapping",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMapping_region_id",
                table: "ProvinceMapping",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_Places_end_date",
                table: "Places",
                column: "end_date");

            migrationBuilder.CreateIndex(
                name: "IX_Places_start_date",
                table: "Places",
                column: "start_date");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMapping_Regions_region_id",
                table: "ProvinceMapping",
                column: "region_id",
                principalTable: "Regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMapping_Regions_region_id",
                table: "ProvinceMapping");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvinceMapping",
                table: "ProvinceMapping");

            migrationBuilder.DropIndex(
                name: "IX_ProvinceMapping_region_id",
                table: "ProvinceMapping");

            migrationBuilder.DropIndex(
                name: "IX_Places_end_date",
                table: "Places");

            migrationBuilder.DropIndex(
                name: "IX_Places_start_date",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ProvinceMapping");

            migrationBuilder.DropColumn(
                name: "region_id",
                table: "ProvinceMapping");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvinceMapping",
                table: "ProvinceMapping",
                column: "abbreviation");

            migrationBuilder.CreateTable(
                name: "RegionMapping",
                columns: table => new
                {
                    province_abbreviation = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionMapping", x => x.province_abbreviation);
                });
        }

        protected virtual void Seed(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new NullReferenceException();
            }
            var regionColumns = new[] { "name"};
            migrationBuilder.InsertData(nameof(RegionMapping), regionColumns, new[] {"Valle d'Aosta"});
            migrationBuilder.InsertData(nameof(RegionMapping), regionColumns, new[] {"Piemonte"});
            migrationBuilder.InsertData(nameof(RegionMapping), regionColumns, new[] {"Liguria"});
        }
    }
}
