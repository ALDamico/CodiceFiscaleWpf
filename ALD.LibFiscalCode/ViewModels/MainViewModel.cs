﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Builders;
using ALD.LibFiscalCode.Interfaces;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using ALD.LibFiscalCode.Settings;
using ALD.LibFiscalCode.Validators;
using ALD.LibFiscalCode.Validators.Interfaces;
using ALD.LibFiscalCode.Validators.Person;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel : AbstractNotifyPropertyChanged, IEditableObject
    {
        private Person currentPerson;

        private FiscalCode fiscalCode;

        private FiscalCodeBuilder fiscalCodeBuilder;

        public bool FiscalCodeCalculated => currentPerson.FiscalCode != null;

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

            CurrentPerson.DateOfBirth = settings.DefaultDate;
            CanUserInteract = true;
        }

        public MainViewModel()
        {
            CanUserInteract = false;

            CurrentPerson = new Person();
            PopulatePlaceList();

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
            Validator = new PersonValidator(CurrentPerson);
            if (Validator.IsValid)
            {
                fiscalCodeBuilder = new FiscalCodeBuilder(CurrentPerson);
                FiscalCode = fiscalCodeBuilder.ComputedFiscalCode;
                CurrentPerson.FiscalCode = new FiscalCodeEntity() { FiscalCode = FiscalCode.FullFiscalCode, Person = CurrentPerson };
                omocodeBuilder = new OmocodeBuilder(fiscalCodeBuilder);
                omocodes = omocodeBuilder.Omocodes;
                using var context = new AppDataContext();
                context.FiscalCodes.Add(CurrentPerson.FiscalCode);
                context.People.Add(CurrentPerson);
                context.Entry(CurrentPerson.PlaceOfBirth).State = EntityState.Unchanged;

                OnPropertyChanged(nameof(FiscalCode));
                OnPropertyChanged(nameof(CurrentPerson));
                OnPropertyChanged(nameof(CurrentPerson.FiscalCode));
                OnPropertyChanged(nameof(FiscalCodeCalculated));
                try
                {
                    context.SaveChanges();
                }
                catch (SqliteException)
                {
                }
                catch (DbUpdateException)
                {
                }
            }

            EndEdit();
            HasPendingChanges = false;

            return Validator;
        }

        private void SaveFiscalCode(AppDataContext context, IEnumerable<FiscalCodeDecorator> codes, Person person)
        {
            context?.SaveFiscalCode(codes, person);
        }

        public void ChangePlace(Place newPlace)
        {
            SelectedPlace = newPlace;
            CurrentPerson.PlaceOfBirth = newPlace;
        }

        public void Export(string targetPath, IExportStrategy exportStrategy)
        {
            if (exportStrategy == null)
            {
                throw new ArgumentNullException(nameof(exportStrategy));
            }
            exportStrategy.Export(currentPerson, targetPath);
        }
    }
}