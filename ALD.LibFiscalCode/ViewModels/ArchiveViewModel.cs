using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
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