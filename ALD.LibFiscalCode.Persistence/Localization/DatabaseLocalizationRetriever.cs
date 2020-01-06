using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.Persistence.Localization
{
    public class DatabaseLocalizationRetriever:ILocalizationRetrievalStrategy
    {
        public DatabaseLocalizationRetriever(CultureInfo culture)
        {
            Language = culture;
        }

        public LocalizedString GetLocalizedString(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<LocalizedString> GetLocalizedStringAsync(string name)
        {
            using var context = new AppDataContext();
            return await context.LocalizedStrings.Include(s => s.Language).Where(l => l.Name == name).FirstOrDefaultAsync();
        }

        public CultureInfo Language { get; }
        public List<LocalizedString> GetLocalizedStringsAsList()
        {
            using var context = new AppDataContext();
            return context.LocalizedStrings.Include(s => s.Language).ToList();
        }


        
    }
}