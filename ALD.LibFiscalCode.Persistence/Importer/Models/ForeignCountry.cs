using ALD.LibFiscalCode.Persistence.Importer.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer.Models
{
    public class ForeignCountry
    {
        public string GeographicEntityType { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentNameIt { get; set; }
        public string AreaCode { get; set; }
        public string AreaNameIt { get; set; }
        public string IstatCode { get; set; }
        public string NameIt { get; set; }
        public string NameEn { get; set; }
        public string MinCode { get; set; }
        public string AtCode
        {
            get => atCode;
            set => atCode = ForeignCountryHelpers.CommonSetter(value);
        }
        private string atCode;
        public string Unsd_M49Code
        {
            get => unsd_M49Code;
            set => unsd_M49Code = ForeignCountryHelpers.CommonSetter(value);
        }
        private string unsd_M49Code;
        public string Iso3166Alpha2Code { get; set; }
        public string Iso3166Alpha3Code { get; set; }
        public string IstatOwnerCountryCode { get; set; }
        
        public string Iso3166Alpha3OwnerCountryCode { get; set; }
        private static class ForeignCountryHelpers
        {
            public static string CommonSetter(string value)
            {
                if (value == "n.d")
                {
                    return null;
                }
                return value;
            }

            public static int? CommonIntSetter(string value)
            {
                if (value == "n.d")
                {
                    return null;
                }
                _ = int.TryParse(value, out var number);
                int? output = number;
                return output;
            }
        }
    }


}
