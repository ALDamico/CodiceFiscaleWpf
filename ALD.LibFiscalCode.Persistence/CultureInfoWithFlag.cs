using System.Globalization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence
{
    public class CultureInfoWithFlag
    {
        public static CultureInfoWithFlag FromLanguageInfo(LanguageInfo source)
        {
            var instance = new CultureInfoWithFlag();
            instance.Culture = new CultureInfo(source.Iso2Code);
            instance.ImagePath = source.ImagePath;
            return instance;
        }
        public CultureInfo Culture { get; set; }
        public string ImagePath { get; set; }
    }
}