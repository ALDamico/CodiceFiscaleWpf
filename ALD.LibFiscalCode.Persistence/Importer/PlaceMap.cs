using System;
using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper.Configuration;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    internal class PlaceMap:ClassMap<Place>
    {
        public PlaceMap()
        {
            Map(p => p.Name).Name("name");
            Map(p => p.Province).Name("province_name");
            Map(p => p.ProvinceAbbreviation).Name("province_abbreviation");
            Map(p => p.Region).Name("region_name");
            Map(p => p.Code).Name("code");
        }
    }
}
