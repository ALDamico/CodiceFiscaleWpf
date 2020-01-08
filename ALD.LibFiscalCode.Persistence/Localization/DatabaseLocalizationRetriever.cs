using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
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
            var output = context.LocalizedStrings
                .Include(s => s.Language)
                .Include(s => s.Window)
                .Where(s => s.Window.Name.Equals(windowName));
            return output;
        }
    }
}