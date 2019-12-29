using ALD.LibFiscalCode.ViewModels;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace CodiceFiscaleUI.PlaceImport
{
    /// <summary>
    /// Logica di interazione per PlacesImportView.xaml
    /// </summary>
    public partial class PlacesImportView : Window
    {
        public PlacesImportView()
        {
            
            InitializeComponent();
            viewModel = new PlaceImportViewModel();
            DataContext = viewModel;
            colSel.ItemsSource = viewModel.FieldMapping[0].AvailableProperties;
        }

        private PlaceImportViewModel viewModel;

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Scegli il file CSV di origine";
            dialog.Filter = "Valori separati da virgola(*.csv)|*.csv";
            dialog.DefaultExt = "csv";
            var result = dialog.ShowDialog();

            viewModel.InputFilename = dialog.FileName;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtFilename_LostFocus(object sender, RoutedEventArgs e)
        {
            //Force update of viewModel
            viewModel.InputFilename = txtFilename.Text;
        }

        private void txtFilename_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //Force updte of viewModel
            //This is required otherwise the button IsEnabled doesn't update quickly enough
            viewModel.InputFilename = txtFilename.Text;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                viewModel.Import();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show($"Non è stato possibile aprire il file {viewModel.InputFilename}", "Errore durante l'apertura", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
