﻿// <auto-generated />
using System;
using ALD.LibFiscalCode.Persistence.ORM.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ALD.LibFiscalCode.Persistence.Migrations.SqlServerData
{
    [DbContext(typeof(SqlServerDataContext))]
    [Migration("20201018235131_Mapping seeding fix")]
    partial class Mappingseedingfix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("end_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnName("province_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProvinceAbbreviation")
                        .HasColumnName("province_abbreviation")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Region")
                        .HasColumnName("region_name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("start_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnName("updated_on")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EndDate");

                    b.HasIndex("ProvinceAbbreviation");

                    b.HasIndex("Region");

                    b.HasIndex("StartDate");

                    b.HasIndex("UpdatedOn");

                    b.ToTable("places");
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.ProvinceMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnName("abbreviation")
                        .HasColumnType("VARCHAR(2)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int?>("region_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("region_id");

                    b.ToTable("province_mapping");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abbreviation = "AG",
                            Name = "Agrigento"
                        },
                        new
                        {
                            Id = 2,
                            Abbreviation = "AL",
                            Name = "Alessandria"
                        },
                        new
                        {
                            Id = 3,
                            Abbreviation = "AN",
                            Name = "Ancona"
                        },
                        new
                        {
                            Id = 4,
                            Abbreviation = "AO",
                            Name = "Aosta"
                        },
                        new
                        {
                            Id = 5,
                            Abbreviation = "AO",
                            Name = "Arezzo"
                        },
                        new
                        {
                            Id = 6,
                            Abbreviation = "AP",
                            Name = "Ascoli Piceno"
                        },
                        new
                        {
                            Id = 7,
                            Abbreviation = "AT",
                            Name = "Asti"
                        },
                        new
                        {
                            Id = 8,
                            Abbreviation = "AV",
                            Name = "Avellino"
                        },
                        new
                        {
                            Id = 9,
                            Abbreviation = "BA",
                            Name = "Bari"
                        },
                        new
                        {
                            Id = 10,
                            Abbreviation = "BT",
                            Name = "Barletta-Andria-Trani"
                        },
                        new
                        {
                            Id = 11,
                            Abbreviation = "BL",
                            Name = "Belluno"
                        },
                        new
                        {
                            Id = 12,
                            Abbreviation = "BN",
                            Name = "Benevento"
                        },
                        new
                        {
                            Id = 13,
                            Abbreviation = "BG",
                            Name = "Bergamo"
                        },
                        new
                        {
                            Id = 14,
                            Abbreviation = "BI",
                            Name = "Biella"
                        },
                        new
                        {
                            Id = 15,
                            Abbreviation = "BO",
                            Name = "Bologna"
                        },
                        new
                        {
                            Id = 16,
                            Abbreviation = "BZ",
                            Name = "Bolzano"
                        },
                        new
                        {
                            Id = 17,
                            Abbreviation = "BS",
                            Name = "Brescia"
                        },
                        new
                        {
                            Id = 18,
                            Abbreviation = "BR",
                            Name = "Brindisi"
                        },
                        new
                        {
                            Id = 19,
                            Abbreviation = "CA",
                            Name = "Cagliari"
                        },
                        new
                        {
                            Id = 20,
                            Abbreviation = "CL",
                            Name = "Caltanissetta"
                        },
                        new
                        {
                            Id = 21,
                            Abbreviation = "CB",
                            Name = "Campobasso"
                        },
                        new
                        {
                            Id = 22,
                            Abbreviation = "CE",
                            Name = "Caserta"
                        },
                        new
                        {
                            Id = 23,
                            Abbreviation = "CT",
                            Name = "Catania"
                        },
                        new
                        {
                            Id = 24,
                            Abbreviation = "CZ",
                            Name = "Catanzaro"
                        },
                        new
                        {
                            Id = 25,
                            Abbreviation = "CH",
                            Name = "Chieti"
                        },
                        new
                        {
                            Id = 26,
                            Abbreviation = "CO",
                            Name = "Como"
                        },
                        new
                        {
                            Id = 27,
                            Abbreviation = "CS",
                            Name = "Cosenza"
                        },
                        new
                        {
                            Id = 28,
                            Abbreviation = "CR",
                            Name = "Cremona"
                        },
                        new
                        {
                            Id = 29,
                            Abbreviation = "KR",
                            Name = "Crotone"
                        },
                        new
                        {
                            Id = 30,
                            Abbreviation = "CN",
                            Name = "Cuneo"
                        },
                        new
                        {
                            Id = 31,
                            Abbreviation = "EN",
                            Name = "Enna"
                        },
                        new
                        {
                            Id = 32,
                            Abbreviation = "FM",
                            Name = "Fermo"
                        },
                        new
                        {
                            Id = 33,
                            Abbreviation = "FE",
                            Name = "Ferrara"
                        },
                        new
                        {
                            Id = 34,
                            Abbreviation = "FI",
                            Name = "Firenze"
                        },
                        new
                        {
                            Id = 35,
                            Abbreviation = "FG",
                            Name = "Foggia"
                        },
                        new
                        {
                            Id = 36,
                            Abbreviation = "FC",
                            Name = "Forlì-Cesena"
                        },
                        new
                        {
                            Id = 37,
                            Abbreviation = "FR",
                            Name = "Frosinone"
                        },
                        new
                        {
                            Id = 38,
                            Abbreviation = "GE",
                            Name = "Genova"
                        },
                        new
                        {
                            Id = 39,
                            Abbreviation = "GO",
                            Name = "Gorizia"
                        },
                        new
                        {
                            Id = 40,
                            Abbreviation = "GR",
                            Name = "Grosseto"
                        },
                        new
                        {
                            Id = 41,
                            Abbreviation = "IM",
                            Name = "Imperia"
                        },
                        new
                        {
                            Id = 42,
                            Abbreviation = "IS",
                            Name = "Isernia"
                        },
                        new
                        {
                            Id = 43,
                            Abbreviation = "AQ",
                            Name = "L'Aquila"
                        },
                        new
                        {
                            Id = 44,
                            Abbreviation = "SP",
                            Name = "La Spezia"
                        },
                        new
                        {
                            Id = 45,
                            Abbreviation = "LT",
                            Name = "Latina"
                        },
                        new
                        {
                            Id = 46,
                            Abbreviation = "LE",
                            Name = "Lecce"
                        },
                        new
                        {
                            Id = 47,
                            Abbreviation = "LC",
                            Name = "Lecco"
                        },
                        new
                        {
                            Id = 48,
                            Abbreviation = "LI",
                            Name = "Livorno"
                        },
                        new
                        {
                            Id = 49,
                            Abbreviation = "LO",
                            Name = "Lodi"
                        },
                        new
                        {
                            Id = 50,
                            Abbreviation = "LU",
                            Name = "Lucca"
                        },
                        new
                        {
                            Id = 51,
                            Abbreviation = "MC",
                            Name = "Macerata"
                        },
                        new
                        {
                            Id = 52,
                            Abbreviation = "MN",
                            Name = "Mantova"
                        },
                        new
                        {
                            Id = 53,
                            Abbreviation = "MS",
                            Name = "Massa-Carrara"
                        },
                        new
                        {
                            Id = 54,
                            Abbreviation = "MT",
                            Name = "Matera"
                        },
                        new
                        {
                            Id = 55,
                            Abbreviation = "ME",
                            Name = "Messina"
                        },
                        new
                        {
                            Id = 56,
                            Abbreviation = "MI",
                            Name = "Milano"
                        },
                        new
                        {
                            Id = 57,
                            Abbreviation = "MO",
                            Name = "Modena"
                        },
                        new
                        {
                            Id = 58,
                            Abbreviation = "MB",
                            Name = "Monza e della Brianza"
                        },
                        new
                        {
                            Id = 59,
                            Abbreviation = "NA",
                            Name = "Napoli"
                        },
                        new
                        {
                            Id = 60,
                            Abbreviation = "NO",
                            Name = "Novara"
                        },
                        new
                        {
                            Id = 61,
                            Abbreviation = "NU",
                            Name = "Nuoro"
                        },
                        new
                        {
                            Id = 62,
                            Abbreviation = "OR",
                            Name = "Oristano"
                        },
                        new
                        {
                            Id = 63,
                            Abbreviation = "PD",
                            Name = "Padova"
                        },
                        new
                        {
                            Id = 64,
                            Abbreviation = "PA",
                            Name = "Palermo"
                        },
                        new
                        {
                            Id = 65,
                            Abbreviation = "PR",
                            Name = "Parma"
                        },
                        new
                        {
                            Id = 66,
                            Abbreviation = "PV",
                            Name = "Pavia"
                        },
                        new
                        {
                            Id = 67,
                            Abbreviation = "PG",
                            Name = "Perugia"
                        },
                        new
                        {
                            Id = 68,
                            Abbreviation = "PU",
                            Name = "Pesaro e Urbino"
                        },
                        new
                        {
                            Id = 69,
                            Abbreviation = "PE",
                            Name = "Pescara"
                        },
                        new
                        {
                            Id = 70,
                            Abbreviation = "PC",
                            Name = "Piacenza"
                        },
                        new
                        {
                            Id = 71,
                            Abbreviation = "PI",
                            Name = "Pisa"
                        },
                        new
                        {
                            Id = 72,
                            Abbreviation = "PT",
                            Name = "Pistoia"
                        },
                        new
                        {
                            Id = 73,
                            Abbreviation = "PN",
                            Name = "Pordenone"
                        },
                        new
                        {
                            Id = 74,
                            Abbreviation = "PZ",
                            Name = "Potenza"
                        },
                        new
                        {
                            Id = 75,
                            Abbreviation = "PO",
                            Name = "Prato"
                        },
                        new
                        {
                            Id = 76,
                            Abbreviation = "RG",
                            Name = "Ragusa"
                        },
                        new
                        {
                            Id = 77,
                            Abbreviation = "RA",
                            Name = "Ravenna"
                        },
                        new
                        {
                            Id = 78,
                            Name = "Reggio Calabria"
                        },
                        new
                        {
                            Id = 79,
                            Abbreviation = "RE",
                            Name = "Reggio Emilia"
                        },
                        new
                        {
                            Id = 80,
                            Abbreviation = "RI",
                            Name = "Rieti"
                        },
                        new
                        {
                            Id = 81,
                            Abbreviation = "RN",
                            Name = "Rimini"
                        },
                        new
                        {
                            Id = 82,
                            Abbreviation = "RM",
                            Name = "Roma"
                        },
                        new
                        {
                            Id = 83,
                            Abbreviation = "RO",
                            Name = "Rovigo"
                        },
                        new
                        {
                            Id = 84,
                            Abbreviation = "SA",
                            Name = "Salerno"
                        },
                        new
                        {
                            Id = 85,
                            Abbreviation = "SS",
                            Name = "Sassari"
                        },
                        new
                        {
                            Id = 86,
                            Abbreviation = "SV",
                            Name = "Savona"
                        },
                        new
                        {
                            Id = 87,
                            Abbreviation = "SI",
                            Name = "Siena"
                        },
                        new
                        {
                            Id = 88,
                            Abbreviation = "SR",
                            Name = "Siracusa"
                        },
                        new
                        {
                            Id = 89,
                            Abbreviation = "SO",
                            Name = "Sondrio"
                        },
                        new
                        {
                            Id = 90,
                            Abbreviation = "SU",
                            Name = "Sud Sardegna"
                        },
                        new
                        {
                            Id = 91,
                            Abbreviation = "TA",
                            Name = "Taranto"
                        },
                        new
                        {
                            Id = 92,
                            Abbreviation = "TE",
                            Name = "Teramo"
                        },
                        new
                        {
                            Id = 93,
                            Abbreviation = "TR",
                            Name = "Terni"
                        },
                        new
                        {
                            Id = 94,
                            Abbreviation = "TO",
                            Name = "Torino"
                        },
                        new
                        {
                            Id = 95,
                            Abbreviation = "TP",
                            Name = "Trapani"
                        },
                        new
                        {
                            Id = 96,
                            Abbreviation = "TN",
                            Name = "Trento"
                        },
                        new
                        {
                            Id = 97,
                            Abbreviation = "TV",
                            Name = "Treviso"
                        },
                        new
                        {
                            Id = 98,
                            Abbreviation = "TS",
                            Name = "Trieste"
                        },
                        new
                        {
                            Id = 99,
                            Abbreviation = "UD",
                            Name = "Udine"
                        },
                        new
                        {
                            Id = 100,
                            Abbreviation = "VA",
                            Name = "Varese"
                        },
                        new
                        {
                            Id = 101,
                            Abbreviation = "VE",
                            Name = "Venezia"
                        },
                        new
                        {
                            Id = 102,
                            Abbreviation = "VB",
                            Name = "Verbano-Cusio-Ossola"
                        },
                        new
                        {
                            Id = 103,
                            Abbreviation = "VC",
                            Name = "Vercelli"
                        },
                        new
                        {
                            Id = 104,
                            Abbreviation = "VR",
                            Name = "Verona"
                        },
                        new
                        {
                            Id = 105,
                            Abbreviation = "VV",
                            Name = "Vibo Valentia"
                        },
                        new
                        {
                            Id = 106,
                            Abbreviation = "VI",
                            Name = "Vicenza"
                        },
                        new
                        {
                            Id = 107,
                            Abbreviation = "VT",
                            Name = "Viterbo"
                        });
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.RegionMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.ToTable("region_mapping");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Abruzzo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Basilicata"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Calabria"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Campania"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Emilia-Romagna"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Friuli Venezia Giulia"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Lazio"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Liguria"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Lombardia"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Marche"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Molise"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Piemonte"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Puglia"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Sardegna"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Sicilia"
                        },
                        new
                        {
                            Id = 16,
                            Name = "Toscana"
                        },
                        new
                        {
                            Id = 17,
                            Name = "Trentino-Alto Adige"
                        },
                        new
                        {
                            Id = 18,
                            Name = "Umbria"
                        },
                        new
                        {
                            Id = 19,
                            Name = "Valle d'Aosta"
                        },
                        new
                        {
                            Id = 20,
                            Name = "Veneto"
                        });
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.ProvinceMapping", b =>
                {
                    b.HasOne("ALD.LibFiscalCode.Persistence.Models.RegionMapping", "Region")
                        .WithMany("Provinces")
                        .HasForeignKey("region_id");
                });
#pragma warning restore 612, 618
        }
    }
}
