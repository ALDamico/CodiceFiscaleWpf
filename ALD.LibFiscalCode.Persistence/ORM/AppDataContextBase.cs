using System;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.MySQL;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.ORM
{
    public class AppDataContextBase:DbContext
    {
        public AppDataContextBase()
        {
            
        }
        public AppDataContextBase(DbContextOptions<AppDataContextBase> options):base(options)
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
            placeEntity.HasKey("Id");
            placeEntity.Property(p => p.Name).HasColumnName("name");
            placeEntity.Property(p => p.Province).HasColumnName("province_name");
            placeEntity.Property(p => p.ProvinceAbbreviation).HasColumnName("province_abbreviation");
            placeEntity.Property(p => p.Region).HasColumnName("region_name");
            placeEntity.Property(p => p.Code).HasColumnName("code");
            placeEntity.HasIndex(p => p.ProvinceAbbreviation);
            placeEntity.HasIndex(p => p.Region);
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Place> Places { get; set; }
    }
}