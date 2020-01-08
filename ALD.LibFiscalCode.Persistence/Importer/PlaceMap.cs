using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper.Configuration;

namespace ALD.LibFiscalCode.Persistence.Importer
{
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