using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Models;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Validators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

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
                Task<List<Place>> task = dbContext.GetAllPlaces();
                Places = new ObservableCollection<Place>(await task.ConfigureAwait(false));

                OnPropertyChanged(nameof(Places));
            }
        }

        public Person CurrentPerson
        {
            get
            {
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

        public string CalculateFiscalCode()
        {
            var validator = new PersonValidator(CurrentPerson);
            string errorMessages = null;
            if (validator.IsValid)
            {
                fiscalCodeBuilder = new FiscalCodeBuilder(CurrentPerson);
                FiscalCode = fiscalCodeBuilder.ComputedFiscalCode;
            }
            else
            {
                errorMessages = string.Concat(validator.ValidationMessages);
                errorMessages = "Convalida delle informazioni fornite non riuscita:\n" + errorMessages;
            }
            return errorMessages;
        }

        public FiscalCode FiscalCode
        {
            get => fiscalCode;
            private set
            {
                fiscalCode = value;
                OnPropertyChanged(nameof(FiscalCode));
            }
        }

        private FiscalCode fiscalCode;

        public ObservableCollection<Place> Places { get; set; }

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

        private FiscalCodeBuilder fiscalCodeBuilder;

        public bool HasPendingChanges
        {
            get; private set;
        }
    }
}