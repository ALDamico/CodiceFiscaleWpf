using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence.Localization
{
    public class LocalizationProvider
    {
        public LocalizationProvider(ILocalizationRetrievalStrategy retrievalStrt)
        {
            retrievalStrategy = retrievalStrt;
            handler = retrievalStrategy.GetLocalizedString;

        }
        private ILocalizationRetrievalStrategy retrievalStrategy;

        public delegate LocalizedString GetLocalizedStringDel(string name);

        private GetLocalizedStringDel handler;

        public LocalizedString GetLocalizedString(string name)
        {
            return handler(name);
        }

        public List<LocalizedString> GetLocalizedStrings()
        {
            return retrievalStrategy.GetLocalizedStringsAsList();
        }

        public ILocalizationRetrievalStrategy RetrievalStrategy => retrievalStrategy;
    }
}