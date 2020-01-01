using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace CodiceFiscaleUI.PlacesListView
{
    /// <summary>
    /// Interaction logic for PlacesList.xaml
    /// </summary>
    public partial class PlacesList : Window
    {
        public CollectionViewSource ViewSource { get; set; }

        
        public PlacesList(ICollection<Place> places)
        {
            viewModel = new PlacesListViewModel(places);
            DataContext = viewModel;
            loading = true;
           //ViewSource = new CollectionViewSource();
            InitializeComponent();
        }

        private bool loading;

        private readonly PlacesListViewModel viewModel;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null)
            {
                return;
            }

        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loading)
            {
                loading = false;
                return;
            }

            var filterText = (sender as TextBox).Text;
            if (string.IsNullOrEmpty(filterText))
            {
                viewModel.ResetFilter();
            }
            else
            {
                viewModel.Filter(filterText);
            }
        }

        private void ViewSourceOnFilter(object sender, FilterEventArgs e)
        {
            var temp = e.Item as Place;
            var filterText = (sender as TextBox).Text;
            e.Accepted = temp.Name.Contains(filterText, StringComparison.InvariantCultureIgnoreCase);
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Foreground = Brushes.Black;
        }
    }
}