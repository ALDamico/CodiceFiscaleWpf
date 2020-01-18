﻿using System.Globalization;
using System.Windows;
using System.Windows.Input;
using ALD.LibFiscalCode.ViewModels;
using CodiceFiscaleUI.DatePicker;
using Microsoft.Win32;

namespace CodiceFiscaleUI.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel viewModel;
        public SettingsWindow()
        {
            viewModel = new SettingsViewModel(((App)Application.Current).Settings);
            DataContext = viewModel;
            InitializeComponent();
        }


        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.ChangeSettingsInvoker.HasPendingActions)
            {
                var response = MessageBox.Show(this, "Hai delle modifiche in sospeso. Sei sicuro di voler uscire senza applicarle?",
                    "Modifiche in sospeso", MessageBoxButton.OKCancel, MessageBoxImage.Information,
                    MessageBoxResult.Cancel);
                if (response != MessageBoxResult.OK)
                {
                    return;
                }
            }
            Close();
        }

        private void BtnPickDbLocation_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Seleziona la posizione del file di dati";
            dialog.Multiselect = false;
            var result = dialog.ShowDialog(this);

            if (result.GetValueOrDefault() == true)
            {
                if (dialog.FileName.Equals(viewModel.DataSourceLocation))
                {
                    return;
                }
                if (dialog.FileName.EndsWith(".db", true, CultureInfo.InvariantCulture))
                {

                    viewModel.DataSourceLocation = dialog.FileName;
                }
                else
                {
                    MessageBox.Show(this,
                        "Il file non è un database di Codice Fiscale valido.\nLa posizione del database non è stata cambiata.", "File non valido", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
                }
            }
        }

        private void BtnConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ChangeSettings();

            Close();
        }

        private void TxtDefaultDate_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DatePickerWindow win = new DatePickerWindow(viewModel);
            win.DataContext = viewModel.DefaultDate;

            win.ShowDialog();
        }
    }
}