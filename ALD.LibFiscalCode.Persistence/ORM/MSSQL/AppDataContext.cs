using System;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.ORM.MSSQL
{
    public class AppDataContext:DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            
            //Places configuration
            var placeEntity = modelBuilder.Entity<Place>();
            placeEntity.Property(p => p.Id).HasColumnName("id");
            placeEntity.HasKey(nameof(Place.Id));
            placeEntity.Property(p => p.Name).HasColumnName("name");
            placeEntity.Property(p => p.Province).HasColumnName("province_name");
            placeEntity.Property(p => p.ProvinceAbbreviation).HasColumnName("province_abbreviation");
            placeEntity.Property(p => p.Region).HasColumnName("region_name");
            placeEntity.Property(p => p.Code).HasColumnName("code");
            placeEntity.Property(p => p.StartDate).HasColumnName("start_date");
            placeEntity.Property(p => p.EndDate).HasColumnName("end_date");
            placeEntity.HasIndex(p => p.ProvinceAbbreviation);
            placeEntity.HasIndex(p => p.Region);

            var provinceMappingEntity = modelBuilder.Entity<ProvinceMapping>();
            provinceMappingEntity.Property(map => map.Abbreviation).HasColumnName("abbreviation")
                .HasColumnType("VARCHAR(2)").IsRequired();
            provinceMappingEntity.Property(map => map.Name).HasColumnName("name").HasColumnType("VARCHAR(50)");
            provinceMappingEntity.HasKey(map => map.Abbreviation);

            var regionMappingEntity = modelBuilder.Entity<RegionMapping>();
            regionMappingEntity.Property(map => map.ProvinceAbbreviation).HasColumnName("province_abbreviation")
                .HasColumnType("VARCHAR(2)").IsRequired();
            regionMappingEntity.Property(map => map.Name).HasColumnName("name").HasColumnType("VARCHAR(50)");
            regionMappingEntity.HasKey(map => map.ProvinceAbbreviation);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Place> Places { get; set; }
    }
}