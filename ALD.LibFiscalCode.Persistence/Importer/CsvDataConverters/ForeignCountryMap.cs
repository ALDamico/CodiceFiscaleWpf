using ALD.LibFiscalCode.Persistence.Importer.Enums;
using ALD.LibFiscalCode.Persistence.Importer.Models;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters
{
    internal class ForeignCountryMap: ClassMap<ForeignCountry>
    {
        public ForeignCountryMap()
        {
            base.Map(c => c.GeographicEntityType).Name("Stato(S)/Territorio(T)");
            base.Map(c => c.ContinentCode).Name("Codice Continente");
            base.Map(c => c.ContinentNameIt).Name("Denominazione Continente (IT)");
            base.Map(c => c.AreaCode).Name("Codice Area");
            base.Map(c => c.AreaNameIt).Name("Denominazione Area (IT)");
            base.Map(c => c.IstatCode).Name("Codice ISTAT");
            base.Map(c => c.NameIt).Name("Denominazione IT");
            base.Map(c => c.NameEn).Name("Denominazione EN");
            base.Map(c => c.MinCode).Name("Codice MIN");
            base.Map(c => c.AtCode).Name("Codice AT");
            base.Map(c => c.Unsd_M49Code).Name("Codice UNSD_M49");
            base.Map(c => c.Iso3166Alpha2Code).Name("Codice ISO 3166 alpha2");
            base.Map(c => c.Iso3166Alpha3Code).Name("Codice ISO 3166 alpha3");
            base.Map(c => c.IstatOwnerCountryCode).Name("Codice ISTAT_Stato Padre");
            base.Map(c => c.Iso3166Alpha3OwnerCountryCode).Name("Codice ISO alpha3_Stato Padre");
        }
    }
}
