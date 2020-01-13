using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.Sqlite
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
        {
        }

        public AppDataContext(string dbPath)
        {
            this.dbPath = dbPath;
        }

        public DbSet<Place> Places { get; set; }

        public DbSet<Person> People { get; }
        public DbSet<FiscalCodeEntity> FiscalCodes { get; set; }
        public DbSet<LanguageInfo> Languages { get; set; }
        public DbSet<LocalizedString> LocalizedStrings { get; set; }
        public DbSet<WindowModel> Windows { get; set; }
        public DbSet<SettingModel> Settings { get; set; }

        private string dbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = AppDomain.CurrentDomain.BaseDirectory + "\\DataSource\\app.db";
            }
            string connectionString = $"Data source={dbPath}";
            optionsBuilder.UseSqlite(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var placeEntity = modelBuilder.Entity<Place>();
            placeEntity.Property(p => p.Id).HasColumnName("id");
            placeEntity.HasKey("Id");
            placeEntity.Property(p => p.Name).HasColumnName("name");
            placeEntity.Property(p => p.Province).HasColumnName("province_name");
            placeEntity.Property(p => p.ProvinceAbbreviation).HasColumnName("province_abbreviation");
            placeEntity.Property(p => p.Region).HasColumnName("region_name");
            placeEntity.Property(p => p.Code).HasColumnName("code");
            placeEntity.HasIndex(p => p.ProvinceAbbreviation);
            placeEntity.HasIndex(p => p.Region);

            var peopleEntity = modelBuilder.Entity<Person>();

            peopleEntity.Property<int>("Id").HasColumnType("int").HasColumnName("id");
            peopleEntity.HasKey("Id");
            peopleEntity.Property(p => p.Name).HasColumnName("name");
            peopleEntity.Property(p => p.Surname).HasColumnName("surname");
            peopleEntity.Property(p => p.DateOfBirth).HasColumnName("date_of_birth");
            peopleEntity.Property(p => p.Gender).HasColumnName("gender").HasColumnType("text")
                .HasConversion(g => g == Gender.Male ? "M" : "F", g => g.Equals("M") ? Gender.Male : Gender.Female);
            peopleEntity.HasOne(p => p.PlaceOfBirth);

            var fiscalCodeEntity = modelBuilder.Entity<FiscalCodeEntity>();
            fiscalCodeEntity.ToTable("FiscalCodes");
            fiscalCodeEntity.Property<int>("Id").HasColumnType("int").HasColumnName("id");
            fiscalCodeEntity.HasKey("Id");
            fiscalCodeEntity.Property(fc => fc.FiscalCode).HasColumnName("fiscal_code");
            fiscalCodeEntity.Property<int>("PersonId").HasColumnName("person_id");
            fiscalCodeEntity.HasOne(fc => fc.Person);

            var languageInfoEntity = modelBuilder.Entity<LanguageInfo>();
            languageInfoEntity.ToTable("Languages");
            languageInfoEntity.Property(l => l.Id).HasColumnName("id");
            languageInfoEntity.HasKey(l => l.Id);
            languageInfoEntity.Property(l => l.Name).HasColumnName("name");
            languageInfoEntity.Property(l => l.Iso2Code).HasColumnName("iso_2_code");
            languageInfoEntity.Property(l => l.Iso3Code).HasColumnName("iso_3_code");
            languageInfoEntity.Property(l => l.Icon).HasColumnName("icon").HasColumnType("blob");
            languageInfoEntity.Property(l => l.ImagePath).HasColumnName("icon_name");

            var windowsEntity = modelBuilder.Entity<WindowModel>();
            windowsEntity.ToTable("Windows");
            windowsEntity.Property(w => w.Id).HasColumnName("id");
            windowsEntity.Property(w => w.Name).HasColumnName("window_name");

            var localizedStringEntity = modelBuilder.Entity<LocalizedString>();
            localizedStringEntity.Property(s => s.Id).HasColumnName("id");
            localizedStringEntity.HasKey(s => s.Id);
            localizedStringEntity.Property(s => s.Name).HasColumnName("name");
            localizedStringEntity.Property(s => s.Value).HasColumnName("value");
            localizedStringEntity.Property<int>("language_id").HasColumnName("language_id").HasColumnType("int");
            localizedStringEntity.HasOne(s => s.Language).WithMany().HasForeignKey("language_id");
            localizedStringEntity.Property<int>("window_id").HasColumnName("window_id").HasColumnType("int");
            localizedStringEntity.HasOne(s => s.Window).WithMany().HasForeignKey("window_id");

            var appSettingsEntity = modelBuilder.Entity<SettingModel>();
            appSettingsEntity.ToTable("Settings");
            appSettingsEntity.Property(s => s.Id).HasColumnName("id");
            appSettingsEntity.Property(s => s.Name).HasColumnName("name");
            appSettingsEntity.Property(s => s.IntValue).HasColumnName("int_value");
            appSettingsEntity.Property(s => s.StringValue).HasColumnName("string_value");

            base.OnModelCreating(modelBuilder);
        }

        public async Task SavePerson(Person person)
        {
            if (await People.Include(p => p.PlaceOfBirth).ContainsAsync(person))
            {
                return;
            }

            People.Add(person);
        }

        public Dictionary<string, string> GetLocalizedStrings(LanguageInfo languageInfo)
        {
            var dic = (from l in LocalizedStrings.Include(l => l.Language)
                       where l.Language.Equals(languageInfo)
                       select l
                ).ToDictionary(l => l.Name, x => x.Value);
            return dic;
        }
    }
}