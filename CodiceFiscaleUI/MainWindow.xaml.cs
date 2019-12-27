using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            var placesWin = new PlacesListView.PlacesList(this.viewModel.Places);
            placesWin.Owner = this;
            placesWin.Show();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            var check = viewModel.CalculateFiscalCode();
            if (!string.IsNullOrWhiteSpace(check))
            {
                MessageBox.Show(check, "Errore di convalida!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}