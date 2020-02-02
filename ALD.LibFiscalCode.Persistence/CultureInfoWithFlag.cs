using System;
using System.Globalization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence
{
    public class CultureInfoWithFlag
    {
        public static CultureInfoWithFlag FromLanguageInfo(LanguageInfo source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var instance = new CultureInfoWithFlag
            {
                Culture = new CultureInfo(source.Iso2Code),
                ImagePath = AppDomain.CurrentDomain.BaseDirectory + source.ImagePath
            };
            return instance;
        }

        public CultureInfo Culture { get; set; }
        public string ImagePath { get; set; }
    }
}