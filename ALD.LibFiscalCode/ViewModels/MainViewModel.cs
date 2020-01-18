﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Localization;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Settings;
using ALD.LibFiscalCode.Validators;
using Microsoft.EntityFrameworkCore.Sqlite.Query.Internal;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel : AbstractNotifyPropertyChanged, IEditableObject
    {
        public LocalizationProvider LocalizationProvider
        {
            get => localizationProvider;
            private set
            {
                localizationProvider = value;
                OnPropertyChanged(nameof(LocalizationProvider));
            }
        }

        private LocalizationProvider localizationProvider;

        private Person currentPerson;

        private FiscalCode fiscalCode;

        private FiscalCodeBuilder fiscalCodeBuilder;

        private OmocodeBuilder omocodeBuilder;

        private List<FiscalCodeDecorator> omocodes;

        private ObservableCollection<Place> places;

        private Place selectedPlace;

        public MainViewModel(AppSettings settings) : this()
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            LocalizationProvider = new LocalizationProvider(new DatabaseLocalizationRetriever(settings.AppLanguage), "MainWindow");
            CurrentPerson.DateOfBirth = settings.DefaultDate;
            CanUserInteract = true;
        }

        public MainViewModel()
        {
            CanUserInteract = false;
            
            CurrentPerson = new Person();
            PopulatePlaceList();

            LocalizationProvider = new LocalizationProvider(new DatabaseLocalizationRetriever(CultureInfo.CurrentUICulture), "MainWindow");

            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Name));
            PropertyChanged += OnPropertyChanged(nameof(CurrentPerson.Surname));
            PropertyChanged += OnPropertyChanged(nameof(Omocodes));

            CancelEdit();
            
        }

        public bool CanUserInteract { get; }

        public Person CurrentPerson
        {
            get => currentPerson;
            set
            {
                currentPerson = value;
                OnPropertyChanged(nameof(CurrentPerson));
            }
        }

        public PersonValidator Validator { get; set; }

        public FiscalCode FiscalCode
        {
            get => fiscalCode;
            private set
            {
                fiscalCode = value;
                OnPropertyChanged(nameof(FiscalCode));
            }
        }

        public List<FiscalCodeDecorator> Omocodes => omocodes;

        public ObservableCollection<Place> Places => places;

        public Place SelectedPlace
        {
            get => selectedPlace;
            set
            {
                selectedPlace = value;
                OnPropertyChanged(nameof(SelectedPlace));
            }
        }

        public bool HasPendingChanges { get; private set; }

        public bool PlacesLoaded => Places?.Count > 0;

        public void BeginEdit()
        {
            if (CanUserInteract)
                HasPendingChanges = true;
        }

        public void CancelEdit()
        {
            HasPendingChanges = false;
        }

        public void EndEdit()
        {
            if (CanUserInteract)
                HasPendingChanges = false;
        }

        private void PopulatePlaceList()
        {
            using var ctx = new AppDataContext();
            places = new ObservableCollection<Place>(ctx.Places);
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

        public void SetGender(string gender)
        {
            CurrentPerson.Gender = gender switch
            {
                "M" => Gender.Male,
                "F" => Gender.Female,
                _ => Gender.Unspecified
            };
            OnPropertyChanged(nameof(CurrentPerson));
        }

        public void ResetPerson(DateTime? defaultDate = null)
        {
            CurrentPerson = new Person(defaultDate.GetValueOrDefault());
            CancelEdit();
        }

        public IValidator CalculateFiscalCode()
        {
            Validator = new PersonValidator(CurrentPerson, LocalizationProvider);
            if (Validator.IsValid)
            {
                //Executed in a task because Unidecoder is quite slow and we don't need to await its completion.
                Task.Run(
                    () =>
                    {
                        fiscalCodeBuilder = new FiscalCodeBuilder(CurrentPerson, LocalizationProvider);
                        FiscalCode = fiscalCodeBuilder.ComputedFiscalCode;
                        omocodeBuilder = new OmocodeBuilder(fiscalCodeBuilder);
                        omocodes = omocodeBuilder.Omocodes;
                        using var context = new AppDataContext();
                        context.SavePerson(CurrentPerson);
                        SaveFiscalCode(context, Omocodes, CurrentPerson);
                        context.SaveChangesAsync();
                    }
                );
            }

            return Validator;
        }

        private void SaveFiscalCode(AppDataContext context, IEnumerable<FiscalCodeDecorator> codes, Person person)
        {
            var newFc = new FiscalCodeEntity
            {
                FiscalCode = codes.FirstOrDefault(fc => fc.IsMain)?.FiscalCode.FullFiscalCode,
                Person = person
            };
            context.FiscalCodes.Add(newFc);
            context.SaveChangesAsync();
        }

        public void ChangePlace(Place newPlace)
        {
            SelectedPlace = newPlace;
            CurrentPerson.PlaceOfBirth = newPlace;
        }
    }
}