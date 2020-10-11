using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using ALD.LibFiscalCode.Localization;
using System.Linq;
using ALD.LibFiscalCode.Win32.ViewModels;

namespace CodiceFiscaleUI.PlacesListView
{
    /// <summary>
    ///     Interaction logic for PlacesList.xaml
    /// </summary>
    public partial class PlacesList : Window
    {
        private readonly MainViewModel parentViewModel;

        private readonly PlacesListViewModel viewModel;

        private bool loading;

        public PlacesList(IEnumerable<Place> places)
        {
            var placesFiltered = (
                from p in places
                where p.StartDate != null || p.EndDate != null
                select p
            ) as ICollection<Place>;
            viewModel = new PlacesListViewModel(placesFiltered as ICollection<Place>);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name),
                ListSortDirection.Ascending));

            DataContext = viewModel;
            loading = true;
            InitializeComponent();
        }

        public PlacesList(ICollection<Place> places, MainViewModel parentViewModel)
        {
            if (parentViewModel == null)
            {
                throw new NullReferenceException();
            }
            IEnumerable<Place> placesFiltered;
            placesFiltered = (
                from p in places
                where (p.StartDate == null || p.StartDate <= parentViewModel.CurrentPerson.DateOfBirth) &&
                      (p.EndDate == null || p.EndDate >= parentViewModel.CurrentPerson.DateOfBirth)
                select p
            ).ToArray();

            viewModel = new PlacesListViewModel(placesFiltered as ICollection<Place>);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name),
                ListSortDirection.Ascending));

            DataContext = viewModel;
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
            if (string.IsNullOrEmpty(filterText) || filterText.Equals(
                ALD.LibFiscalCode.Localization.CodiceFiscaleUI.TxtFilterResults, StringComparison.InvariantCulture))
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
                TxtFilter.Text = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.TxtFilterResults;
            }
            else if (TxtFilter.Text.Equals(ALD.LibFiscalCode.Localization.CodiceFiscaleUI.TxtFilterResults,
                StringComparison.InvariantCulture))
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
                TxtFilter.Text = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.TxtFilterResults;
            }
            else if (TxtFilter.Text.Equals(ALD.LibFiscalCode.Localization.CodiceFiscaleUI.TxtFilterResults,
                StringComparison.InvariantCulture))
            {
                TxtFilter.Text = "";
                TxtFilter.Foreground = Brushes.Black;
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void LstPlace_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var elemnt = (sender as ListBoxItem).DataContext as Place;
            BtnSelectPlace_OnClick(this, e);
        }
    }
}