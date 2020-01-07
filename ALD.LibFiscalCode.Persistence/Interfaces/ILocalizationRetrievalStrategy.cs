using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence.Interfaces
{
    public interface ILocalizationRetrievalStrategy
    {
        CultureInfo Language { get; }

        IEnumerable<LocalizedString> GetLocalizedStrings(string windowName);
    }
}