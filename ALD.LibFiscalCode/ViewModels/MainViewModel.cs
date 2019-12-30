using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Validators;
using ALD.LibFiscalCode.Persistence.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Linq;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel : AbstractNotifyPropertyChanged
    {
        public MainViewModel()
        {
            CurrentPerson = new Person();

            Task.Run(PopulatePlacesList);
            
            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Name));
            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Surname));
            PropertyChanged += OnPropertyChanged(nameof(Omocodes));

            HasPendingChanges = false;
        }

        public void SetMainFiscalCode(FiscalCode code)
        {
            FiscalCode = code;
        }

        protected override PropertyChangedEventHandler OnPropertyChanged(string propertyName)
        {
            HasPendingChanges = true;
            return base.OnPropertyChanged(propertyName);
        }

        private async void PopulatePlacesList()
        {
            await using (var dbContext = new PlacesContext())
            {
                List<Place> task = await dbContext.GetAllPlaces();
                Places = new ObservableCollection<Place>(task);

                OnPropertyChanged(nameof(Places));
            }
        }

        public ALD.LibFiscalCode.Persistence.Models.Person CurrentPerson
        {
            get
            {
                return _currentPerson;
            }
            set
            {
                _currentPerson = value;
                HasPendingChanges = true;
                OnPropertyChanged(nameof(CurrentPerson));
                
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

        public async Task<IValidator> CalculateFiscalCode()
        {
            Validator = new PersonValidator(CurrentPerson);
            string errorMessages = null;
            if (Validator.IsValid)
            {
                fiscalCodeBuilder = new FiscalCodeBuilder(CurrentPerson);
                FiscalCode = fiscalCodeBuilder.ComputedFiscalCode;
                omocodeBuilder = new OmocodeBuilder(fiscalCodeBuilder);
                Omocodes = omocodeBuilder.Omocodes;
                using (var context = new PlacesContext())
                {
                    context.SavePerson(CurrentPerson);
                    SaveFiscalCode(context, new List<FiscalCodeDecorator>(Omocodes), CurrentPerson);
                    Task.Run(() => context.SaveChangesAsync());
                }
            }
            return Validator;
        }

      

        public PersonValidator Validator { get; set; }

        private void SaveFiscalCode(PlacesContext context, IEnumerable<FiscalCodeDecorator> codes, Person person)
        {
            var newFc = new FiscalCodeEntity();
            newFc.FiscalCode = codes.Where(fc => fc.IsMain).FirstOrDefault().FiscalCode.FullFiscalCode;
            newFc.Person = person;
            context.FiscalCodes.Add(newFc);
            context.SaveChangesAsync();
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

        public List<FiscalCodeDecorator> Omocodes
        {
            get => omocodes;
            set
            {
                omocodes = value;
                OnPropertyChanged(nameof(Omocodes));
            }
        }
        private List<FiscalCodeDecorator> omocodes;

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

        private OmocodeBuilder omocodeBuilder;
    }
}