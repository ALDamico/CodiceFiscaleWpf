using ALD.LibFiscalCode.Persistence.Models;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
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
            ClassMap = new PlaceMap();
        }

        public ImporterConfiguration()
        {
            Delimiter = ";";
            Escape = '"';
            Encoding = Encoding.UTF8;
            ClassMap = new PlaceMap();
        }
        public string Delimiter { get; set; }
        public char Escape { get; set; }
        public Encoding Encoding { get; set; }
        public ClassMap<Place> ClassMap { get; set; }

        private Type typeInfo = new Place().GetType();
        public Dictionary<string, PropertyInfo> Mapping { get; private set; }

        internal void ConfigureClassMap()
        {
            ClassMap.Map(m => m.Name).Name(Mapping[nameof(Place.Name)].Name);
            ClassMap.Map(p => p.Province).Name(Mapping[nameof(Place.Province)].Name);
            ClassMap.Map(p => p.ProvinceAbbreviation).Name(Mapping[nameof(Place.ProvinceAbbreviation)].Name);
            ClassMap.Map(p => p.Region).Name(Mapping[nameof(Place.Region)].Name);
            ClassMap.Map(p => p.Code).Name(Mapping[nameof(Place.Code)].Name);
        }

        public Configuration ToConfiguration()
        {
            var output = new Configuration();

            output.Delimiter = Delimiter;
            output.Escape = Escape;
            output.Encoding = Encoding;
            output.HasHeaderRecord = true;
            //output.PrepareHeaderForMatch = ((s, i) => s.Replace("place_", "").ToSentenceCase());
            

            return output;
        }

    }
}
