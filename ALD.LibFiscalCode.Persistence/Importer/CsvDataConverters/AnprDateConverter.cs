using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer.CsvDataConverters
{
    public class AnprDateConverter: DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == "1900-01-01" || text == "9999-12-31")
            {
                return default(DateTime);
            }
            try
            {
                return DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            }
            catch(TypeConverterException)
            {
                return default(DateTime);
            }
            catch
            {
                throw;
            }
        }
    }
}
