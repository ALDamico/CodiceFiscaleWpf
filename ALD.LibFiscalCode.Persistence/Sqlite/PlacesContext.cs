using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ALD.LibFiscalCode.Persistence.Sqlite
{
    public class PlacesContext : DbContext
    {
        public PlacesContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data source=C:/Users/Luciano/source/repos/CodiceFiscale/ALD.LibFiscalCode.Persistence/DataSource/app.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Place> Places { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var placeEntity = modelBuilder.Entity<Place>();
            placeEntity.Property<int>("Id").HasColumnType("int");
            placeEntity.Property(p => p.Name).HasColumnName("name");
            placeEntity.Property(p => p.Province).HasColumnName("province_name");
            placeEntity.Property(p => p.ProvinceAbbreviation).HasColumnName("province_abbreviation");
            placeEntity.Property(p => p.Region).HasColumnName("region_name");
            placeEntity.Property(p => p.Code).HasColumnName("code");
            base.OnModelCreating(modelBuilder);
        }

        public async Task<List<Place>> GetAllPlaces()
        {
            Task<List<Place>> placesTask = Places.OrderBy(p => p.ProvinceAbbreviation).ToListAsync();
            return await placesTask;
        }
    }
}
