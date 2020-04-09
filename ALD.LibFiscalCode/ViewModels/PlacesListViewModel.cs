using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;

using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using System.Globalization;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlacesListViewModel : AbstractNotifyPropertyChanged
    {
        private readonly ObservableCollection<Place> places;
        private string filterText;

        private Place selectedPlace;

        public PlacesListViewModel(ICollection<Place> places)
        {
            this.places = new ObservableCollection<Place>(places);
            ViewSource = new CollectionViewSource();
            ViewSource.Source = places;
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
                var caseInsensitiveName = item.Name.ToLower(CultureInfo.InvariantCulture);
                var caseInsensitiveFilter = filterText.ToLower(CultureInfo.InvariantCulture);
                e.Accepted = caseInsensitiveName.Contains(caseInsensitiveFilter, StringComparison.InvariantCulture);
            }
        }

        public void ResetFilter()
        {
            filterText = "";
            ViewSource.Filter -= ViewSourceOnFilter;
        }
    }
}