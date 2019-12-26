using ALD.LibFiscalCode.Models;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using System.Collections.Generic;
using System.ComponentModel;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel : AbstractNotifyPropertyChanged
    {
        public MainViewModel()
        {
            CurrentPerson = new Person();

            PopulatePlacesList();
            HasPendingChanges = false;
            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Name));
            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Surname));
        }

        protected internal override PropertyChangedEventHandler OnPropertyChanged(string propertyName)
        {
            HasPendingChanges = true;
            return base.OnPropertyChanged(propertyName);
        }

        private async void PopulatePlacesList()
        {
            using (var dbContext = new PlacesContext())
            {
                availablePlaces = await dbContext.GetAllPlaces();
            }
            OnPropertyChanged(nameof(Places));
        }

        public Person CurrentPerson
        {
            get
            {
                //HasPendingChanges = true;
                return _currentPerson;
            }
            set
            {
                _currentPerson = value;
                OnPropertyChanged(nameof(CurrentPerson));
                HasPendingChanges = true;
            }
        }

        private Person _currentPerson;

        public void SetGender(string gender)
        {
            if (gender == "M")
            {
                CurrentPerson.Gender = Enums.Gender.Male;
            }
            else if (gender == "F")
            {
                CurrentPerson.Gender = Enums.Gender.Female;
            }
            else
            {
                CurrentPerson.Gender = Enums.Gender.Unspecified;
            }
            HasPendingChanges = true;
        }

        public void ResetPerson()
        {
            CurrentPerson = new Person();
            HasPendingChanges = false;
        }

        public List<Place> Places => availablePlaces;

        public List<Place> availablePlaces;

        public Place SelectedPlace
        {
            get
            {
                return selectedPlace;
            }
            set
            {
                selectedPlace = value;
                HasPendingChanges = true;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }

        public void ChangePlace(Place newPlace)
        {
            SelectedPlace = newPlace;
            CurrentPerson.PlaceOfBirth = newPlace;
        }

        private Place selectedPlace;

        public bool HasPendingChanges
        {
            get; private set;
        }
    }
}