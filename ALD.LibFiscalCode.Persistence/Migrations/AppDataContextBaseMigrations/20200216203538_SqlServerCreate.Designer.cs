﻿// <auto-generated />
using ALD.LibFiscalCode.Persistence.ORM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ALD.LibFiscalCode.Persistence.Migrations.AppDataContextBaseMigrations
{
    [DbContext(typeof(AppDataContextBase))]
    [Migration("20200216203538_SqlServerCreate")]
    partial class SqlServerCreate
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

                    b.HasKey("Id");

                    b.HasIndex("ProvinceAbbreviation");

                    b.HasIndex("Region");

                    b.ToTable("Places");
                });
#pragma warning restore 612, 618
        }
    }
}
