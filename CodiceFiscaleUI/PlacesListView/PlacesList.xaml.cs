using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CodiceFiscaleUI.PlacesListView
{
    /// <summary>
    /// Interaction logic for PlacesList.xaml
    /// </summary>
    public partial class PlacesList : Window
    {
        public PlacesList(ICollection<Place> places)
        {
            viewModel = new PlacesListViewModel(places);
            DataContext = viewModel;
            loading = true;
            InitializeComponent();
        }

        private bool loading;

        private PlacesListViewModel viewModel;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            var selectedPlace = e.AddedItems[0] as Place;
            viewModel.SelectedPlace = selectedPlace;
        }

        
       

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loading)
            {
                loading = false;
                return;
            }
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                viewModel.ResetFilter();
            }
            else {
                viewModel.FilterPlaces(txtFilter.Text);
                
            }
            
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Foreground = Brushes.Black;
        }
    }
}
