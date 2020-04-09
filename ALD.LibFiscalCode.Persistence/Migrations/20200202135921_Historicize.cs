using System;
using Microsoft.EntityFrameworkCore.Migrations;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Text;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Importer;
using ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters;
using ALD.LibFiscalCode.Persistence.Importer.Models;
using System.Collections.Generic;
using System.Linq;
using CsvHelper.Configuration;

namespace ALD.LibFiscalCode.Persistence.Migrations
{
    public partial class PlaceDataInsertion : Migration
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
                }); ;

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
                    code = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime?>(nullable: true),
                    end_date = table.Column<DateTime?>(nullable: true)
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
            ImportItalianMunicipalities(builder);
            // Places => Foreign countries
            var foreignCountries = ReadForeignCountries();

            // Places => Former foreign countries
            var formerCountries = ReadFormerForeignCountries();
            formerCountries.Sort(new FormerForeignCountryComparer());

            foreach (var formerCountry in formerCountries)
            {
                var newCountry = formerCountry.ChildName switch
                {
                    "Kazakistan" => foreignCountries.FirstOrDefault(c =>
                        c.Name.Equals("Kazakhstan", StringComparison.InvariantCultureIgnoreCase)),
                    "Macedonia, Repubblica di" => foreignCountries.FirstOrDefault(c =>
                        c.Name.StartsWith("Macedonia", StringComparison.InvariantCultureIgnoreCase)),
                    _ => foreignCountries.FirstOrDefault(c =>
                        c.Name.Equals(formerCountry.ChildName, StringComparison.InvariantCultureIgnoreCase))
                };

                var eventDate = new DateTime(formerCountry.YearOccurred.GetValueOrDefault(), 1, 1);
                //Updates the start date of the old country
                newCountry.StartDate = eventDate;

                var oldCountry = new Place()
                {
                    Name = formerCountry.Name,
                    Code = formerCountry.AtCode != "n.d" ? formerCountry.AtCode : null,
                    EndDate = eventDate,
                    Province = "Stato estero",
                    Region = newCountry.Region,
                    ProvinceAbbreviation = "EE"
                };
                if (!foreignCountries.Contains(oldCountry, oldCountry.GetEqualityComparer()))
                {
                    foreignCountries.Add(oldCountry);
                }


            }

            foreach (var country in foreignCountries)
            {
                string endDateStr = null;
                string startDateStr = null;
                if (country.EndDate.GetValueOrDefault() != default(DateTime))
                {
                    endDateStr = country.EndDate.GetValueOrDefault().ToString("s", CultureInfo.InvariantCulture);
                }
                if (country.StartDate.GetValueOrDefault() != default(DateTime))
                {
                    startDateStr = country.StartDate.GetValueOrDefault().ToString("s", CultureInfo.InvariantCulture);
                }
                if (country.EndDate == default(DateTime))
                {
                    country.EndDate = null;
                }
                if (country.StartDate == default(DateTime))
                {
                    country.StartDate = null;
                }

                builder.InsertData("Places",
                    new string[] { "name", "province_name", "province_abbreviation", "region_name", "code", "start_date", "end_date" },
                    new object[] { country.Name, country.Province, country.ProvinceAbbreviation, country.Region, country.Code, startDateStr, endDateStr });
            }
            //languages
            builder.InsertData("Languages", new string[] { "name", "iso_2_code", "iso_3_code", "icon_name" }, new object[] { "Italiano", "it", "ita", "Assets/it.png" });

            //settings
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new object[] { "AppLanguage", null, "1" });
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new object[] { "MaxHistorySize", null, "0" });
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new object[] { "DefaultDate", null, null });
            builder.InsertData("Settings", new string[] { "name", "string_value", "int_value" }, new object[] { "SplittingMethod", "FAST", null });
        }

        private List<FormerForeignCountry> ReadFormerForeignCountries()
        {
            var formerForeignCountries = new List<FormerForeignCountry>();

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                //Escape = '"',
                Encoding = Encoding.ASCII,
                HeaderValidated = null,
                MissingFieldFound = null
            };
            configuration.RegisterClassMap(new FormerForeignCountryMap());
            var reader = new StreamReader(FormerForeignCountriesPath);
            using var csv = new CsvReader(reader, configuration);
            var records = csv.GetRecords<FormerForeignCountry>();
            var emptyRows = 0;
            
            foreach (var line in records)
            {
                if (line.GeographicEntityType == "S")
                {
                    formerForeignCountries.Add(line);
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(line.GeographicEntityType))
                    {
                        emptyRows++;
                    }
                }

                if (emptyRows >= 2)
                {
                    break;
                }
            }
            
            return formerForeignCountries;
        }

        private List<Place> ReadForeignCountries()
        {
            var foreignCountries = new List<Place>();
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                //Escape = '"',
                Encoding = Encoding.UTF8,
                HeaderValidated = null,
                MissingFieldFound = null
            };
            using var reader = new StreamReader(IstatForeignCountriesPath);
            using var csv = new CsvReader(reader, configuration);
            configuration.RegisterClassMap(new ForeignCountryMap());

            var foreignCountryRecords = csv.GetRecords<ForeignCountry>();

            foreach (var line in foreignCountryRecords)
            {
                // We're not interested in territories, only places.
                if (line.GeographicEntityType == "S")
                {
                    var newPlace = new Place()
                    {
                        Name = line.NameIt,
                        Region = line.ContinentNameIt,
                        Province = "Stato estero",
                        ProvinceAbbreviation = "EE",
                        Code = line.AtCode,
                        StartDate = null,
                        EndDate = null
                    };
                    foreignCountries.Add(newPlace);
                }
            }
            return foreignCountries;
        }

        private void ImportItalianMunicipalities(MigrationBuilder builder)
        {
            using var reader = new StreamReader(AnprFilePath);
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Escape = '"',
                Encoding = Encoding.UTF8,
                HeaderValidated = null,
                MissingFieldFound = null
            };
            configuration.RegisterClassMap(new AnprPlaceMap());
            using var csv = new CsvReader(reader, configuration);

            //Places => Italian Municipalities
            var records = csv.GetRecords<Place>();
            foreach (var line in records)
            {
                string endDateStr = null;
                string startDateStr = null;
                if (line.EndDate.GetValueOrDefault() != default(DateTime))
                {
                    endDateStr = line.EndDate.GetValueOrDefault().ToString("s", CultureInfo.InvariantCulture);
                }
                if (line.StartDate.GetValueOrDefault() != default(DateTime))
                {
                    startDateStr = line.StartDate.GetValueOrDefault().ToString("s", CultureInfo.InvariantCulture);
                }
                if (line.EndDate == default(DateTime))
                {
                    line.EndDate = null;
                }
                if (line.StartDate == default(DateTime))
                {
                    line.StartDate = null;
                }

                builder.InsertData("Places",
                    new string[] { "name", "province_name", "province_abbreviation", "region_name", "code", "start_date", "end_date" },
                    new object[] { line.Name, line.Province, line.ProvinceAbbreviation, line.Region, line.Code, startDateStr, endDateStr });
            }

        }
        private static readonly string AnprFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Migrations\\Data\\ANPR_archivio_comuni.csv";
        private static readonly string IstatForeignCountriesPath = AppDomain.CurrentDomain.BaseDirectory + "\\Migrations\\Data\\Elenco-codici-e-denominazioni-al-31_12_2019.csv";
        private static readonly string FormerForeignCountriesPath = AppDomain.CurrentDomain.BaseDirectory + "\\Migrations\\Data\\Elenco-Paesi-esteri-cessati.csv";
    }
}