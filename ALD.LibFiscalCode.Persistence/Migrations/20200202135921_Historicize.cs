using System;
using Microsoft.EntityFrameworkCore.Migrations;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Text;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;
using ALD.LibFiscalCode.Persistence.Importer;

namespace ALD.LibFiscalCode.Persistence.Migrations
{
    public partial class Historicize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(nullable: true),
                    iso_2_code = table.Column<string>(nullable: true),
                    iso_3_code = table.Column<string>(nullable: true),
                    icon_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    string_value = table.Column<string>(nullable: true),
                    int_value = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Windows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Windows", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    surname = table.Column<string>(nullable: true),
                    date_of_birth = table.Column<DateTime>(nullable: false),
                    place_of_birth_id = table.Column<int>(nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    fiscal_code_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.id);
                    table.ForeignKey(
                        name: "FK_People_Places_place_of_birth_id",
                        column: x => x.place_of_birth_id,
                        principalTable: "Places",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedStrings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    WindowId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalizedStrings_Windows_WindowId",
                        column: x => x.WindowId,
                        principalTable: "Windows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiscalCodes",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fiscal_code = table.Column<string>(nullable: true),
                    person_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalCodes", x => x.id);
                    table.ForeignKey(
                        name: "FK_FiscalCodes_People_person_id",
                        column: x => x.person_id,
                        principalTable: "People",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalCodes_person_id",
                table: "FiscalCodes",
                column: "person_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_LanguageId",
                table: "LocalizedStrings",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedStrings_WindowId",
                table: "LocalizedStrings",
                column: "WindowId");

            migrationBuilder.CreateIndex(
                name: "IX_People_place_of_birth_id",
                table: "People",
                column: "place_of_birth_id");

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
                name: "FiscalCodes");

            migrationBuilder.DropTable(
                name: "LocalizedStrings");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Windows");

            migrationBuilder.DropTable(
                name: "Places");
        }

        protected virtual void Seed(MigrationBuilder builder)
        {
            using var reader = new StreamReader(@"../ALD.LibFiscalCode.Persistence/Migrations/Places_201912281810.csv");
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

            //languages
            builder.InsertData("Languages", new string[] { "name", "iso_2_code", "iso_3_code", "icon_name" }, new string[] { "Italiano", "it", "ita", "Assets/it.png" });

            //settings
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "AppLanguage", null, "1" });
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "MaxHistorySize", null, "0" });
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "DefaultDate", null, null });
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "SplittingMethod", "FAST", null });
        }
    }
}