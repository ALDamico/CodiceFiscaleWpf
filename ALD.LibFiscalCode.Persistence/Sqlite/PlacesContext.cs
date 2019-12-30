using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using ALD.LibFiscalCode.Persistence.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;
using ALD.LibFiscalCode.Enums;

namespace ALD.LibFiscalCode.Persistence.Sqlite
{
    public class PlacesContext : DbContext
    {
        public PlacesContext()
        {
           // Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=C:/Users/aldam/Source/Repos/CodiceFiscaleWpf/ALD.LibFiscalCode.Persistence/DataSource/app.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Place> Places { get; set; }

        internal DbSet<Person> People { get; set; }
        public DbSet<FiscalCodeEntity> FiscalCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var placeEntity = modelBuilder.Entity<Place>();
            placeEntity.Property<int>("Id").HasColumnType("int");
            placeEntity.HasKey("Id");
            placeEntity.Property(p => p.Name).HasColumnName("name");
            placeEntity.Property(p => p.Province).HasColumnName("province_name");
            placeEntity.Property(p => p.ProvinceAbbreviation).HasColumnName("province_abbreviation");
            placeEntity.Property(p => p.Region).HasColumnName("region_name");
            placeEntity.Property(p => p.Code).HasColumnName("code");

            var peopleEntity = modelBuilder.Entity<Person>();
           
            peopleEntity.Property<int>("Id").HasColumnType("int");
            peopleEntity.HasKey("Id");
            peopleEntity.Property(p => p.Name).HasColumnName("name");
            peopleEntity.Property(p => p.Surname).HasColumnName("surname");
            peopleEntity.Property(p => p.DateOfBirth).HasColumnName("date_of_birth");
            peopleEntity.Property(p => p.Gender).HasColumnName("gender").HasColumnType("text").HasConversion(g => g == Gender.Male ? "M" : "F", g => g.Equals("M") ? Gender.Male : Gender.Female);
            
            peopleEntity.HasOne<Place>(p => p.PlaceOfBirth).WithMany();

            var fiscalCodeEntity = modelBuilder.Entity<FiscalCodeEntity>();
            fiscalCodeEntity.ToTable("FiscalCodes");
            fiscalCodeEntity.Property<int>("Id").HasColumnType("int");
            fiscalCodeEntity.HasKey("Id");
            fiscalCodeEntity.Property(fc => fc.FiscalCode).HasColumnName("fiscal_code");
            //fiscalCodeEntity.Property(fc => fc.Person).HasColumnName("person_id");
            fiscalCodeEntity.Property<int>("PersonId").HasColumnName("person_id");
            fiscalCodeEntity.HasOne<Person>(fc => fc.Person);

            base.OnModelCreating(modelBuilder);
        }

        public async void SavePerson(Person person)
        {
            var peopleTmp = People.Include(p => p.PlaceOfBirth).ToList();
            if (peopleTmp.Contains(person, person.GetEqualityComparer()))
            {
                return;
            }

            People.Add(person);
            await SaveChangesAsync();
        }

        public Task<List<Place>> GetAllPlaces()
        {
            Task<List<Place>> placesTask = Places.OrderBy(p => p.Name).ToListAsync();

            return placesTask;
        }
    }
}