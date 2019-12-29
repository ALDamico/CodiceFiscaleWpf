using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public static class PropertyListFactory
    {
        public static ObservableCollection<PlaceCsvMapper> Generate(IEnumerable<PropertyInfo> properties)
        {
            ObservableCollection<PlaceCsvMapper> output = new ObservableCollection<PlaceCsvMapper>();
            int i = 0;
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
