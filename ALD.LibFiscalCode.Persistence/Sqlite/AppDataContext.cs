using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ALD.LibFiscalCode.Persistence.Sqlite
{
    public class AppDataContext : DbContext
    {
        public AppDataContext()
        {
        }

        public async Task MigrateAsync()
        {
            Database.Migrate();
        }

        public DbSet<Place> Places { get; set; }

        public DbSet<Person> People { get; set; }
        public DbSet<FiscalCodeEntity> FiscalCodes { get; set; }
        public DbSet<LanguageInfo> Languages { get; set; }
        public DbSet<SettingModel> Settings { get; set; }

        private string dbPath;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(dbPath))
            {
                dbPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CodiceFiscale\\DataSource\\app.db";
            }

            if (!File.Exists(dbPath))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CodiceFiscale\\DataSource");
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
            peopleEntity.Property<int>("PlaceOfBirthId").HasColumnName("place_of_birth_id");
            peopleEntity.HasOne<Place>(p => p.PlaceOfBirth).WithMany().HasForeignKey("PlaceOfBirthId");
            peopleEntity.Property<int>("FiscalCodeId").HasColumnName("fiscal_code_id");
            peopleEntity.HasOne<FiscalCodeEntity>(p => p.FiscalCode).WithOne(fc => fc.Person).HasForeignKey<Person>("FiscalCodeId");

            var fiscalCodeEntity = modelBuilder.Entity<FiscalCodeEntity>();
            fiscalCodeEntity.ToTable("FiscalCodes");
            fiscalCodeEntity.Property<int>("Id").HasColumnType("int").HasColumnName("id");
            fiscalCodeEntity.HasKey("Id");
            fiscalCodeEntity.Property(fc => fc.FiscalCode).HasColumnName("fiscal_code");
            fiscalCodeEntity.Property<int>("PersonId").HasColumnName("person_id");
            fiscalCodeEntity.HasOne<Person>(fc => fc.Person).WithOne(p => p.FiscalCode).HasForeignKey<FiscalCodeEntity>("PersonId");

            var languageInfoEntity = modelBuilder.Entity<LanguageInfo>();
            languageInfoEntity.ToTable("Languages");
            languageInfoEntity.Property(l => l.Id).HasColumnName("id");
            languageInfoEntity.HasKey(l => l.Id);
            languageInfoEntity.Property(l => l.Name).HasColumnName("name");
            languageInfoEntity.Property(l => l.Iso2Code).HasColumnName("iso_2_code");
            languageInfoEntity.Property(l => l.Iso3Code).HasColumnName("iso_3_code");
            languageInfoEntity.Property(l => l.ImagePath).HasColumnName("icon_name");

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
            if (person == null)
            {
                return;
            }
            if (await People.ContainsAsync(person))
            {
                Entry(person).State = EntityState.Unchanged;
                return;
            }

            People.Add(person);
            FiscalCodes.Add(person.FiscalCode);
            Entry(person.PlaceOfBirth).State = EntityState.Detached;
            SaveChanges();
        }

        public async Task SaveFiscalCode(IEnumerable<FiscalCodeDecorator> codes, Person person)
        {
            var newFc = new FiscalCodeEntity
            {
                FiscalCode = codes.FirstOrDefault(fc => fc.IsMain)?.FiscalCode.FullFiscalCode,
                Person = person
            };

            if (await FiscalCodes.ContainsAsync(newFc))
            {
                return;
            }
            Entry<Place>(person.PlaceOfBirth).State = EntityState.Detached;
            Entry<Person>(person).State = EntityState.Detached;
            FiscalCodes.Add(newFc);
            await SaveChangesAsync();
        }
    }
}