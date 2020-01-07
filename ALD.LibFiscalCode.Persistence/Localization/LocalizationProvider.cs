using System;
using System.Collections.Generic;
using System.Windows;
using ALD.LibFiscalCode.Persistence.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;

namespace ALD.LibFiscalCode.Persistence.Localization
{
    public class LocalizationProvider
    {
        public LocalizationProvider(ILocalizationRetrievalStrategy retrievalStrat)
        {
            retrievalStrategy = retrievalStrat;
        }

        public ResourceDictionary GetResourceDictionary()
        {
            var rd = new ResourceDictionary();
            var localizedStrings = retrievalStrategy.GetLocalizedStringsAsList();

            foreach (var localizedString in localizedStrings)
            {
                rd.Add(localizedString.Name, localizedString.Value);
            }

            return rd;
        }

        private ILocalizationRetrievalStrategy retrievalStrategy;
    }
}