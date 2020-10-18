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

            regionMappingEntity.HasData(new RegionMapping() {Id = 1, Name = "Abruzzo"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 2, Name = "Basilicata"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 3, Name = "Calabria"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 4, Name = "Campania"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 5, Name = "Emilia-Romagna"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 6, Name = "Friuli Venezia Giulia"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 7, Name = "Lazio"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 8, Name = "Liguria"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 9, Name = "Lombardia"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 10, Name = "Marche"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 11, Name = "Molise"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 12, Name = "Piemonte"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 13, Name = "Puglia"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 14, Name = "Sardegna"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 15, Name = "Sicilia"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 16, Name = "Toscana"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 17, Name = "Trentino-Alto Adige"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 18, Name = "Umbria"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 19, Name = "Valle d'Aosta"});
            regionMappingEntity.HasData(new RegionMapping() {Id = 20, Name = "Veneto"});
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
                .WithMany(r => r.Provinces)
                .HasForeignKey("region_id");


            provinceMappingEntity.HasData(new {Id = 1, Name = "Agrigento", Abbreviation = "AG", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 2, Name = "Alessandria", Abbreviation = "AL", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 3, Name = "Ancona", Abbreviation = "AN", Region_Id = 10});
            provinceMappingEntity.HasData(new {Id = 4, Name = "Aosta", Abbreviation = "AO", Region_Id = 19});
            provinceMappingEntity.HasData(new {Id = 5, Name = "Arezzo", Abbreviation = "AO", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 6, Name = "Ascoli Piceno", Abbreviation = "AP", Region_Id = 10});
            provinceMappingEntity.HasData(new {Id = 7, Name = "Asti", Abbreviation = "AT", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 8, Name = "Avellino", Abbreviation = "AV", Region_Id = 4});
            provinceMappingEntity.HasData(new {Id = 9, Name = "Bari", Abbreviation = "BA", Region_Id = 13});
            provinceMappingEntity.HasData(new
                {Id = 10, Name = "Barletta-Andria-Trani", Abbreviation = "BT", Region_Id = 13});
            provinceMappingEntity.HasData(new {Id = 11, Name = "Belluno", Abbreviation = "BL", Region_Id = 20});
            provinceMappingEntity.HasData(new {Id = 12, Name = "Benevento", Abbreviation = "BN", Region_Id = 4});
            provinceMappingEntity.HasData(new {Id = 13, Name = "Bergamo", Abbreviation = "BG", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 14, Name = "Biella", Abbreviation = "BI", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 15, Name = "Bologna", Abbreviation = "BO", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 16, Name = "Bolzano", Abbreviation = "BZ", Region_Id = 17});
            provinceMappingEntity.HasData(new {Id = 17, Name = "Brescia", Abbreviation = "BS", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 18, Name = "Brindisi", Abbreviation = "BR", Region_Id = 13});
            provinceMappingEntity.HasData(new {Id = 19, Name = "Cagliari", Abbreviation = "CA", Region_Id = 14});
            provinceMappingEntity.HasData(new {Id = 20, Name = "Caltanissetta", Abbreviation = "CL", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 21, Name = "Campobasso", Abbreviation = "CB", Region_Id = 11});
            provinceMappingEntity.HasData(new {Id = 22, Name = "Caserta", Abbreviation = "CE", Region_Id = 4});
            provinceMappingEntity.HasData(new {Id = 23, Name = "Catania", Abbreviation = "CT", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 24, Name = "Catanzaro", Abbreviation = "CZ", Region_Id = 3});
            provinceMappingEntity.HasData(new {Id = 25, Name = "Chieti", Abbreviation = "CH", Region_Id = 1});
            provinceMappingEntity.HasData(new {Id = 26, Name = "Como", Abbreviation = "CO", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 27, Name = "Cosenza", Abbreviation = "CS", Region_Id = 3});
            provinceMappingEntity.HasData(new {Id = 28, Name = "Cremona", Abbreviation = "CR", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 29, Name = "Crotone", Abbreviation = "KR", Region_Id = 3});
            provinceMappingEntity.HasData(new {Id = 30, Name = "Cuneo", Abbreviation = "CN", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 31, Name = "Enna", Abbreviation = "EN", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 32, Name = "Fermo", Abbreviation = "FM", Region_Id = 10});
            provinceMappingEntity.HasData(new {Id = 33, Name = "Ferrara", Abbreviation = "FE", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 34, Name = "Firenze", Abbreviation = "FI", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 35, Name = "Foggia", Abbreviation = "FG", Region_Id = 13});
            provinceMappingEntity.HasData(new {Id = 36, Name = "Forlì-Cesena", Abbreviation = "FC", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 37, Name = "Frosinone", Abbreviation = "FR", Region_Id = 7});
            provinceMappingEntity.HasData(new {Id = 38, Name = "Genova", Abbreviation = "GE", Region_Id = 8});
            provinceMappingEntity.HasData(new {Id = 39, Name = "Gorizia", Abbreviation = "GO", Region_Id = 6});
            provinceMappingEntity.HasData(new {Id = 40, Name = "Grosseto", Abbreviation = "GR", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 41, Name = "Imperia", Abbreviation = "IM", Region_Id = 8});
            provinceMappingEntity.HasData(new {Id = 42, Name = "Isernia", Abbreviation = "IS", Region_Id = 11});
            provinceMappingEntity.HasData(new {Id = 43, Name = "L'Aquila", Abbreviation = "AQ", Region_Id = 1});
            provinceMappingEntity.HasData(new {Id = 44, Name = "La Spezia", Abbreviation = "SP", Region_Id = 8});
            provinceMappingEntity.HasData(new {Id = 45, Name = "Latina", Abbreviation = "LT", Region_Id = 7});
            provinceMappingEntity.HasData(new {Id = 46, Name = "Lecce", Abbreviation = "LE", Region_Id = 13});
            provinceMappingEntity.HasData(new {Id = 47, Name = "Lecco", Abbreviation = "LC", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 48, Name = "Livorno", Abbreviation = "LI", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 49, Name = "Lodi", Abbreviation = "LO", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 50, Name = "Lucca", Abbreviation = "LU", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 51, Name = "Macerata", Abbreviation = "MC", Region_Id = 10});
            provinceMappingEntity.HasData(new {Id = 52, Name = "Mantova", Abbreviation = "MN", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 53, Name = "Massa-Carrara", Abbreviation = "MS", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 54, Name = "Matera", Abbreviation = "MT", Region_Id = 2});
            provinceMappingEntity.HasData(new {Id = 55, Name = "Messina", Abbreviation = "ME", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 56, Name = "Milano", Abbreviation = "MI", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 57, Name = "Modena", Abbreviation = "MO", Region_Id = 5});
            provinceMappingEntity.HasData(new
                {Id = 58, Name = "Monza e della Brianza", Abbreviation = "MB", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 59, Name = "Napoli", Abbreviation = "NA", Region_Id = 4});
            provinceMappingEntity.HasData(new {Id = 60, Name = "Novara", Abbreviation = "NO", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 61, Name = "Nuoro", Abbreviation = "NU", Region_Id = 14});
            provinceMappingEntity.HasData(new {Id = 62, Name = "Oristano", Abbreviation = "OR", Region_Id = 14});
            provinceMappingEntity.HasData(new {Id = 63, Name = "Padova", Abbreviation = "PD", Region_Id = 20});
            provinceMappingEntity.HasData(new {Id = 64, Name = "Palermo", Abbreviation = "PA", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 65, Name = "Parma", Abbreviation = "PR", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 66, Name = "Pavia", Abbreviation = "PV", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 67, Name = "Perugia", Abbreviation = "PG", Region_Id = 18});
            provinceMappingEntity.HasData(new {Id = 68, Name = "Pesaro e Urbino", Abbreviation = "PU", Region_Id = 10});
            provinceMappingEntity.HasData(new {Id = 69, Name = "Pescara", Abbreviation = "PE", Region_Id = 1});
            provinceMappingEntity.HasData(new {Id = 70, Name = "Piacenza", Abbreviation = "PC", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 71, Name = "Pisa", Abbreviation = "PI", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 72, Name = "Pistoia", Abbreviation = "PT", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 73, Name = "Pordenone", Abbreviation = "PN", Region_Id = 6});
            provinceMappingEntity.HasData(new {Id = 74, Name = "Potenza", Abbreviation = "PZ", Region_Id = 2});
            provinceMappingEntity.HasData(new {Id = 75, Name = "Prato", Abbreviation = "PO", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 76, Name = "Ragusa", Abbreviation = "RG", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 77, Name = "Ravenna", Abbreviation = "RA", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 78, Name = "Reggio Calabria", Abbrevition = "RC", Region_Id = 3});
            provinceMappingEntity.HasData(new {Id = 79, Name = "Reggio Emilia", Abbreviation = "RE", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 80, Name = "Rieti", Abbreviation = "RI", Region_Id = 7});
            provinceMappingEntity.HasData(new {Id = 81, Name = "Rimini", Abbreviation = "RN", Region_Id = 5});
            provinceMappingEntity.HasData(new {Id = 82, Name = "Roma", Abbreviation = "RM", Region_Id = 7});
            provinceMappingEntity.HasData(new {Id = 83, Name = "Rovigo", Abbreviation = "RO", Region_Id = 20});
            provinceMappingEntity.HasData(new {Id = 84, Name = "Salerno", Abbreviation = "SA", Region_Id = 4});
            provinceMappingEntity.HasData(new {Id = 85, Name = "Sassari", Abbreviation = "SS", Region_Id = 14});
            provinceMappingEntity.HasData(new {Id = 86, Name = "Savona", Abbreviation = "SV", Region_Id = 8});
            provinceMappingEntity.HasData(new {Id = 87, Name = "Siena", Abbreviation = "SI", Region_Id = 16});
            provinceMappingEntity.HasData(new {Id = 88, Name = "Siracusa", Abbreviation = "SR", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 89, Name = "Sondrio", Abbreviation = "SO", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 90, Name = "Sud Sardegna", Abbreviation = "SU", Region_Id = 14});
            provinceMappingEntity.HasData(new {Id = 91, Name = "Taranto", Abbreviation = "TA", Region_Id = 13});
            provinceMappingEntity.HasData(new {Id = 92, Name = "Teramo", Abbreviation = "TE", Region_Id = 1});
            provinceMappingEntity.HasData(new {Id = 93, Name = "Terni", Abbreviation = "TR", Region_Id = 18});
            provinceMappingEntity.HasData(new {Id = 94, Name = "Torino", Abbreviation = "TO", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 95, Name = "Trapani", Abbreviation = "TP", Region_Id = 15});
            provinceMappingEntity.HasData(new {Id = 96, Name = "Trento", Abbreviation = "TN", Region_Id = 17});
            provinceMappingEntity.HasData(new {Id = 97, Name = "Treviso", Abbreviation = "TV", Region_Id = 20});
            provinceMappingEntity.HasData(new {Id = 98, Name = "Trieste", Abbreviation = "TS", Region_Id = 6});
            provinceMappingEntity.HasData(new {Id = 99, Name = "Udine", Abbreviation = "UD", Region_Id = 6});
            provinceMappingEntity.HasData(new {Id = 100, Name = "Varese", Abbreviation = "VA", Region_Id = 9});
            provinceMappingEntity.HasData(new {Id = 101, Name = "Venezia", Abbreviation = "VE", Region_Id = 20});
            provinceMappingEntity.HasData(new
                {Id = 102, Name = "Verbano-Cusio-Ossola", Abbreviation = "VB", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 103, Name = "Vercelli", Abbreviation = "VC", Region_Id = 12});
            provinceMappingEntity.HasData(new {Id = 104, Name = "Verona", Abbreviation = "VR", Region_Id = 20});
            provinceMappingEntity.HasData(new {Id = 105, Name = "Vibo Valentia", Abbreviation = "VV", Region_Id = 3});
            provinceMappingEntity.HasData(new {Id = 106, Name = "Vicenza", Abbreviation = "VI", Region_Id = 20});
            provinceMappingEntity.HasData(new {Id = 107, Name = "Viterbo", Abbreviation = "VT", Region_Id = 7});
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

        public DbSet<Place> Places { get; }
        public DbSet<RegionMapping> Regions { get; }
        public DbSet<ProvinceMapping> Provinces { get; }
    }
}