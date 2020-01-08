using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;

namespace CodiceFiscaleUI.PlacesListView
{
    /// <summary>
    ///     Interaction logic for PlacesList.xaml
    /// </summary>
    public partial class PlacesList
    {
        private readonly MainViewModel parentViewModel;

        private readonly PlacesListViewModel viewModel;

        private bool loading;

        public PlacesList(IEnumerable<Place> places)
        {
            viewModel = new PlacesListViewModel(places as ICollection<Place>);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name),
                ListSortDirection.Ascending));

            DataContext = viewModel;
            Resources = viewModel.LocalizationProvider.GetResourceDictionary();
            loading = true;
            InitializeComponent();
        }

        public PlacesList(ICollection<Place> places, MainViewModel parentViewModel)
        {
            viewModel = new PlacesListViewModel(places);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name),
                ListSortDirection.Ascending));

            DataContext = viewModel;
            Resources = viewModel.LocalizationProvider.GetResourceDictionary();
            loading = true;
            this.parentViewModel = parentViewModel;
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            viewModel.SelectedPlace = viewModel.ViewSource.View.CurrentItem as Place;
        }

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loading)
            {
                loading = false;
                return;
            }

            var filterText = (sender as TextBox)?.Text;
            if (string.IsNullOrEmpty(filterText) || filterText.Equals(Resources["TxtFilterResults"]))
            {
                viewModel.ResetFilter();
            }
            else
            {
                viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name),
                    ListSortDirection.Ascending));
                viewModel.Filter(filterText);
            }
        }

        private void TxtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFilter.Text))
            {
                TxtFilter.Foreground = Brushes.Gainsboro;
                TxtFilter.Text = Resources["TxtFilterResults"].ToString();
            }
            else if (TxtFilter.Text.Equals(Resources["TxtFilterResults"]))
            {
                TxtFilter.Text = "";
                TxtFilter.Foreground = Brushes.Black;
            }
        }

        private void BtnSelectPlace_OnClick(object sender, RoutedEventArgs e)
        {
            parentViewModel.SelectedPlace = viewModel.SelectedPlace;
            Close();
        }

        private void TxtFilter_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFilter.Text))
            {
                TxtFilter.Foreground = Brushes.Gainsboro;
                TxtFilter.Text = Resources["TxtFilterResults"].ToString();
            }
            else if (TxtFilter.Text.Equals(Resources["TxtFilterResults"]))
            {
                TxtFilter.Text = "";
                TxtFilter.Foreground = Brushes.Black;
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}