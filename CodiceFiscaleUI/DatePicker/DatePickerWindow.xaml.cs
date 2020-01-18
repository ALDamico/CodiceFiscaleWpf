using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ALD.LibFiscalCode.ViewModels;

namespace CodiceFiscaleUI.DatePicker
{
    /// <summary>
    ///     Interaction logic for DatePicker.xaml
    /// </summary>
    public partial class DatePickerWindow
    {
        private readonly DatePickerViewModel datePickerViewModel;

        private readonly MainViewModel mainViewModel;
        private readonly SettingsViewModel settingsViewModel;

        private DatePickerWindow()
        {
            InitializeComponent();
            CalDatePicker.Focusable = false;
        }

        public DatePickerWindow(DateTime startingDate):this()
        {
            datePickerViewModel = new DatePickerViewModel(startingDate);
            DataContext = startingDate;
        }

        public DatePickerWindow(MainViewModel viewModel) : this()
        {
            mainViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            datePickerViewModel = new DatePickerViewModel(mainViewModel.CurrentPerson.DateOfBirth);
            DataContext = datePickerViewModel;
        }
        public DatePickerWindow(SettingsViewModel viewModel) : this()
        {
            settingsViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            datePickerViewModel = new DatePickerViewModel(settingsViewModel.DefaultDate);
            DataContext = datePickerViewModel;
        }

        private void calDatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            datePickerViewModel.SelectedDateTime = (DateTime)e.AddedItems[0];
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (mainViewModel != null)
            {
                mainViewModel.CurrentPerson.DateOfBirth = datePickerViewModel.SelectedDateTime;
            }
            else if (settingsViewModel != null)
            {
                settingsViewModel.DefaultDate = datePickerViewModel.SelectedDateTime;
            }
            Close();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/b4413872-59d0-4a06-9d20-8d21de126dc6/calendar-not-losing-focus-wpf-40?forum=wpf
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}