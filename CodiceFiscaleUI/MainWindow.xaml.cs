﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ALD.LibFiscalCode.Exporters;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Settings;
using ALD.LibFiscalCode.StringManipulation;
using ALD.LibFiscalCode.ViewModels;
using CodiceFiscaleUI.AboutView;
using CodiceFiscaleUI.DatePicker;
using CodiceFiscaleUI.PlaceImport;
using CodiceFiscaleUI.PlacesListView;
using Microsoft.Win32;
using ALD.LibFiscalCode.Localization;
using System.Diagnostics;
using CodiceFiscaleUI.Omocodes;
using System.Globalization;

namespace CodiceFiscaleUI
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly MainViewModel viewModel;

        public MainWindow()
        {
            settings = ((App)Application.Current).Settings;
            viewModel = new MainViewModel(settings);
            CultureInfo.DefaultThreadCurrentUICulture = settings.AppLanguage;
            CultureInfo.DefaultThreadCurrentCulture = settings.AppLanguage;
            ALD.LibFiscalCode.Localization.CodiceFiscaleUI.Culture = settings.AppLanguage;
            InitializeComponent();
        }

        private void BtnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TxtFiscalCode.Text);
        }

        private readonly AppSettings settings;

        private void TxtCalendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var datePicker = new DatePickerWindow(viewModel)
            {
                Owner = this
            };
            datePicker.ShowDialog();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBoxResult.None;
            if (viewModel.HasPendingChanges)
            {
                response = MessageBox.Show(ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxResetText,
                    ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxResetCaption,
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Question,
                    MessageBoxResult.Cancel);
            }

            if (response != MessageBoxResult.OK && response != MessageBoxResult.None) return;

            if (viewModel.CanUserInteract)
            {
                DrpPlaceOfBirth.SelectedItem = null;
                DrpGenderSelector.SelectedItem = null;
            }

            viewModel.ResetPerson(settings.DefaultDate);
        }

        private void DrpGenderSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                viewModel.SetGender((e.AddedItems[0] as ComboBoxItem)?.Content.ToString());
            }
        }

        private void DrpPlaceOfBirth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                return;
            }

            if (e.AddedItems[0] is Place selectedItem)
            {
                viewModel.ChangePlace(selectedItem);
            }
        }

        private void MnuPlaces_Click(object sender, RoutedEventArgs e)
        {
            ShowPlacesWindow();
        }

        private void ShowPlacesWindow()
        {
            var placesWin = new PlacesList(viewModel.Places, viewModel)
            {
                Owner = this
            };

            placesWin.Show();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            var check = viewModel.CalculateFiscalCode();
            if (!check.IsValid)
            {
                MessageBox.Show(check.GetValidationMessagesAsString(), ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ValidationDialogCaption, MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void MnuUpdatePlaces_Click(object sender, RoutedEventArgs e)
        {
            var updateWin = new PlacesImportView { Owner = this };
            updateWin.ShowDialog();
        }

        //private void MnuLstOmocode_Click(object sender, RoutedEventArgs e)
        //{
        //    var selectedCode = LstOmocode.SelectedItem;
        //    if (selectedCode != null)
        //    {
        //        viewModel.SetMainFiscalCode(selectedCode as FiscalCode);

        //        var tmp = (ListBoxItem)LstOmocode.ItemContainerGenerator.ContainerFromItem(LstOmocode.SelectedItem);

        //        foreach (var v in LstOmocode.Items)
        //        {
        //            if (LstOmocode.ItemContainerGenerator.ContainerFromItem(v) is ListBoxItem lbi)
        //            {
        //                lbi.FontWeight = FontWeights.Normal;
        //            }
        //        }

        //        if (tmp != null)
        //        {
        //            tmp.FontWeight = FontWeights.Bold;
        //        }
        //    }
        //}

        private void MnuQuit_Click(object sender, RoutedEventArgs e)
        {
            Window_Closing(this, new CancelEventArgs());
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (viewModel.HasPendingChanges)
            {
                var result =
                    MessageBox.Show(ALD.LibFiscalCode.Localization.CodiceFiscaleUI.DialogConfirmExitText,
                        ALD.LibFiscalCode.Localization.CodiceFiscaleUI.DialogConfirmExitCaption,
                        MessageBoxButton.OKCancel,
                        MessageBoxImage.Asterisk,
                        MessageBoxResult.Cancel);
                if (result == MessageBoxResult.OK)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void MnuAbout_OnClick(object sender, RoutedEventArgs e)
        {
            var aboutDialog = new AboutWindow { Owner = this };
            aboutDialog.ShowDialog();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = viewModel;
            var configWindow = Application.Current.Windows;
            foreach (var win in configWindow)
            {
                if (win != this)
                {
                    ((Window)win).Close();
                }
            }
        }

        private void BtnOpenPlaceList_OnClick(object sender, RoutedEventArgs e)
        {
            ShowPlacesWindow();
        }

        private void MnuOptions_OnClick(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new Settings.SettingsWindow { Owner = this };
            settingsWindow.ShowDialog();
        }

        private void MnuExportToJson_OnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.FiscalCode != null)
            {
                var dialog = new SaveFileDialog();
                dialog.CheckFileExists = false;
                dialog.AddExtension = true;
                dialog.Title = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ExportDialogTitle;
                dialog.DefaultExt = ".json";
                dialog.Filter = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ExportDialogJsonFilter;
                dialog.FileName = viewModel.CurrentPerson.Name + "_" + viewModel.CurrentPerson.Surname + "_" +
                                      DateTime.Now.ToString(DateFormat.FilenameSortable) + ".json";

                var response = dialog.ShowDialog(this);
                if (response.GetValueOrDefault() == true)
                {
                    viewModel.Export(dialog.FileName, new JsonExporter());
                }
            }
            else
            {
                MessageBox.Show(this, ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxExportUnavailableText,
                    ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxExportUnavailableCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MnuExportToXml_OnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.FiscalCode != null)
            {
                var dialog = new SaveFileDialog();
                dialog.CheckFileExists = false;
                dialog.AddExtension = true;
                dialog.Title = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ExportDialogTitle;
                dialog.DefaultExt = ".xml";
                dialog.Filter = ALD.LibFiscalCode.Localization.CodiceFiscaleUI.ExportDialogXmlFilter;
                dialog.FileName = viewModel.CurrentPerson.Name + "_" + viewModel.CurrentPerson.Surname + "_" +
                                  DateTime.Now.ToString(DateFormat.FilenameSortable) + ".xml";

                var response = dialog.ShowDialog(this);
                if (response.GetValueOrDefault() == true)
                {
                    viewModel.Export(dialog.FileName, new XmlExporter());
                }
            }
            else
            {
                MessageBox.Show(this, ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxExportUnavailableText,
                    ALD.LibFiscalCode.Localization.CodiceFiscaleUI.MsgBoxExportUnavailableCaption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MnuArchive_OnClick(object sender, RoutedEventArgs e)
        {
            var archiveWin = new ArchiveWindow.ArchiveView();
            archiveWin.Show();
        }

        private void MnuViewOmocodes_Click(object sender, RoutedEventArgs e)
        {
            var omocodeWindow = new OmocodeWindow(viewModel);
            omocodeWindow.Show();
        }

        private void MnuOpenWebsite_Click(object sender, RoutedEventArgs e)
        {
            //Broken in dotnet core, has to use this trick from here: https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            var url = "https://www.lucianodamico.info";
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
        }
    }
}