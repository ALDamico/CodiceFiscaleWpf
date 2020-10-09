using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters;
using ALD.LibFiscalCode.Persistence.Importer.Models;
using ALD.LibFiscalCode.Persistence.Migrations;
using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodiceFiscaleApi.Migrations
{
    public partial class PlacesConversionMappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new NullReferenceException();
            }

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
                    start_date = table.Column<DateTime>(nullable: true),
                    end_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Places", x => x.id); });

            migrationBuilder.CreateTable(
                name: "ProvinceMapping",
                columns: table => new
                {
                    abbreviation = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ProvinceMapping", x => x.abbreviation); });

            migrationBuilder.CreateTable(
                name: "RegionMapping",
                columns: table => new
                {
                    province_abbreviation = table.Column<string>(type: "VARCHAR(2)", nullable: false),
                    name = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_RegionMapping", x => x.province_abbreviation); });

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
            if (migrationBuilder == null)
            {
                throw new NullReferenceException();
            }

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "ProvinceMapping");

            migrationBuilder.DropTable(
                name: "RegionMapping");
        }

        protected virtual void Seed(MigrationBuilder builder)
        {
            if (builder == null)
            {
                throw new NullReferenceException();
            }

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
                if (newCountry != null)
                {
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
                    new[]
                    {
                        "name", "province_name", "province_abbreviation", "region_name", "code", "start_date",
                        "end_date"
                    },
                    new object[]
                    {
                        country.Name, country.Province, country.ProvinceAbbreviation, country.Region, country.Code,
                        startDateStr, endDateStr
                    });
            }
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

                //Used to determine the end of file
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
                    new[]
                    {
                        "name", "province_name", "province_abbreviation", "region_name", "code", "start_date",
                        "end_date"
                    },
                    new object[]
                    {
                        line.Name, line.Province, line.ProvinceAbbreviation, line.Region, line.Code, startDateStr,
                        endDateStr
                    });
            }
        }

        private static readonly string AnprFilePath =
            AppDomain.CurrentDomain.BaseDirectory + "\\Migrations\\Data\\ANPR_archivio_comuni.csv";

        private static readonly string IstatForeignCountriesPath = AppDomain.CurrentDomain.BaseDirectory +
                                                                   "\\Migrations\\Data\\Elenco-codici-e-denominazioni-al-31_12_2019.csv";

        private static readonly string FormerForeignCountriesPath = AppDomain.CurrentDomain.BaseDirectory +
                                                                    "\\Migrations\\Data\\Elenco-Paesi-esteri-cessati.csv";
    }
}