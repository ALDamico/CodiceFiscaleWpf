using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using ALD.LibFiscalCode.Persistence.Events;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public class PlaceCsvMapper:AbstractNotifyPropertyChanged
    {

        internal PlaceCsvMapper(IEnumerable<PropertyInfo> properties, string propertyName)
        {
            SelectedPropertyName = propertyName;
            AvailableProperties = new ObservableCollection<string>();
            foreach (var prop in properties)
            {
                AvailableProperties.Add(prop.Name);
            }
            OnPropertyChanged(nameof(AvailableProperties));
           // AvailableProperties = properties as ObservableCollection<string>;
        }
        public string SelectedPropertyName
        {
            get => propertyName;
            set
            {
                propertyName = value;
                OnPropertyChanged(nameof(SelectedPropertyName));
            }
        }

        public ObservableCollection<string> AvailableProperties
        {
            get => availableProperties;
            set
            {
                availableProperties = value;
                OnPropertyChanged(nameof(AvailableProperties));
            }
        }
        private ObservableCollection<string> availableProperties;
        public string CsvName
        {
            get => csvName;
            set
            {
                csvName = value;
                OnPropertyChanged(nameof(CsvName));
            }
        }
        public int Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged(nameof(Position));
            }
        }
        private string propertyName;
        private string csvName;
        private int position;
    }
}
