using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Factories;
using ALD.LibFiscalCode.Persistence.Sqlite;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class LanguageInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso2Code { get; set; }
        public string Iso3Code { get; set; }

       public string ImagePath { get; set; }
        public override string ToString()
        {
            return Name;
        }

        public static implicit operator CultureInfo(LanguageInfo par)
        {
            return CultureInfoFactory.GetCultureInfoFromLanguageInfo(par);
        }

        public static implicit operator LanguageInfo(CultureInfo par)
        {
            var isoCode = par.Name;
            if (isoCode.Length != 2)
            {
                isoCode = isoCode.Substring(0, 2);
            }

            using var db = new AppDataContext();
            return db.Languages.FirstOrDefault(l => l.Iso2Code.Equals(isoCode));
        }
    }
}