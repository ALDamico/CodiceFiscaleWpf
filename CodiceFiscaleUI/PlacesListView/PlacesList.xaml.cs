using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private readonly PlacesListViewModel viewModel;

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            try
            {
                var selectedPlace = e.AddedItems[0] as Place;
                viewModel.SelectedPlace = selectedPlace;
            }
            catch (IndexOutOfRangeException)
            {
                viewModel.SelectedPlace = null;
            }
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
            else
            {
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