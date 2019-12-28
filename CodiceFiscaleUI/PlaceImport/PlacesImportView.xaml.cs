using Microsoft.Win32;
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
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Scegli il file CSV di origine";
            dialog.Filter = "Valori separati da virgola(*.csv)|*.csv";
            dialog.DefaultExt = "csv";
            var result = dialog.ShowDialog();

            var filepath = dialog.FileName;
            
        }
    }
}
