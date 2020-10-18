using Microsoft.EntityFrameworkCore.Migrations;

namespace ALD.LibFiscalCode.Persistence.Migrations.SqlServerData
{
    public partial class Mappingseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceMapping_Regions_region_id",
                table: "ProvinceMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Places",
                table: "Places");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProvinceMapping",
                table: "ProvinceMapping");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "places");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "region_mapping");

            migrationBuilder.RenameTable(
                name: "ProvinceMapping",
                newName: "province_mapping");

            migrationBuilder.RenameIndex(
                name: "IX_Places_updated_on",
                table: "places",
                newName: "IX_places_updated_on");

            migrationBuilder.RenameIndex(
                name: "IX_Places_start_date",
                table: "places",
                newName: "IX_places_start_date");

            migrationBuilder.RenameIndex(
                name: "IX_Places_region_name",
                table: "places",
                newName: "IX_places_region_name");

            migrationBuilder.RenameIndex(
                name: "IX_Places_province_abbreviation",
                table: "places",
                newName: "IX_places_province_abbreviation");

            migrationBuilder.RenameIndex(
                name: "IX_Places_end_date",
                table: "places",
                newName: "IX_places_end_date");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceMapping_region_id",
                table: "province_mapping",
                newName: "IX_province_mapping_region_id");

            migrationBuilder.AlterColumn<string>(
                name: "abbreviation",
                table: "province_mapping",
                type: "VARCHAR(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_places",
                table: "places",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_region_mapping",
                table: "region_mapping",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_province_mapping",
                table: "province_mapping",
                column: "id");

            migrationBuilder.InsertData(
                table: "province_mapping",
                columns: new[] { "id", "abbreviation", "name", "region_id" },
                values: new object[,]
                {
                    { 1, "AG", "Agrigento", null },
                    { 80, "RI", "Rieti", null },
                    { 79, "RE", "Reggio Emilia", null },
                    { 78, null, "Reggio Calabria", null },
                    { 77, "RA", "Ravenna", null },
                    { 76, "RG", "Ragusa", null },
                    { 75, "PO", "Prato", null },
                    { 74, "PZ", "Potenza", null },
                    { 73, "PN", "Pordenone", null },
                    { 72, "PT", "Pistoia", null },
                    { 71, "PI", "Pisa", null },
                    { 70, "PC", "Piacenza", null },
                    { 69, "PE", "Pescara", null },
                    { 68, "PU", "Pesaro e Urbino", null },
                    { 67, "PG", "Perugia", null },
                    { 66, "PV", "Pavia", null },
                    { 65, "PR", "Parma", null },
                    { 63, "PD", "Padova", null },
                    { 62, "OR", "Oristano", null },
                    { 61, "NU", "Nuoro", null },
                    { 60, "NO", "Novara", null },
                    { 59, "NA", "Napoli", null },
                    { 58, "MB", "Monza e della Brianza", null },
                    { 57, "MO", "Modena", null },
                    { 81, "RN", "Rimini", null },
                    { 56, "MI", "Milano", null },
                    { 82, "RM", "Roma", null },
                    { 84, "SA", "Salerno", null },
                    { 107, "VT", "Viterbo", null },
                    { 106, "VI", "Vicenza", null },
                    { 105, "VV", "Vibo Valentia", null },
                    { 104, "VR", "Verona", null },
                    { 103, "VC", "Vercelli", null },
                    { 102, "VB", "Verbano-Cusio-Ossola", null },
                    { 101, "VE", "Venezia", null },
                    { 100, "VA", "Varese", null },
                    { 99, "UD", "Udine", null },
                    { 98, "TS", "Trieste", null },
                    { 97, "TV", "Treviso", null },
                    { 96, "TN", "Trento", null },
                    { 95, "TP", "Trapani", null },
                    { 94, "TO", "Torino", null },
                    { 93, "TR", "Terni", null },
                    { 92, "TE", "Teramo", null },
                    { 91, "TA", "Taranto", null },
                    { 90, "SU", "Sud Sardegna", null },
                    { 89, "SO", "Sondrio", null },
                    { 88, "SR", "Siracusa", null },
                    { 87, "SI", "Siena", null },
                    { 86, "SV", "Savona", null },
                    { 85, "SS", "Sassari", null },
                    { 83, "RO", "Rovigo", null },
                    { 55, "ME", "Messina", null },
                    { 64, "PA", "Palermo", null },
                    { 53, "MS", "Massa-Carrara", null },
                    { 25, "CH", "Chieti", null },
                    { 24, "CZ", "Catanzaro", null },
                    { 23, "CT", "Catania", null },
                    { 22, "CE", "Caserta", null },
                    { 21, "CB", "Campobasso", null },
                    { 20, "CL", "Caltanissetta", null },
                    { 19, "CA", "Cagliari", null },
                    { 54, "MT", "Matera", null },
                    { 17, "BS", "Brescia", null },
                    { 16, "BZ", "Bolzano", null },
                    { 15, "BO", "Bologna", null },
                    { 26, "CO", "Como", null },
                    { 14, "BI", "Biella", null },
                    { 12, "BN", "Benevento", null },
                    { 11, "BL", "Belluno", null },
                    { 10, "BT", "Barletta-Andria-Trani", null },
                    { 9, "BA", "Bari", null },
                    { 8, "AV", "Avellino", null },
                    { 7, "AT", "Asti", null },
                    { 6, "AP", "Ascoli Piceno", null },
                    { 5, "AO", "Arezzo", null },
                    { 4, "AO", "Aosta", null },
                    { 3, "AN", "Ancona", null },
                    { 2, "AL", "Alessandria", null },
                    { 13, "BG", "Bergamo", null },
                    { 27, "CS", "Cosenza", null },
                    { 18, "BR", "Brindisi", null },
                    { 29, "KR", "Crotone", null },
                    { 52, "MN", "Mantova", null },
                    { 51, "MC", "Macerata", null },
                    { 28, "CR", "Cremona", null },
                    { 49, "LO", "Lodi", null },
                    { 48, "LI", "Livorno", null },
                    { 47, "LC", "Lecco", null },
                    { 46, "LE", "Lecce", null },
                    { 45, "LT", "Latina", null },
                    { 44, "SP", "La Spezia", null },
                    { 43, "AQ", "L'Aquila", null },
                    { 42, "IS", "Isernia", null },
                    { 50, "LU", "Lucca", null },
                    { 40, "GR", "Grosseto", null },
                    { 41, "IM", "Imperia", null },
                    { 30, "CN", "Cuneo", null },
                    { 32, "FM", "Fermo", null },
                    { 33, "FE", "Ferrara", null },
                    { 34, "FI", "Firenze", null },
                    { 31, "EN", "Enna", null },
                    { 36, "FC", "Forlì-Cesena", null },
                    { 37, "FR", "Frosinone", null },
                    { 38, "GE", "Genova", null },
                    { 39, "GO", "Gorizia", null },
                    { 35, "FG", "Foggia", null }
                });

            migrationBuilder.InsertData(
                table: "region_mapping",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 18, "Umbria" },
                    { 11, "Molise" },
                    { 17, "Trentino-Alto Adige" },
                    { 16, "Toscana" },
                    { 15, "Sicilia" },
                    { 14, "Sardegna" },
                    { 13, "Puglia" },
                    { 12, "Piemonte" },
                    { 10, "Marche" },
                    { 4, "Campania" },
                    { 8, "Liguria" },
                    { 7, "Lazio" },
                    { 6, "Friuli Venezia Giulia" },
                    { 5, "Emilia-Romagna" },
                    { 3, "Calabria" },
                    { 2, "Basilicata" },
                    { 1, "Abruzzo" },
                    { 19, "Valle d'Aosta" },
                    { 9, "Lombardia" },
                    { 20, "Veneto" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_province_mapping_region_mapping_region_id",
                table: "province_mapping",
                column: "region_id",
                principalTable: "region_mapping",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_province_mapping_region_mapping_region_id",
                table: "province_mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_places",
                table: "places");

            migrationBuilder.DropPrimaryKey(
                name: "PK_region_mapping",
                table: "region_mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_province_mapping",
                table: "province_mapping");

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "province_mapping",
                keyColumn: "id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "region_mapping",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.RenameTable(
                name: "places",
                newName: "Places");

            migrationBuilder.RenameTable(
                name: "region_mapping",
                newName: "Regions");

            migrationBuilder.RenameTable(
                name: "province_mapping",
                newName: "ProvinceMapping");

            migrationBuilder.RenameIndex(
                name: "IX_places_updated_on",
                table: "Places",
                newName: "IX_Places_updated_on");

            migrationBuilder.RenameIndex(
                name: "IX_places_start_date",
                table: "Places",
                newName: "IX_Places_start_date");

            migrationBuilder.RenameIndex(
                name: "IX_places_region_name",
                table: "Places",
                newName: "IX_Places_region_name");

            migrationBuilder.RenameIndex(
                name: "IX_places_province_abbreviation",
                table: "Places",
                newName: "IX_Places_province_abbreviation");

            migrationBuilder.RenameIndex(
                name: "IX_places_end_date",
                table: "Places",
                newName: "IX_Places_end_date");

            migrationBuilder.RenameIndex(
                name: "IX_province_mapping_region_id",
                table: "ProvinceMapping",
                newName: "IX_ProvinceMapping_region_id");

            migrationBuilder.AlterColumn<string>(
                name: "abbreviation",
                table: "ProvinceMapping",
                type: "VARCHAR(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(2)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Places",
                table: "Places",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProvinceMapping",
                table: "ProvinceMapping",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceMapping_Regions_region_id",
                table: "ProvinceMapping",
                column: "region_id",
                principalTable: "Regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
