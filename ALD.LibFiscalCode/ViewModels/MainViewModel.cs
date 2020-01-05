﻿using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Validators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel : AbstractNotifyPropertyChanged, IEditableObject
    {
        private readonly bool canUserInteract;
        public bool CanUserInteract => canUserInteract;

        public MainViewModel()
        {
            canUserInteract = false;
            CurrentPerson = new Person();
            PopulatePlaceList();

            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Name));
            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Surname));
            PropertyChanged += OnPropertyChanged(nameof(Omocodes));

            CancelEdit();
            canUserInteract = true;
        }

        private async Task PopulatePlaceList()
        {
            var t = new PlacesContext().Places;
            Task.Run(() => Places = new ObservableCollection<Place>(t));
        }

        public void SetMainFiscalCode(FiscalCode code)
        {
            FiscalCode = code;
        }

        protected sealed override PropertyChangedEventHandler OnPropertyChanged(string propertyName)
        {
            if (propertyName != nameof(PlacesLoaded))
            {
                BeginEdit();
            }

            return base.OnPropertyChanged(propertyName);
        }

        public Person CurrentPerson
        {
            get => currentPerson;
            set
            {
                currentPerson = value;
                OnPropertyChanged(nameof(CurrentPerson));
            }
        }

        private Person currentPerson;

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
        }

        public void ResetPerson()
        {
            CurrentPerson = new Person();
            CancelEdit();
        }

        public IValidator CalculateFiscalCode()
        {
            Validator = new PersonValidator(CurrentPerson);
            string errorMessages = null;
            if (Validator.IsValid)
            {
                //Executed in a task because Unidecoder is quite slow and we don't need to await its completion.
                Task.Run(
                    () =>
                    {
                        fiscalCodeBuilder = new FiscalCodeBuilder(CurrentPerson);
                        FiscalCode = fiscalCodeBuilder.ComputedFiscalCode;
                        omocodeBuilder = new OmocodeBuilder(fiscalCodeBuilder);
                        Omocodes = omocodeBuilder.Omocodes;
                        using var context = new PlacesContext();
                        context.SavePerson(CurrentPerson);
                        SaveFiscalCode(context, Omocodes, CurrentPerson);
                        context.SaveChangesAsync();
                    }
                );
            }
            return Validator;
        }

        public PersonValidator Validator { get; set; }

        private void SaveFiscalCode(PlacesContext context, IEnumerable<FiscalCodeDecorator> codes, Person person)
        {
            var newFc = new FiscalCodeEntity
            {
                FiscalCode = codes.FirstOrDefault(fc => fc.IsMain)?.FiscalCode.FullFiscalCode,
                Person = person
            };
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

        public ObservableCollection<Place> Places
        {
            get => places;
            set
            {
                places = value;
                OnPropertyChanged(nameof(Places));
                OnPropertyChanged(nameof(PlacesLoaded));
            }
        }

        private ObservableCollection<Place> places;

        public Place SelectedPlace
        {
            get
            {
                return selectedPlace;
            }
            set
            {
                selectedPlace = value;
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

        public void BeginEdit()
        {
            if (canUserInteract)
                HasPendingChanges = true;
        }

        public void CancelEdit()
        {
            if (!canUserInteract)
            {
                HasPendingChanges = false;
                CurrentPerson = new Person();
            }
        }

        public void EndEdit()
        {
            if (canUserInteract)
                HasPendingChanges = false;
        }

        public bool PlacesLoaded => Places?.Count > 0;
    }
}