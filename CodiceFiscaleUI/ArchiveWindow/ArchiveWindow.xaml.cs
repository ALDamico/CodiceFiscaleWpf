using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;
using ALD.LibFiscalCode.ViewModels;
using Localization = ALD.LibFiscalCode.Localization.Localization;
using Microsoft.Win32;
using ALD.LibFiscalCode.Exporters;

namespace CodiceFiscaleUI.ArchiveWindow
{
    /// <summary>
    /// Interaction logic for ArchiveWindow.xaml
    /// </summary>
    public partial class ArchiveWindow : Window
    {
        private readonly ArchiveViewModel viewModel;

        public ArchiveWindow()
        {
            viewModel = new ArchiveViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }

        private void MnuExport_Click(object sender, RoutedEventArgs e)
        {
        }

        private void MnuCopy_Click(object sender, RoutedEventArgs e)
        {
            var eventType = e.RoutedEvent;
            var selectedElements = GrdPeople.SelectedItems;
            if (selectedElements.Count > 1)
            {
                MessageBox.Show("Copiare più codici fiscali non è un'operazione supportata!", "Non supportato!");
                return;
            }

            Clipboard.SetText((selectedElements[0] as Person).FiscalCode.FiscalCode);
            MessageBox.Show((selectedElements[0] as Person).FiscalCode.FiscalCode);
        }

        private IEnumerable<Person> GetSelectedItems()
        {
            return GrdPeople.SelectedItems as IEnumerable<Person>;
        }

        private void MnuExportToJson_Click(object sender, RoutedEventArgs e)
        {
            var selectedElements = GrdPeople.SelectedItems;
            var dialog = new SaveFileDialog();
            dialog.CheckFileExists = false;
            dialog.AddExtension = true;
            dialog.DefaultExt = ".json";
            dialog.Title = Localization.ExportDialogTitle;

            var elementsToExport = new List<PersonJson>();
            dialog.Filter = Localization.ExportDialogJsonFilter;
            dialog.FileName = "Export_" + DateTime.Now.ToString(DateFormat.FilenameSortable) + ".json";
            var response = dialog.ShowDialog(this);

            foreach (var person in selectedElements)
            {
                var currentFiscalCode = (person as Person).FiscalCode;
                var currentJsonObject = new PersonJson(person as Person, currentFiscalCode);
                elementsToExport.Add(currentJsonObject);
            }

            if (response.GetValueOrDefault() == true)
            {
                var exporter = new JsonExporter();
                exporter.Export(elementsToExport, dialog.FileName);
            }
        }
    }
}