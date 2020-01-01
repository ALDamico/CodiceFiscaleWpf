using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlacesListViewModel : AbstractNotifyPropertyChanged
    {
        private readonly ObservableCollection<Place> places;
        private string filterText;

        public PlacesListViewModel(ICollection<Place> places)
        {
            this.places = new ObservableCollection<Place>(places);
            ViewSource = new CollectionViewSource { Source = this.places };
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
                e.Accepted = item.Name.Contains(filterText, StringComparison.InvariantCultureIgnoreCase);
            }
        }

        public void ResetFilter()
        {
            filterText = "";
            ViewSource.Filter -= ViewSourceOnFilter;
        }
    }
}