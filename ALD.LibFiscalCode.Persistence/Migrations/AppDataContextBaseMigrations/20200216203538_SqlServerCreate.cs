using ALD.LibFiscalCode.Persistence.Importer;
using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters;
using ALD.LibFiscalCode.Persistence.Importer.Models;
using CsvHelper.Configuration;

namespace ALD.LibFiscalCode.Persistence.Migrations.AppDataContextBaseMigrations
{
    public partial class SqlServerCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    province_name = table.Column<string>(nullable: true),
                    province_abbreviation = table.Column<string>(nullable: true),
                    region_name = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable:true),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Places_province_abbreviation",
                table: "Places",
                column: "province_abbreviation");

            migrationBuilder.CreateIndex(
                name: "IX_Places_region_name",
                table: "Places",
                column: "region_name");
            //Seed(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");
        }

        
    }
}
