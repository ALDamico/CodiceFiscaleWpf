using ALD.LibFiscalCode.Persistence.Importer.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters
{
    class FormerForeignCountryMap: ClassMap<FormerForeignCountry>
    {
        public FormerForeignCountryMap()
        {
            base.Map(c => c.YearOccurred).Name("Anno evento");
            base.Map(c => c.GeographicEntityType).Name("Stato(S)/Territorio(T)");
            base.Map(c => c.ContinentCode).Name("Codice Continente (a)");
            base.Map(c => c.IstatCode).Name("Codice ISTAT");
            base.Map(c => c.AtCode).Name("Codice AT");
            base.Map(c => c.Iso3166Alpha2Code).Name("Codice ISO 3166 alpha2");
            base.Map(c => c.Iso3166Alpha3Code).Name("Codice ISO 3166 alpha3");
            base.Map(c => c.Name).Name("Denominazione (b)");
            base.Map(c => c.ChildCode).Name("Codice Stato/Territorio_Figlio");
            base.Map(c => c.ChildName).Name("Denominazione Stato/Territorio Figlio ");
        }
    }
}


