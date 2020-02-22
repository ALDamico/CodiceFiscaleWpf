using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using ALD.LibFiscalCode.Settings;
using ALD.LibFiscalCode.ViewModels;
using CodiceFiscaleUI.DatePicker;
using Microsoft.Win32;
using ALD.LibFiscalCode.Localization;

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
            using var db = new AppDataContext();
            viewModel = new SettingsViewModel(AppSettings.GetInstance(db));
            DataContext = viewModel;
            InitializeComponent();
        }


        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            if (viewModel.ChangeSettingsInvoker.HasPendingActions)
            {
                var response = MessageBox.Show(this, LocalizationStrings.MsgBoxPendingChangesText,
                    LocalizationStrings.MsgBoxPendingChangesCaption, MessageBoxButton.OKCancel, MessageBoxImage.Information,
                    MessageBoxResult.Cancel);
                if (response != MessageBoxResult.OK)
                {
                    return;
                }
            }
            Close();
        }

        //We're not changing the database location anymore
        /*
        private void BtnPickDbLocation_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = LocalizationStrings.FileDialogDatabaseFile;
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
                        LocalizationStrings.MsgBoxInvalidDatabaseText,
                        LocalizationStrings.MsgBoxInvalidDatabaseCaption,
                        MessageBoxButton.OK,
                        MessageBoxImage.Exclamation,
                        MessageBoxResult.OK);
                }
            }
        }
        */
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

        private void DrpChangeSplittingMethod_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DrpChangeSplittingMethod.SelectedIndex == 0)
            {
                viewModel.SplittingStrategy = "FAST";
            }
            else
            {
                viewModel.SplittingStrategy = "UNIDECODE";
            }
            viewModel.ChangeSettingsInvoker.ChangeSplittingStrategy(viewModel.SplittingStrategy);
        }
    }
}
