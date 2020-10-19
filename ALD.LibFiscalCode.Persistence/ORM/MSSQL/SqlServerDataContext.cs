using System;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.ORM.MSSQL
{
    public class SqlServerDataContext : DbContext
    {
        public SqlServerDataContext(DbContextOptions<SqlServerDataContext> options) : base(options)
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

            regionMappingEntity.ToTable("region_mapping");
            regionMappingEntity.Property(p => p.RegionId)
                .HasColumnName("id")
                .HasColumnType("INTEGER")
                .IsRequired();
            regionMappingEntity.HasKey(p => p.RegionId);

            regionMappingEntity.Property(map => map.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)");
            regionMappingEntity.HasMany(r => r.Provinces)
                .WithOne(p => p.Region);

            regionMappingEntity.HasData(new RegionMapping() {RegionId = 1, Name = "Abruzzo"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 2, Name = "Basilicata"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 3, Name = "Calabria"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 4, Name = "Campania"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 5, Name = "Emilia-Romagna"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 6, Name = "Friuli Venezia Giulia"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 7, Name = "Lazio"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 8, Name = "Liguria"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 9, Name = "Lombardia"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 10, Name = "Marche"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 11, Name = "Molise"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 12, Name = "Piemonte"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 13, Name = "Puglia"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 14, Name = "Sardegna"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 15, Name = "Sicilia"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 16, Name = "Toscana"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 17, Name = "Trentino-Alto Adige"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 18, Name = "Umbria"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 19, Name = "Valle d'Aosta"});
            regionMappingEntity.HasData(new RegionMapping() {RegionId = 20, Name = "Veneto"});
        }

        private static void ConfigureProvinceMapping(ModelBuilder modelBuilder)
        {
            var provinceMappingEntity = modelBuilder.Entity<ProvinceMapping>();

            provinceMappingEntity.ToTable("province_mapping");
            provinceMappingEntity.Property(map => map.Abbreviation)
                .HasColumnName("abbreviation")
                .HasColumnType("VARCHAR(2)");
                //.IsRequired();
            provinceMappingEntity.Property(map => map.Name)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(50)");
            provinceMappingEntity.Property(map => map.Id)
                .HasColumnName("id");
            provinceMappingEntity.HasKey(map => map.Id);
            provinceMappingEntity.HasOne(map => map.Region)
                .WithMany(r => r.Provinces);


            provinceMappingEntity.HasData(new {Id = 1, Name = "Agrigento", Abbreviation = "AG", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 2, Name = "Alessandria", Abbreviation = "AL", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 3, Name = "Ancona", Abbreviation = "AN", RegionId = 10});
            provinceMappingEntity.HasData(new {Id = 4, Name = "Aosta", Abbreviation = "AO", RegionId = 19});
            provinceMappingEntity.HasData(new {Id = 5, Name = "Arezzo", Abbreviation = "AO", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 6, Name = "Ascoli Piceno", Abbreviation = "AP", RegionId = 10});
            provinceMappingEntity.HasData(new {Id = 7, Name = "Asti", Abbreviation = "AT", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 8, Name = "Avellino", Abbreviation = "AV", RegionId = 4});
            provinceMappingEntity.HasData(new {Id = 9, Name = "Bari", Abbreviation = "BA", RegionId = 13});
            provinceMappingEntity.HasData(new
                {Id = 10, Name = "Barletta-Andria-Trani", Abbreviation = "BT", RegionId = 13});
            provinceMappingEntity.HasData(new {Id = 11, Name = "Belluno", Abbreviation = "BL", RegionId = 20});
            provinceMappingEntity.HasData(new {Id = 12, Name = "Benevento", Abbreviation = "BN", RegionId = 4});
            provinceMappingEntity.HasData(new {Id = 13, Name = "Bergamo", Abbreviation = "BG", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 14, Name = "Biella", Abbreviation = "BI", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 15, Name = "Bologna", Abbreviation = "BO", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 16, Name = "Bolzano", Abbreviation = "BZ", RegionId = 17});
            provinceMappingEntity.HasData(new {Id = 17, Name = "Brescia", Abbreviation = "BS", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 18, Name = "Brindisi", Abbreviation = "BR", RegionId = 13});
            provinceMappingEntity.HasData(new {Id = 19, Name = "Cagliari", Abbreviation = "CA", RegionId = 14});
            provinceMappingEntity.HasData(new {Id = 20, Name = "Caltanissetta", Abbreviation = "CL", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 21, Name = "Campobasso", Abbreviation = "CB", RegionId = 11});
            provinceMappingEntity.HasData(new {Id = 22, Name = "Caserta", Abbreviation = "CE", RegionId = 4});
            provinceMappingEntity.HasData(new {Id = 23, Name = "Catania", Abbreviation = "CT", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 24, Name = "Catanzaro", Abbreviation = "CZ", RegionId = 3});
            provinceMappingEntity.HasData(new {Id = 25, Name = "Chieti", Abbreviation = "CH", RegionId = 1});
            provinceMappingEntity.HasData(new {Id = 26, Name = "Como", Abbreviation = "CO", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 27, Name = "Cosenza", Abbreviation = "CS", RegionId = 3});
            provinceMappingEntity.HasData(new {Id = 28, Name = "Cremona", Abbreviation = "CR", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 29, Name = "Crotone", Abbreviation = "KR", RegionId = 3});
            provinceMappingEntity.HasData(new {Id = 30, Name = "Cuneo", Abbreviation = "CN", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 31, Name = "Enna", Abbreviation = "EN", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 32, Name = "Fermo", Abbreviation = "FM", RegionId = 10});
            provinceMappingEntity.HasData(new {Id = 33, Name = "Ferrara", Abbreviation = "FE", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 34, Name = "Firenze", Abbreviation = "FI", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 35, Name = "Foggia", Abbreviation = "FG", RegionId = 13});
            provinceMappingEntity.HasData(new {Id = 36, Name = "Forlì-Cesena", Abbreviation = "FC", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 37, Name = "Frosinone", Abbreviation = "FR", RegionId = 7});
            provinceMappingEntity.HasData(new {Id = 38, Name = "Genova", Abbreviation = "GE", RegionId = 8});
            provinceMappingEntity.HasData(new {Id = 39, Name = "Gorizia", Abbreviation = "GO", RegionId = 6});
            provinceMappingEntity.HasData(new {Id = 40, Name = "Grosseto", Abbreviation = "GR", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 41, Name = "Imperia", Abbreviation = "IM", RegionId = 8});
            provinceMappingEntity.HasData(new {Id = 42, Name = "Isernia", Abbreviation = "IS", RegionId = 11});
            provinceMappingEntity.HasData(new {Id = 43, Name = "L'Aquila", Abbreviation = "AQ", RegionId = 1});
            provinceMappingEntity.HasData(new {Id = 44, Name = "La Spezia", Abbreviation = "SP", RegionId = 8});
            provinceMappingEntity.HasData(new {Id = 45, Name = "Latina", Abbreviation = "LT", RegionId = 7});
            provinceMappingEntity.HasData(new {Id = 46, Name = "Lecce", Abbreviation = "LE", RegionId = 13});
            provinceMappingEntity.HasData(new {Id = 47, Name = "Lecco", Abbreviation = "LC", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 48, Name = "Livorno", Abbreviation = "LI", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 49, Name = "Lodi", Abbreviation = "LO", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 50, Name = "Lucca", Abbreviation = "LU", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 51, Name = "Macerata", Abbreviation = "MC", RegionId = 10});
            provinceMappingEntity.HasData(new {Id = 52, Name = "Mantova", Abbreviation = "MN", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 53, Name = "Massa-Carrara", Abbreviation = "MS", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 54, Name = "Matera", Abbreviation = "MT", RegionId = 2});
            provinceMappingEntity.HasData(new {Id = 55, Name = "Messina", Abbreviation = "ME", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 56, Name = "Milano", Abbreviation = "MI", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 57, Name = "Modena", Abbreviation = "MO", RegionId = 5});
            provinceMappingEntity.HasData(new
                {Id = 58, Name = "Monza e della Brianza", Abbreviation = "MB", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 59, Name = "Napoli", Abbreviation = "NA", RegionId = 4});
            provinceMappingEntity.HasData(new {Id = 60, Name = "Novara", Abbreviation = "NO", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 61, Name = "Nuoro", Abbreviation = "NU", RegionId = 14});
            provinceMappingEntity.HasData(new {Id = 62, Name = "Oristano", Abbreviation = "OR", RegionId = 14});
            provinceMappingEntity.HasData(new {Id = 63, Name = "Padova", Abbreviation = "PD", RegionId = 20});
            provinceMappingEntity.HasData(new {Id = 64, Name = "Palermo", Abbreviation = "PA", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 65, Name = "Parma", Abbreviation = "PR", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 66, Name = "Pavia", Abbreviation = "PV", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 67, Name = "Perugia", Abbreviation = "PG", RegionId = 18});
            provinceMappingEntity.HasData(new {Id = 68, Name = "Pesaro e Urbino", Abbreviation = "PU", RegionId = 10});
            provinceMappingEntity.HasData(new {Id = 69, Name = "Pescara", Abbreviation = "PE", RegionId = 1});
            provinceMappingEntity.HasData(new {Id = 70, Name = "Piacenza", Abbreviation = "PC", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 71, Name = "Pisa", Abbreviation = "PI", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 72, Name = "Pistoia", Abbreviation = "PT", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 73, Name = "Pordenone", Abbreviation = "PN", RegionId = 6});
            provinceMappingEntity.HasData(new {Id = 74, Name = "Potenza", Abbreviation = "PZ", RegionId = 2});
            provinceMappingEntity.HasData(new {Id = 75, Name = "Prato", Abbreviation = "PO", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 76, Name = "Ragusa", Abbreviation = "RG", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 77, Name = "Ravenna", Abbreviation = "RA", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 78, Name = "Reggio Calabria", Abbrevition = "RC", RegionId = 3});
            provinceMappingEntity.HasData(new {Id = 79, Name = "Reggio Emilia", Abbreviation = "RE", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 80, Name = "Rieti", Abbreviation = "RI", RegionId = 7});
            provinceMappingEntity.HasData(new {Id = 81, Name = "Rimini", Abbreviation = "RN", RegionId = 5});
            provinceMappingEntity.HasData(new {Id = 82, Name = "Roma", Abbreviation = "RM", RegionId = 7});
            provinceMappingEntity.HasData(new {Id = 83, Name = "Rovigo", Abbreviation = "RO", RegionId = 20});
            provinceMappingEntity.HasData(new {Id = 84, Name = "Salerno", Abbreviation = "SA", RegionId = 4});
            provinceMappingEntity.HasData(new {Id = 85, Name = "Sassari", Abbreviation = "SS", RegionId = 14});
            provinceMappingEntity.HasData(new {Id = 86, Name = "Savona", Abbreviation = "SV", RegionId = 8});
            provinceMappingEntity.HasData(new {Id = 87, Name = "Siena", Abbreviation = "SI", RegionId = 16});
            provinceMappingEntity.HasData(new {Id = 88, Name = "Siracusa", Abbreviation = "SR", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 89, Name = "Sondrio", Abbreviation = "SO", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 90, Name = "Sud Sardegna", Abbreviation = "SU", RegionId = 14});
            provinceMappingEntity.HasData(new {Id = 91, Name = "Taranto", Abbreviation = "TA", RegionId = 13});
            provinceMappingEntity.HasData(new {Id = 92, Name = "Teramo", Abbreviation = "TE", RegionId = 1});
            provinceMappingEntity.HasData(new {Id = 93, Name = "Terni", Abbreviation = "TR", RegionId = 18});
            provinceMappingEntity.HasData(new {Id = 94, Name = "Torino", Abbreviation = "TO", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 95, Name = "Trapani", Abbreviation = "TP", RegionId = 15});
            provinceMappingEntity.HasData(new {Id = 96, Name = "Trento", Abbreviation = "TN", RegionId = 17});
            provinceMappingEntity.HasData(new {Id = 97, Name = "Treviso", Abbreviation = "TV", RegionId = 20});
            provinceMappingEntity.HasData(new {Id = 98, Name = "Trieste", Abbreviation = "TS", RegionId = 6});
            provinceMappingEntity.HasData(new {Id = 99, Name = "Udine", Abbreviation = "UD", RegionId = 6});
            provinceMappingEntity.HasData(new {Id = 100, Name = "Varese", Abbreviation = "VA", RegionId = 9});
            provinceMappingEntity.HasData(new {Id = 101, Name = "Venezia", Abbreviation = "VE", RegionId = 20});
            provinceMappingEntity.HasData(new
                {Id = 102, Name = "Verbano-Cusio-Ossola", Abbreviation = "VB", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 103, Name = "Vercelli", Abbreviation = "VC", RegionId = 12});
            provinceMappingEntity.HasData(new {Id = 104, Name = "Verona", Abbreviation = "VR", RegionId = 20});
            provinceMappingEntity.HasData(new {Id = 105, Name = "Vibo Valentia", Abbreviation = "VV", RegionId = 3});
            provinceMappingEntity.HasData(new {Id = 106, Name = "Vicenza", Abbreviation = "VI", RegionId = 20});
            provinceMappingEntity.HasData(new {Id = 107, Name = "Viterbo", Abbreviation = "VT", RegionId = 7});
        }

        private static void ConfigurePlaces(ModelBuilder modelBuilder)
        {
            //Places configuration
            var placeEntity = modelBuilder.Entity<Place>();
            placeEntity.ToTable("places");
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

        public DbSet<Place> Places { get; set; }
        public DbSet<RegionMapping> Regions { get; set; }
        public DbSet<ProvinceMapping> Provinces { get; set; }
    }
}