using ALD.LibFiscalCode.Persistence.Importer;
using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Globalization;
using System.IO;
using System.Text;

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
                    code = table.Column<string>(nullable: true)
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
            Seed(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Places");
        }

        protected virtual void Seed(MigrationBuilder builder)
        {
            using var reader = new StreamReader(@"./Migrations/Places_201912281810.csv");
            var configuration = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);
            configuration.Delimiter = ";";
            configuration.Escape = '"';
            configuration.Encoding = Encoding.UTF8;
            configuration.HeaderValidated = null;
            configuration.MissingFieldFound = null;
            configuration.RegisterClassMap(new PlaceMap());
            using var csv = new CsvReader(reader, configuration);

            //Places
            var records = csv.GetRecords<Place>();
            foreach (var line in records)
            {
                builder.InsertData("Places", new string[] { "name", "province_name", "province_abbreviation", "region_name", "code" }, new string[] { line.Name, line.Province, line.ProvinceAbbreviation, line.Region, line.Code });
            }

        }
    }
}
