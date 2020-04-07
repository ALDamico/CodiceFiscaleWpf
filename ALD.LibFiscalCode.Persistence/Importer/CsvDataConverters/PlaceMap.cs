using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper.Configuration;
using System;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    // This class represents an older map for converting a CSV stream to a Place object.
    // New developments will use the AnprPlaceMap class instead, which is to be used to import data from the ANPR portal
    // https://www.anpr.interno.it/portale/tabelle-di-riferimento
    [Obsolete]
    internal class PlaceMap : ClassMap<Place>
    {
        public PlaceMap()
        {
            base.Map(p => p.Name).Name("name");
            base.Map(p => p.Province).Name("province_name");
            base.Map(p => p.ProvinceAbbreviation).Name("province_abbreviation");
            base.Map(p => p.Region).Name("region_name");
            base.Map(p => p.Code).Name("code");
        }
    }
}