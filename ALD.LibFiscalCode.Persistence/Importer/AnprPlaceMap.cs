using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper.TypeConversion;

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
    internal class AnprPlaceMap: ClassMap<Place>
    {
        public AnprPlaceMap()
        {
            TypeConverterCache cache = new TypeConverterCache();
            cache.AddConverter<DateTime>(new DateTimeConverter());
            ITypeConverter typeConverter = new NullableConverter(typeof(DateTime?), cache);
            base.Map(p => p.Name).Name("DENOMINAZIONE_IT");
            base.Map(p => p.ProvinceAbbreviation).Name("SIGLAPROVINCIA");
            base.Map(p => p.Province).Name("SIGLAPROVINCIA").ConvertUsing(r => ProvinceAbbreviationToProvinceMapping[r.ProvinceAbbreviation]);
            base.Map(p => p.Code).Name("CODCATASTALE");
            base.Map(p => p.Region).Name("SIGLAPROVINCIA").ConvertUsing(r => ProvinceAbbreviationToRegionMapping[r.ProvinceAbbreviation]);
            base.Map(p => p.StartDate).Name("DATAISTITUZIONE").TypeConverter(typeConverter);
            base.Map(p => p.EndDate).Name("DATACESSAZIONE").TypeConverter(typeConverter);
            // TODO Mappare le province alle sigle
            //base.Map(p => p.)
            // TODO Mappare le regioni alle sigle

        }

        private static readonly Dictionary<String, String> ProvinceAbbreviationToProvinceMapping = new Dictionary<string, string>
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
            {"VC", "Vercelli" },
            {"VE", "Venezia" },
            //{"VG", "?" } //Forse si tratta di un refuso. Non dovrebbe comunque essere un problema
            {"VI", "Vicenza" },
            {"VR", "Verona" },
            {"VT", "Viterbo" },
            {"VV", "Vibo Valentia" },
            {"ZA", "Zara" }
        };

        //TODO Completare
        private static readonly Dictionary<String, String> ProvinceAbbreviationToRegionMapping = new Dictionary<string, string>
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
            {"VC", "Vercelli" },
            {"VE", "Venezia" },
            //{"VG", "?" } //Forse si tratta di un refuso. Non dovrebbe comunque essere un problema
            {"VI", "Vicenza" },
            {"VR", "Verona" },
            {"VT", "Viterbo" },
            {"VV", "Vibo Valentia" },
            {"ZA", "Zara" }
        };

    }
}
