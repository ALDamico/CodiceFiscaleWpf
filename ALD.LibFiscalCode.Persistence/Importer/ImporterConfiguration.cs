using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public class ImporterConfiguration 
    {
        public ImporterConfiguration(string delimiter, char escape, Encoding encoding) 
        {
            Delimiter = delimiter;
            Escape = escape;
            Encoding = encoding;
        }

        public ImporterConfiguration()
        {
            Delimiter = ";";
            Escape = '"';
            Encoding = Encoding.UTF8;
        }
        public string Delimiter { get; set; }
        public char Escape { get; set; }
        public Encoding Encoding { get; set; }

        public Configuration ToConfiguration()
        {
            var output = new Configuration();

            output.Delimiter = Delimiter;
            output.Escape = Escape;
            output.Encoding = Encoding;

            return output;
        }
    }
}
