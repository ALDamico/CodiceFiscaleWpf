using System;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.ORM.MSSQL
{
    public class SqlServerDataContext:DbContext
    {
        public SqlServerDataContext(DbContextOptions<SqlServerDataContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            
            ConfigurePlaces(modelBuilder);
            ConfigureProvinceMapping(modelBuilder);
            ConfigureRegionMapping(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private static void ConfigureRegionMapping(ModelBuilder modelBuilder)
        {
            var regionMappingEntity = modelBuilder.Entity<RegionMapping>();

            regionMappingEntity.Property(p => p.Id)
                .HasColumnName("id")
                .HasColumnType("INTEGER")
                .IsRequired();
            regionMappingEntity.HasKey(p => p.Id);
            
            regionMappingEntity.Property(map => map.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)");
            regionMappingEntity.HasMany(r => r.Provinces)
                .WithOne(p => p.Region);
        }

        private static void ConfigureProvinceMapping(ModelBuilder modelBuilder)
        {
            var provinceMappingEntity = modelBuilder.Entity<ProvinceMapping>();
            
            provinceMappingEntity.Property(map => map.Abbreviation)
                .HasColumnName("abbreviation")
                .HasColumnType("VARCHAR(2)")
                .IsRequired();
            provinceMappingEntity.Property(map => map.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)");
            provinceMappingEntity.Property(map => map.Id)
                .HasColumnName("id");
            provinceMappingEntity.HasKey(map => map.Id);
            provinceMappingEntity.HasOne(map => map.Region)
                .WithMany(r => r.Provinces).HasForeignKey("region_id");

        }

        private static void ConfigurePlaces(ModelBuilder modelBuilder)
        {
            //Places configuration
            var placeEntity = modelBuilder.Entity<Place>();
            placeEntity.HasKey(p => p.Id);
            placeEntity.Property(p => p.Id)
                .HasColumnName("id");
            placeEntity.Property(p => p.Name)
                .HasColumnName("name");
            placeEntity.Property(p => p.Province)
                .HasColumnName("province_name");
            placeEntity.Property(p => p.ProvinceAbbreviation)
                .HasColumnName("province_abbreviation");
            placeEntity.Property(p => p.Region)
                .HasColumnName("region_name");
            placeEntity.Property(p => p.Code)
                .HasColumnName("code");
            placeEntity.Property(p => p.StartDate)
                .HasColumnName("start_date");
            placeEntity.Property(p => p.EndDate)
                .HasColumnName("end_date");
            placeEntity.Property(p => p.UpdatedOn)
                .HasColumnName("updated_on");
            
            placeEntity.HasIndex(p => p.ProvinceAbbreviation);
            placeEntity.HasIndex(p => p.Region);
            placeEntity.HasIndex(p => p.UpdatedOn);
            placeEntity.HasIndex(p => p.StartDate);
            placeEntity.HasIndex(p => p.EndDate);
        }

        public DateTime? GetLastPlacesUpdateDate()
        {
            return Places.Max(p => p.UpdatedOn);
        }

        public DbSet<Place> Places { get; }
        public DbSet<RegionMapping> Regions { get; }
    }
}