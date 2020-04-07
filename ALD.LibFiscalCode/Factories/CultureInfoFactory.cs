using System;
using System.Globalization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Factories
{
    public static class CultureInfoFactory
    {
        public static CultureInfo GetCultureInfoFromLanguageInfo(LanguageInfo languageInfo)
        {
            if (languageInfo == null)
            {
                return new CultureInfo("it-IT");
            }
            return new CultureInfo(languageInfo.Iso2Code);
        }
    }
}