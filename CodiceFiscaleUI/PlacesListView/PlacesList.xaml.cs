using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using Localization = ALD.LibFiscalCode.Localization.Localization;

namespace CodiceFiscaleUI.PlacesListView
{
    /// <summary>
    ///     Interaction logic for PlacesList.xaml
    /// </summary>
    public partial class PlacesList:Window
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
            loading = true;
            InitializeComponent();
        }

        public PlacesList(ICollection<Place> places, MainViewModel parentViewModel)
        {
            viewModel = new PlacesListViewModel(places);

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
            if (string.IsNullOrEmpty(filterText) ||  filterText.Equals(Localization.TxtFilterResults))
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
                TxtFilter.Text = Localization.TxtFilterResults;
            }
            else if (TxtFilter.Text.Equals(Localization.TxtFilterResults))
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
                TxtFilter.Text = Localization.TxtFilterResults;
            }
            else if (TxtFilter.Text.Equals(Localization.TxtFilterResults))
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