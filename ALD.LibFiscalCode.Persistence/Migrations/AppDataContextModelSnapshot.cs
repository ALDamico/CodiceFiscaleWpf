﻿// <auto-generated />
using System;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ALD.LibFiscalCode.Persistence.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.FiscalCodeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<string>("FiscalCode")
                        .HasColumnName("fiscal_code")
                        .HasColumnType("TEXT");

                    b.Property<int>("PersonId")
                        .HasColumnName("person_id")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("FiscalCodes");
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.LanguageInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagePath")
                        .HasColumnName("icon_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Iso2Code")
                        .HasColumnName("iso_2_code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Iso3Code")
                        .HasColumnName("iso_3_code")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnName("date_of_birth")
                        .HasColumnType("TEXT");

                    b.Property<int>("FiscalCodeId")
                        .HasColumnName("fiscal_code_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnName("gender")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PlaceOfBirthId")
                        .HasColumnName("place_of_birth_id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Surname")
                        .HasColumnName("surname")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PlaceOfBirthId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnName("end_date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Province")
                        .HasColumnName("province_name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProvinceAbbreviation")
                        .HasColumnName("province_abbreviation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Region")
                        .HasColumnName("region_name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnName("start_date")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceAbbreviation");

                    b.HasIndex("Region");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.SettingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("IntValue")
                        .HasColumnName("int_value")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("TEXT");

                    b.Property<string>("StringValue")
                        .HasColumnName("string_value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.FiscalCodeEntity", b =>
                {
                    b.HasOne("ALD.LibFiscalCode.Persistence.Models.Person", "Person")
                        .WithOne("FiscalCode")
                        .HasForeignKey("ALD.LibFiscalCode.Persistence.Models.FiscalCodeEntity", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ALD.LibFiscalCode.Persistence.Models.Person", b =>
                {
                    b.HasOne("ALD.LibFiscalCode.Persistence.Models.Place", "PlaceOfBirth")
                        .WithMany()
                        .HasForeignKey("PlaceOfBirthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
