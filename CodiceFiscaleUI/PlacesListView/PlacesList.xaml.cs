using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public PlacesList(IEnumerable<Place> places)
        {
            viewModel = new PlacesListViewModel(places as ICollection<Place>);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name), ListSortDirection.Ascending));

            DataContext = viewModel;
            loading = true;
            InitializeComponent();
        }

        public PlacesList(ICollection<Place> places)
        {
            viewModel = new PlacesListViewModel(places);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name), ListSortDirection.Ascending));

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

            viewModel.SelectedPlace = viewModel.ViewSource.View.CurrentItem as Place;
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
                viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name), ListSortDirection.Ascending));
                viewModel.Filter(filterText);
            }
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = "";
            txtFilter.Foreground = Brushes.Black;
        }
    }
}