using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Importer;
using ALD.LibFiscalCode.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlaceImportViewModel : AbstractNotifyPropertyChanged
    {
        public PlaceImportViewModel()
        {
            InputFilename = null;
            Mode = ImportMode.Update;
            Configuration = new ImporterConfiguration();
            PropertyChanged += OnPropertyChanged(nameof(PlaceCsvMapper.SelectedPropertyName));
            PropertyChanged += OnPropertyChanged(nameof(PlaceCsvMapper.Position));
            PropertyChanged += OnPropertyChanged(nameof(PlaceCsvMapper.CsvName));
            PropertyChanged += OnPropertyChanged(nameof(PlaceCsvMapper.AvailableProperties));
            FieldMapping = PropertyListFactory.Generate(Fields);
            OnPropertyChanged(nameof(FieldMapping));
        }

        private string inputFilename;

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

        private bool usesCustomMapping;
        public ImportMode Mode { get; set; }
        public bool CanStartImport => !(string.IsNullOrWhiteSpace(InputFilename));
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

        private ObservableCollection<PlaceCsvMapper> fieldMapping;

        public async void Import()
        {
            await PlacesImporter.Import(InputFilename, Configuration, Mode);
        }

        private Type placeType = new Place().GetType();

        public List<PropertyInfo> Fields => placeType.GetProperties().ToList();
    }
}