using System;
using System.Globalization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Persistence
{
    public class CultureInfoWithFlag
    {
        public CultureInfoWithFlag(CultureInfo culture, string imagePath = "")
        {
            Culture = culture;
            ImagePath = imagePath;
        }
        public static CultureInfoWithFlag FromLanguageInfo(LanguageInfo source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            var instance = new CultureInfoWithFlag(new CultureInfo(source.Iso2Code), AppDomain.CurrentDomain.BaseDirectory + source.ImagePath);

            return instance;
        }

        public CultureInfo Culture { get; set; }
        public string ImagePath { get; set; }
    }
}