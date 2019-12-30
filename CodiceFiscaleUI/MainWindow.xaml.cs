using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Media;
using CodiceFiscaleUI.ResetWindow;

namespace CodiceFiscaleUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtFiscalCode.Text);
        }

        private void txtCalendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var datePicker = new DatePicker.DatePickerWindow(viewModel)
            {
                Owner = this
            };
            datePicker.ShowDialog();
        }

        private readonly MainViewModel viewModel;

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult response = MessageBoxResult.None;
            if (viewModel.HasPendingChanges)
            {
                response = MessageBox.Show("Hai delle modifiche in sospeso. Sei sicuro di voler reimpostare?", "Conferma", MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            }

            if (response == MessageBoxResult.OK || response == MessageBoxResult.None)
            {
                drpPlaceOfBirth.SelectedItem = null;
                drpGenderSelector.SelectedItem = null;
                viewModel.ResetPerson();
            }
        }

        private void drpGenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                viewModel.SetGender((e.AddedItems[0] as ComboBoxItem).Content.ToString());
            }
        }

        private void drpPlaceOfBirth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }
            var selectedItem = e.AddedItems[0] as Place;
            if (selectedItem != null)
            {
                viewModel.ChangePlace(selectedItem);
            }
        }

        private void mnuPlaces_Click(object sender, RoutedEventArgs e)
        {
            var placesWin = new PlacesListView.PlacesList(viewModel.Places);
            placesWin.Owner = this;
            placesWin.Show();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            var check = viewModel.CalculateFiscalCode();
            if (!check.IsValid)
            {
                MessageBox.Show(check.GetValidationMessagesAsString(), "Errore di convalida!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void mnuUpdatePlaces_Click(object sender, RoutedEventArgs e)
        {
            var updateWin = new PlaceImport.PlacesImportView();
            updateWin.Owner = this;
            updateWin.ShowDialog();
        }

        private void mnuLstOmocode_Click(object sender, RoutedEventArgs e)
        {
            var selectedCode = lstOmocode.SelectedItem;
            if (selectedCode != null)
            {
                viewModel.SetMainFiscalCode(selectedCode as FiscalCode);

                var tmp = lstOmocode.ItemContainerGenerator.ContainerFromItem(lstOmocode.SelectedItem) as ListBoxItem;

                foreach (var v in lstOmocode.Items)
                {
                    var lbi = lstOmocode.ItemContainerGenerator.ContainerFromItem(v) as ListBoxItem;
                    lbi.FontWeight= FontWeights.Normal;
                }

                tmp.FontWeight = FontWeights.Bold;
            }
        }

        private void mnuQuit_Click(object sender, RoutedEventArgs e)
        {
            Window_Closing(this, new CancelEventArgs());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.HasPendingChanges)
            {
                var result = MessageBox.Show("Hai delle modifiche in sospeso che verranno perse se esci dall'applicazione. Sei sicuro?", "Conferma uscita", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.OK)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void mnuResetDataSource_Click(object sender, RoutedEventArgs e)
        {
            var resetWindow = new ResetWindow.ResetWindow();
            resetWindow.Owner = this;
            resetWindow.Show();
        }
    }
}