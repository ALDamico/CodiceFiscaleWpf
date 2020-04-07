using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.TypeConversion;
using System.Globalization;
using CsvHelper;
using ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    /*
     * Mapping
     * ID: Identificativo ANPR di un singolo record della tabella dei comuni
     * DATAISTITUZIONE: Data d'inizio validità amministrativa del set di dati del record della tabella dei comuni nel formato ISO YYYY-MM-DD 
     * (DD numero del giorno del mese in due cifre, MM numero del mese in due cifre, YYYY anno in quattro cifre)
     * DATACESSAZIONE: Data di fine validità amministrativa del set di dati del record della tabella dei comuni nel formato ISO YYYY-MM-DD 
     * (DD numero del giorno del mese in due cifre, MM numero del mese in due cifre, YYYY anno in quattro cifre)
     * CODISTAT: Codice identificativo del comune attribuito da Istat nell'intervallo temporale tra la DATAISTITUZIONE e la DATACESSAZIONE
     * CODCATASTALE: Codice attribuito all'unità territoriale del comune dall'Agenzia delle Entrate
     * DENOMINAZIONE_IT: Denominazione ufficiale in lingua Italiana del comune nell'intervallo temporale tra la DATAISTITUZIONE e la DATACESSAZIONE
     * DENOMTRASLITTERATA: Traslitterazione di DENOMINAZIONE_IT secondo quanto stabilito dal decreto Brunetta del 02 Febbraio 2009 
     * (GU n.56 del 9-3-2009) 
     * ALTRADENOMINAZIONE: Denominazione in una seconda lingua ufficiale per il comune nell'intervallo temporale tra la DATAISTITUZIONE e la 
     * DATACESSAZIONE
     * ALTRADENOMTRASLITTERATA: Traslitterazione di ALTRADENOMINAZIONE secondo quanto stabilito dal decreto Brunetta del 02 Febbraio 2009 
     * (GU n.56 del 9-3-2009) 
     * IDPROVINCIA: Identificativo ANPR delle province italiane (comprese quelle non più esistenti o modificate)
     * IDPROVINCIAISTAT: Codice assegnato da Istat alle province italiane
     * IDREGIONE: Identificativo Istat delle regioni italiane o valore EX per "REGIONE ORMAI SOPPRESSA" e 00 per "REGIONE NON DISPONIBILE"
     * IDPREFETTURA: Identificativo ANPR delle prefetture italiane (valorizzato solo per i comuni attivi)
     * STATO: Stato del record della tabella dei comuni: A=ATTIVO, C=CESSATO, D=DA ISTITUIRE
     * SIGLAPROVINCIA: E' la sigla automobilistica della provincia
     * FONTE: la fonte del dato:  Valori: I=Istat, AE=Agenzia delle Entrate; null=non specificato
     * DATAULTIMOAGG: Data di ultimo aggiornamento del record della tabella dei comuni nel formato ISO YYYY-MM-DD (DD numero del giorno del 
     * mese in due cifre, MM numero del mese in due cifre, YYYY anno in quattro cifre)
     * COD_DENOM: Codice assegnato da Istat alle denominazioni dei comuni non più in uso
     */
    internal class AnprPlaceMap : ClassMap<Place>
    {
        public AnprPlaceMap()
        {
            TypeConverterCache cache = new TypeConverterCache();
           
            DateTimeConverter dateTimeConverter = new AnprDateConverter();
            cache.AddConverter<DateTime>(dateTimeConverter);

            ITypeConverter typeConverter = new NullableConverter(typeof(Nullable<DateTime>), cache);
            base.Map(p => p.Name).Name("DENOMINAZIONE_IT");
            base.Map(p => p.ProvinceAbbreviation).Name("SIGLAPROVINCIA");
            base.Map(p => p.Province).ConvertUsing(r => ProvinceAbbreviationToProvinceMapping[r.GetField("SIGLAPROVINCIA")]).Name("SIGLAPROVINCIA");
            base.Map(p => p.Code).Name("CODCATASTALE");
            base.Map(p => p.Region).ConvertUsing(r => ProvinceAbbreviationToRegionMapping[r.GetField("SIGLAPROVINCIA")]).Name("SIGLAPROVINCIA");
            base.Map(p => p.StartDate).Name("DATAISTITUZIONE").TypeConverter<AnprDateConverter>();
            base.Map(p => p.EndDate).Name("DATACESSAZIONE").TypeConverter<AnprDateConverter>();
        }

        private static readonly Dictionary<string, string> ProvinceAbbreviationToProvinceMapping = new Dictionary<string, string>
        {
           {"AG", "Agrigento" },
            {"AL", "Alessandria" },
            {"AN", "Ancona" },
            {"AO", "Aosta" },
            {"AP", "Ascoli Piceno" },
            {"AQ", "L'Aquila" },
            {"AR", "Arezzo" },
            {"AT", "Asti" },
            {"AV", "Avellino" },
            {"BA", "Bari" },
            {"BG", "Bergamo" },
            {"BI", "Biella" },
            {"BL", "Belluno" },
            {"BN", "Benevento" },
            {"BO", "Bologna" },
            {"BR", "Brindisi" },
            {"BS", "Brescia" },
            {"BT",  "Barletta-Andria-Trani" },
            {"BZ", "Bolzano" },
            {"CA", "Cagliari" },
            {"CB", "Campobasso" },
            {"CE", "Caserta" },
            {"CH", "Chieti" },
            {"CI",  "Carbonia-Iglesias" },
            {"CL", "Caltanissetta" },
            {"CN", "Cuneo" },
            {"CO", "Como" },
            {"CR", "Cremona" },
            {"CS", "Cosenza" },
            {"CT", "Catania" },
            {"CZ", "Catanzaro" },
            {"EN", "Enna" },
            {"FC", "Forlì-Cesena" },
            {"FE", "Ferrara" },
            {"FG", "Foggia" },
            {"FI", "Firenze" },
            {"FM", "Fermo" },
            {"FO", "Forlì" },
            {"FR", "Frosinone" },
            {"FU", "Fiume" },
            {"GE", "Genova" },
            {"GO", "Gorizia" },
            {"GR", "Grosseto" },
            {"IM", "Imperia" },
            {"IS", "Isernia" },
            {"KR", "Crotone" },
            {"LC", "Lecco" },
            {"LE", "Lecce" },
            {"LI", "Livorno" },
            {"LO", "Lodi" },
            {"LU", "Lucca" },
            {"LT", "Latina" },
            {"MB", "Monza-Brianza" },
            {"MC", "Macerata" },
            {"ME", "Messina" },
            {"MI", "Milano" },
            {"MN", "Mantova" },
            {"MO", "Modena" },
            {"MS", "Massa-Carrara" },
            {"MT", "Matera" },
            {"NA", "Napoli" },
            {"NO", "Novara" },
            {"NU", "Nuoro" },
            {"OG", "Ogliastra" },
            {"OR", "Oristano" },
            {"OT", "Olbia Tempio" },
            {"PA", "Palermo" },
            {"PC", "Piacenza" },
            {"PD", "Pordenone" },
            {"PE", "Pescara" },
            {"PG", "Perugia" },
            {"PI", "Pisa" },
            {"PL", "Pola" },
            {"PN", "Pordenone" },
            {"PO", "Prato" },
            {"PR", "Parma" },
            {"PS", "Pesaro" },
            {"PT", "Pistoia" },
            {"PU", "Pesaro e Urbino" },
            {"PV", "Pavia" },
            {"PZ", "Potenza" },
            {"RA", "Ravenna" },
            {"RC", "Reggio di Calabria" },
            {"RE", "Reggio nell'Emilia" },
            {"RG", "Ragusa" },
            {"RI", "Rieti" },
            {"RM", "Roma" },
            {"RN",  "Rimini" },
            {"RO", "Rovigo" },
            {"SA", "Salerno" },
            {"SI", "Siena" },
            {"SO", "Sondrio" },
            {"SP", "La Spezia" },
            {"SR", "Siracusa" },
            {"SS", "Sassari" },
            {"SU", "Sud Sardegna" },
            {"SV", "Savona" },
            {"TA", "Taranto" },
            {"TE", "Teramo" },
            {"TN", "Trento" },
            {"TO", "Torino" },
            {"TP", "Trapani" },
            {"TR", "Terni" },
            {"TS", "Trieste" },
            {"TV", "Treviso" },
            {"UD", "Udine" },
            {"VA", "Varese" },
            {"VB", "Verbania" },
            {"VC", "Vercelli" },
            {"VE", "Venezia" },
            {"VG", "?" }, //Forse si tratta di un refuso. Non dovrebbe comunque essere un problema
            {"VI", "Vicenza" },
            {"VR", "Verona" },
            {"VS", "Medio Campidano" },
            {"VT", "Viterbo" },
            {"VV", "Vibo Valentia" },
            {"ZA", "Zara" }
        };

        //TODO Completare
        private static readonly Dictionary<String, String> ProvinceAbbreviationToRegionMapping = new Dictionary<string, string>
        {
           {"AG", "Sicilia" },
            {"AL", "Piemonte" },
            {"AN", "Marche" },
            {"AO", "Valle d'Aosta" },
            {"AP", "Marche" },
            {"AQ", "Abruzzo" },
            {"AR", "Toscana" },
            {"AT", "Piemonte" },
            {"AV", "Campania" },
            {"BA", "Puglia" },
            {"BG", "Lombardia" },
            {"BI", "Piemonte" },
            {"BL", "Veneto" },
            {"BN", "Campania" },
            {"BO", "Emilia-Romagna" },
            {"BR", "Puglia" },
            {"BS", "Lombardia" },
            {"BT",  "Puglia" },
            {"BZ", "Trentino-Alto Adige" },
            {"CA", "Sardegna" },
            {"CB", "Molise" },
            {"CE", "Campania" },
            {"CH", "Abruzzo" },
            {"CI",  "Sardegna" },
            {"CL", "Sicilia" },
            {"CN", "Piemonte" },
            {"CO", "Lombardia" },
            {"CR", "Lombardia" },
            {"CS", "Calabria" },
            {"CT", "Sicilia" },
            {"CZ", "Calabria" },
            {"EN", "Sicilia" },
            {"FC", "Emilia-Romagna" },
            {"FE", "Emilia-Romagna" },
            {"FG", "Puglia" },
            {"FI", "Toscana" },
            {"FM", "Marche" },
            {"FO", "Emilia-Romagna" },
            {"FR", "Lazio" },
            {"FU", "Friuli-Venezia Giulia" },
            {"GE", "Liguria" },
            {"GO", "Friuli-Venezia Giulia" },
            {"GR", "Toscana" },
            {"IM", "Liguria" },
            {"IS", "Molise" },
            {"KR", "Calabria" },
            {"LC", "Lombardia" },
            {"LE", "Puglia" },
            {"LI", "Toscana" },
            {"LO", "Lombardia" },
            {"LU", "Toscana" },
            {"LT", "Lazio" },
            {"MB", "Lombardia" },
            {"MC", "Marche" },
            {"ME", "Sicilia" },
            {"MI", "Lombardia" },
            {"MN", "Lombardia" },
            {"MO", "Emilia-Romagna" },
            {"MS", "Toscana" },
            {"MT", "Basilicata" },
            {"NA", "Campania" },
            {"NO", "Piemonte" },
            {"NU", "Sardegna" },
            {"OG", "Sardegna" },
            {"OR", "Sardegna" },
            {"OT", "Sardegna" },
            {"PA", "Sicilia" },
            {"PC", "Emilia-Romagna" },
            {"PD", "Friuli-Venezia Giulia" },
            {"PE", "Abruzzo" },
            {"PG", "Umbria" },
            {"PI", "Toscana" },
            {"PL", "Friuli-Venezia Giulia" },
            {"PN", "Friuli-Venezia Giulia" },
            {"PO", "Toscana" },
            {"PR", "Emilia-Romagna" },
            {"PS", "Marche" },
            {"PT", "Toscana" },
            {"PU", "Marche" },
            {"PV", "Lombardia" },
            {"PZ", "Basilicata" },
            {"RA", "Emilia-Romagna" },
            {"RC", "Calabria" },
            {"RE", "Emilia-Romagna" },
            {"RG", "Sicilia" },
            {"RI", "Lazio" },
            {"RM", "Lazio" },
            {"RN",  "Emilia-Romagna" },
            {"RO", "Veneto" },
            {"SA", "Campania" },
            {"SI", "Toscana" },
            {"SO", "Lombardia" },
            {"SP", "Liguria" },
            {"SR", "Sicilia" },
            {"SS", "Sardegna" },
            {"SU", "Sardegna" },
            {"SV", "Liguria" },
            {"TA", "Puglia" },
            {"TE", "Abruzzo" },
            {"TN", "Trentino-Alto Adige" },
            {"TO", "Piemonte" },
            {"TP", "Sicilia" },
            {"TR", "Umbria" },
            {"TS", "Friuli-Venezia Giulia" },
            {"TV", "Veneto" },
            {"UD", "Friuli-Venezia Giulia" },
            {"VA", "Lombardia" },
            {"VB", "Piemonte" },
            {"VC", "Piemonte" },
            {"VE", "Veneto" },
            {"VG", "?" }, //Forse si tratta di un refuso. Non dovrebbe comunque essere un problema
            {"VI", "Veneto" },
            {"VR", "Veneto" },
            {"VT", "Lazio" },
            {"VS", "Sardegna" },
            {"VV", "Calabria" },
            {"ZA", "Friuli-Venezia Giulia" }
        };

    }
}
