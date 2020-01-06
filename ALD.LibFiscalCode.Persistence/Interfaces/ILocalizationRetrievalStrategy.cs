using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence.Interfaces
{
    public interface ILocalizationRetrievalStrategy
    {
        LocalizedString GetLocalizedString(string name);
        Task<LocalizedString> GetLocalizedStringAsync(string name);
        CultureInfo Language { get; }
        List<LocalizedString> GetLocalizedStringsAsList();
    }
}