using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ALD.LibFiscalCode.ViewModels;
using ALD.LibFiscalCode.Localization;
using Microsoft.Win32;
using Localization = ALD.LibFiscalCode.Localization.Localization;

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
                Title = Localization.OpenFileDialogTitle,
                Filter = Localization.OpenFileDialogFilter,
                DefaultExt =  Localization.OpenFileDialogDefaultExt
            };

            viewModel.InputFilename = dialog.FileName;
            dialog.ShowDialog();
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
            //Force update of viewModel
            //This is required otherwise the button IsEnabled doesn't update quickly enough
            viewModel.InputFilename = TxtFilename.Text;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            var result = viewModel.Import().Result;

            if (result != null)
            {
                
                MessageBox.Show(string.Format(Localization.ErrorDialogText,  viewModel.InputFilename),
                    Localization.ErrorDialogCaption,
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
