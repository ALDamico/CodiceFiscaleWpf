using ALD.LibFiscalCode.Persistence.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlacesListViewModel : AbstractNotifyPropertyChanged
    {
        public PlacesListViewModel()
        {
            SelectedPlace = null;
        }

        public PlacesListViewModel(ICollection<Place> places)
        {
            SelectedPlace = null;
            Places = new ObservableCollection<Place>(places);
            FilteredPlaces = new ObservableCollection<Place>(places);
        }

        public ObservableCollection<Place> Places { get;  }

        public Place SelectedPlace
        {
            get => selectedPlace; 
            set
            {
                selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }

        public void FilterPlaces(string filter)
        {
            FilteredPlaces = new ObservableCollection<Place>(Places.Where(p => p.Name.Contains(filter, System.StringComparison.InvariantCultureIgnoreCase)));
        }

        public void ResetFilter()
        {
            FilteredPlaces = new ObservableCollection<Place>(Places);
        }

        public ObservableCollection<Place> FilteredPlaces { 
        get
            {
                return filteredPlaces;
            }
            set
            {
                filteredPlaces = value;
                OnPropertyChanged(nameof(FilteredPlaces));
            }
        }

        private ObservableCollection<Place> filteredPlaces;
        private Place selectedPlace;
    }
}