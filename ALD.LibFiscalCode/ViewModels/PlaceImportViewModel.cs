using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Importer;
using ALD.LibFiscalCode.Persistence.Localization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlaceImportViewModel : AbstractNotifyPropertyChanged
    {
        private ObservableCollection<PlaceCsvMapper> fieldMapping;

        private string inputFilename;

        private readonly Type placeType = new Place().GetType();

        private bool usesCustomMapping;

        public PlaceImportViewModel()
        {
            InputFilename = null;
            Mode = ImportMode.Update;
            Configuration = new ImporterConfiguration();
           
            PropertyChanged += base.OnPropertyChanged(nameof(PlaceCsvMapper.SelectedPropertyName));
            PropertyChanged += base.OnPropertyChanged(nameof(PlaceCsvMapper.Position));
            PropertyChanged += base.OnPropertyChanged(nameof(PlaceCsvMapper.CsvName));
            PropertyChanged += base.OnPropertyChanged(nameof(PlaceCsvMapper.AvailableProperties));
            FieldMapping = PropertyListFactory.Generate(Fields);
            base.OnPropertyChanged(nameof(FieldMapping));
        }

        public string InputFilename
        {
            get => inputFilename;
            set
            {
                inputFilename = value;
                OnPropertyChanged(nameof(InputFilename));
                OnPropertyChanged(nameof(CanStartImport));
            }
        }

        public bool UsesCustomMapping
        {
            get => usesCustomMapping;
            set
            {
                usesCustomMapping = value;
                OnPropertyChanged(nameof(UsesCustomMapping));
                OnPropertyChanged(nameof(FieldMapping));
            }
        }

        public ImportMode Mode { get; set; }
        public bool CanStartImport => !string.IsNullOrWhiteSpace(InputFilename);
        public ImporterConfiguration Configuration { get; set; }

        public ObservableCollection<PlaceCsvMapper> FieldMapping
        {
            get => fieldMapping;
            set
            {
                fieldMapping = value;
                OnPropertyChanged(nameof(FieldMapping));
            }
        }

        public List<PropertyInfo> Fields => placeType.GetProperties().ToList();

        public async Task<Exception> Import()
        {
            var t = PlacesImporter.Import(InputFilename, Configuration, Mode);

            return t.Status == TaskStatus.Faulted ? new FileNotFoundException(t.Exception.Message) : null;
        }
    }
}