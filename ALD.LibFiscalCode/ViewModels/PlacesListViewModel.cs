using System;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlacesListViewModel : AbstractNotifyPropertyChanged
    {
        public CollectionViewSource ViewSource { get; set; }
     

        public PlacesListViewModel(ICollection<Place> places)
        {
            Places = new ObservableCollection<Place>(places);
            ViewSource = new CollectionViewSource {Source = Places};

        }
        public string FilterText { get; set; }

        public ObservableCollection<Place> Places { get; }


        public void Filter(string filterText)
        {
            FilterText = filterText;
            ViewSource.Filter += ViewSourceOnFilter;
            OnPropertyChanged(nameof(ViewSource));
        }

        private void ViewSourceOnFilter(object sender, FilterEventArgs e)
        {
            var item = e.Item as Place;
            if (item != null)
            {
                e.Accepted = item.Name.Contains(FilterText, StringComparison.InvariantCultureIgnoreCase);
            }

            
        }

       

        public void ResetFilter()
        {
            FilterText = "";
            ViewSource.Filter -= ViewSourceOnFilter;
        }

       
    }
}