using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Localization;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlacesListViewModel : AbstractNotifyPropertyChanged
    {
        private readonly ObservableCollection<Place> places;
        private string filterText;

        private Place selectedPlace;

        public LocalizationProvider LocalizationProvider { get; }

        public PlacesListViewModel(ICollection<Place> places)
        {
            this.places = new ObservableCollection<Place>(places);
            ViewSource = new CollectionViewSource { Source = this.places };
            LocalizationProvider = new LocalizationProvider(new DatabaseLocalizationRetriever(CultureInfo.CurrentUICulture), "PlacesList");
        }

        public Place SelectedPlace
        {
            get => selectedPlace;
            set
            {
                selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }

        public CollectionViewSource ViewSource { get; set; }

        public void Filter(string filterText)
        {
            this.filterText = filterText;

            ViewSource.Filter += ViewSourceOnFilter;
        }

        private void ViewSourceOnFilter(object sender, FilterEventArgs e)
        {
            if (e.Item is Place item)
            {
                e.Accepted = item.Name.Contains(filterText, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public void ResetFilter()
        {
            filterText = "";
            ViewSource.Filter -= ViewSourceOnFilter;
        }
    }
}