using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Persistence.Factories;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.Localization
{
    public class DatabaseLocalizationRetriever : ILocalizationRetrievalStrategy
    {
        public DatabaseLocalizationRetriever(CultureInfo culture)
        {
            Language = culture;
        }

        public CultureInfo Language { get; }

        IEnumerable<LocalizedString> ILocalizationRetrievalStrategy.GetLocalizedStrings(string windowName)
        {
            var context = new AppDataContext();
            var targetCulture =
                CultureInfoFactory.GetCultureInfoFromLanguageInfo(
                    context.Languages.FirstOrDefault(l => l.Iso2Code.Equals(Language.Name)));

            var output = context.LocalizedStrings
                .Include(s => s.Language)
                .Include(s => s.Window)
                .Where(s => s.Window.Name.Equals(windowName))
                .Where(s => Language.Name.Equals(s.Language.Iso2Code));
            return output;
        }

        IEnumerable<LocalizedString> ILocalizationRetrievalStrategy.GetLocalizedStrings(string windowName, LanguageInfo targetLanguage)
        {
            var context = new AppDataContext();
            var output = context.LocalizedStrings
                .Include(s => s.Language)
                .Include(s => s.Window)
                .Where(s => s.Window.Name.Equals(windowName)).Where(s => s.Language.Equals(targetLanguage));
            return output;
        }
    }
}