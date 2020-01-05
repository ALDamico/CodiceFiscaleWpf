using System.IO;
using System.Windows;
using System.Windows.Controls;
using ALD.LibFiscalCode.ViewModels;
using Microsoft.Win32;

namespace CodiceFiscaleUI.PlaceImport
{
    /// <summary>
    ///     Logica di interazione per PlacesImportView.xaml
    /// </summary>
    public partial class PlacesImportView
    {
        private readonly PlaceImportViewModel viewModel;

        public PlacesImportView()
        {
            InitializeComponent();
            viewModel = new PlaceImportViewModel();
            DataContext = viewModel;
            ColSel.ItemsSource = viewModel.FieldMapping[0].AvailableProperties;
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Scegli il file CSV di origine",
                Filter = "Valori separati da virgola(*.csv)|*.csv",
                DefaultExt = "csv"
            };

            viewModel.InputFilename = dialog.FileName;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtFilename_LostFocus(object sender, RoutedEventArgs e)
        {
            //Force update of viewModel
            viewModel.InputFilename = TxtFilename.Text;
        }

        private void txtFilename_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Force updte of viewModel
            //This is required otherwise the button IsEnabled doesn't update quickly enough
            viewModel.InputFilename = TxtFilename.Text;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Import();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Non è stato possibile aprire il file {viewModel.InputFilename}",
                    "Errore durante l'apertura", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}