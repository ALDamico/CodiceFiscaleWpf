using System.Collections;
using System.Collections.Generic;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class LanguageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso2Code { get; set; }
        public string Iso3Code { get; set; }
    }
}