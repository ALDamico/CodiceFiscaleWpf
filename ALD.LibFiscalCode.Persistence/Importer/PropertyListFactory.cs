using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public static class PropertyListFactory
    {
        public static ObservableCollection<PlaceCsvMapper> Generate(IEnumerable<PropertyInfo> properties)
        {
            var output = new ObservableCollection<PlaceCsvMapper>();
            var i = 0;
            foreach (var element in properties)
            {
                output.Add(new PlaceCsvMapper(properties, element.Name));
                output[i].CsvName = element.Name;
                output[i].Position = i;
                i++;
            }

            return output;
        }
    }
}