using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ALD.LibFiscalCode.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    province_name = table.Column<string>(nullable: true),
                    province_abbreviation = table.Column<string>(nullable: true),
                    region_name = table.Column<string>(nullable: true),
                    code = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
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
            SeedSettings(migrationBuilder);
            SeedLanguages(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiscalCodes");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Places");
        }
        private void SeedSettings(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "AppLanguage", null, "1" });
            migrationBuilder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "MaxHistorySize", null, null });
            migrationBuilder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "DefaultDate", null, null });
            migrationBuilder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new string[] { "SplittingStrategy", "FAST", null });
        }

        private void SeedLanguages(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Languages", new string[] { "name", "iso_2_code", "iso_3_code", "icon_name" }, new string[] { "Italiano", "it", "ita", "Assets/it.png" });
            migrationBuilder.InsertData("Languages", new string[] { "name", "iso_2_code", "iso_3_code", "icon_name" }, new string[] { "English", "en", "eng", "Assets/us.png" });
        }

        private void SeedPlaces(MigrationBuilder migrationBuilder)
        {

        }
    }
}
