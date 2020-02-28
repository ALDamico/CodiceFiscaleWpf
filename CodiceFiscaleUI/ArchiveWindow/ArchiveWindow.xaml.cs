using System;
using System.Collections.Generic;
using System.Windows;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;
using ALD.LibFiscalCode.ViewModels;
using Microsoft.Win32;
using ALD.LibFiscalCode.Exporters;
using System.Windows.Controls;
using System.Windows.Input;
using ALD.LibFiscalCode.Localization;

namespace CodiceFiscaleUI.ArchiveWindow
{
    /// <summary>
    /// Interaction logic for ArchiveWindow.xaml
    /// </summary>
    public partial class ArchiveView : Window
    {
        private readonly ArchiveViewModel viewModel;

        public ArchiveView()
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
            var selectedElements = GrdPeople.SelectedItems;
            if (selectedElements.Count > 1)
            {
                MessageBox.Show(ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxCopyNotSupportedText, ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxCopyNotSupportedCaption);
                return;
            }

            Clipboard.SetText((selectedElements[0] as Person).FiscalCode.FiscalCode);
        }

        private IEnumerable<Person> GetSelectedItems()
        {
            return GrdPeople.SelectedItems as IEnumerable<Person>;
        }

        private void MnuExportToXml_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxFeatureUnavailableText, ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxFeatureUnavailableCaption);
        }

        private void MnuExportToJson_Click(object sender, RoutedEventArgs e)
        {
            var selectedElements = GrdPeople.SelectedItems;
            var dialog = new SaveFileDialog
            {
                CheckFileExists = false,
                AddExtension = true,
                DefaultExt = ".json",
                Title = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ExportDialogTitle,
                Filter = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ExportDialogJsonFilter,
            };

            var elementsToExport = new List<PersonJson>();

            if (selectedElements.Count == 1)
            {
                dialog.FileName = (selectedElements[0] as Person).Name + "_" + (selectedElements[0] as Person).Surname + "_" +
                                      DateTime.Now.ToString(DateFormat.FilenameSortable) + ".json";
                var currentFiscalCode = (selectedElements[0] as Person).FiscalCode;
            }
            else
            {
                dialog.FileName = "Export_" + DateTime.Now.ToString(DateFormat.FilenameSortable) + ".json";
                foreach (var person in selectedElements)
                {
                    var currentFiscalCode = (person as Person).FiscalCode;
                    var currentJsonObject = new PersonJson(person as Person, currentFiscalCode);
                    elementsToExport.Add(currentJsonObject);
                }
            }
            var response = dialog.ShowDialog(this);

            if (selectedElements.Count == 1)
            {
                if (response.GetValueOrDefault() == true)
                {
                    var exporter = new JsonExporter();
                    exporter.Export(selectedElements[0] as Person, dialog.FileName);
                }
            }
            else
            {
                if (response.GetValueOrDefault() == true)
                {
                    var exporter = new JsonExporter();
                    exporter.Export(elementsToExport, dialog.FileName);
                }
            }
        }

        private void MnuDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedElements = GrdPeople.SelectedItems;
            var peopleList = new List<Person>();
            foreach (var element in selectedElements)
            {
                peopleList.Add(element as Person);
            }

            string message;
            if (peopleList.Count == 1)
            {
                message = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgDeleteTextSingular;
            }
            else
            {
                message = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgDeleteTextPlural;
            }

            var response = MessageBox.Show(message, ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgDeleteCaption, MessageBoxButton.OKCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
            if (response == MessageBoxResult.OK)
            {
                viewModel.DeletePeople(peopleList);
            }
        }

        private void GrdPeople_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var dg = sender as DataGrid;
            if (dg != null)
            {
                if (e.Key == Key.Delete)
                {
                    MnuDelete_Click(this, e);
                }
            }
        }
    }
}