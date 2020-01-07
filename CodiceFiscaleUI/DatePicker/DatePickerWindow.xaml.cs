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

        private readonly MainViewModel parentViewModel;

        private DatePickerWindow()
        {
            InitializeComponent();
            CalDatePicker.Focusable = false;
        }

        public DatePickerWindow(MainViewModel viewModel) : this()
        {
            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            parentViewModel = viewModel;
            datePickerViewModel = new DatePickerViewModel(parentViewModel.CurrentPerson.DateOfBirth);
            DataContext = datePickerViewModel;
        }

        private void calDatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            datePickerViewModel.SelectedDateTime = (DateTime)e.AddedItems[0];
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            parentViewModel.CurrentPerson.DateOfBirth = datePickerViewModel.SelectedDateTime;
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