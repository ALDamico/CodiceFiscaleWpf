using System;
using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Persistence.Importer.Enums;

namespace ALD.LibFiscalCode.Persistence.Importer.Models
{
    internal class FormerForeignCountry
    {
        public int? YearOccurred { get; set; }
        public string GeographicEntityType { get; set; }
        public string ContinentCode { get; set; }
        public string IstatCode { get; set; }
        public string AtCode { get; set; }
        public string Iso3166Alpha2Code { get; set; }
        public string Iso3166Alpha3Code { get; set; }
        public string Name { get; set; }
        public string ChildCode { get; set; }
        public string ChildName { get; set; }
    }
}
