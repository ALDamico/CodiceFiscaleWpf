using System.Globalization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence.Factories
{
    public static class CultureInfoFactory
    {
        private static readonly CultureInfo defaultLanguage = new CultureInfo("it");

        public static CultureInfo GetCultureInfoFromLanguageInfo(LanguageInfo languageInfo)
        {
            if (languageInfo == null)
            {
                return defaultLanguage;
            }
            return new CultureInfo(languageInfo.Iso2Code);
        }
    }
}