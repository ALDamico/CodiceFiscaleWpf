﻿using ALD.LibFiscalCode.Persistence.Models;
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

        private readonly MainViewModel parentViewModel;

        public PlacesList(ICollection<Place> places, MainViewModel parentViewModel)
        {
            viewModel = new PlacesListViewModel(places);

            viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name), ListSortDirection.Ascending));

            DataContext = viewModel;
            loading = true;
            this.parentViewModel = parentViewModel;
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

        private void TxtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loading)
            {
                loading = false;
                return;
            }

            var filterText = (sender as TextBox)?.Text;
            if (string.IsNullOrEmpty(filterText) || filterText == "Filtra i risultati...")
            {
                viewModel.ResetFilter();
            }
            else
            {
                viewModel.ViewSource.SortDescriptions.Add(new SortDescription(nameof(Place.Name), ListSortDirection.Ascending));
                viewModel.Filter(filterText);
            }
        }

        private void TxtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFilter.Text))
            {
                TxtFilter.Foreground = Brushes.Gainsboro;
                TxtFilter.Text = "Filtra i risultati...";
            }
            else if (TxtFilter.Text == "Filtra i risultati...")
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
                TxtFilter.Text = "Filtra i risultati...";
            }
            else if (TxtFilter.Text == "Filtra i risultati...")
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