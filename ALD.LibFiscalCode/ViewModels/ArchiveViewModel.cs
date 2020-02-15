using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ALD.LibFiscalCode.ViewModels
{
    public class ArchiveViewModel : AbstractNotifyPropertyChanged
    {
        public ObservableCollection<Person> People
        {
            get => people;
            set
            {
                people = value;
                OnPropertyChanged(nameof(people));
            }
        }

        private ObservableCollection<Person> people;

        public ArchiveViewModel()
        {
            PopulatePlacesList();
        }

        public void DeletePeople(IEnumerable<Person> people)
        {
            using var dataContext = new AppDataContext();

            foreach (var person in people)
            {
                //var fcToRemove = dataContext.FiscalCodes.Where(p => p.Person == person).FirstOrDefault();
                //dataContext.Entry(fcToRemove).State = EntityState.Deleted;
                //dataContext.FiscalCodes.Remove(fcToRemove);
                dataContext.People.Remove(person);
                dataContext.Entry(person).State = EntityState.Deleted;

                People.Remove(person);
            }
            try
            {
                dataContext.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is FiscalCodeEntity)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            // proposedValues[property] = <value to be saved>;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }
            }
        }

        private async void PopulatePlacesList()
        {
            await Task.Run(() =>
            {
                using var dataContext = new AppDataContext();
                People = new ObservableCollection<Person>(dataContext.People.Include(p => p.PlaceOfBirth).Include(p => p.FiscalCode));
            });
        }
    }
}