﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using ALD.LibFiscalCode.ViewModels;
using CodiceFiscaleUI.DatePicker;
using Microsoft.Win32;
using Localization = ALD.LibFiscalCode.Localization.Localization;

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
                var response = MessageBox.Show(this, Localization.MsgBoxPendingChangesText,
                    Localization.MsgBoxPendingChangesCaption, MessageBoxButton.OKCancel, MessageBoxImage.Information,
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
            dialog.Title = Localization.FileDialogDatabaseFile;
            dialog.Multiselect = false;
            var result = dialog.ShowDialog(this);

            if (result.GetValueOrDefault() == true)
            {
                if (dialog.FileName.Equals(viewModel.DataSourceLocation, StringComparison.InvariantCulture))
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
                        Localization.MsgBoxInvalidDatabaseText,
                        Localization.MsgBoxInvalidDatabaseCaption,
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation,
                        MessageBoxResult.OK);
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
