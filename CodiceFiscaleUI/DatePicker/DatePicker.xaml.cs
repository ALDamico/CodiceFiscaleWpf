using ALD.LibFiscalCode.ViewModels;
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

namespace CodiceFiscaleUI.DatePicker
{
    /// <summary>
    /// Interaction logic for DatePicker.xaml
    /// </summary>
    public partial class DatePicker : Window
    {
        private DatePicker()
        { 
            InitializeComponent();
        }

        public DatePicker(MainViewModel viewModel):this()
        {
            if (viewModel == null)
            {
                throw new ArgumentException("The viewModel argument cannot be null");
            }
            parentViewModel = viewModel;
            datePickerViewModel = new DatePickerViewModel(parentViewModel.CurrentPerson.DateOfBirth);
            DataContext = datePickerViewModel;
        }

        private void calDatePicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (sender.Equals(e.AddedItems[0]))
            {
                return;
            }*/
            datePickerViewModel.SelectedDateTime = ((DateTime)e.AddedItems[0]);
        }
        private MainViewModel parentViewModel;
        private DatePickerViewModel datePickerViewModel;

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            parentViewModel.CurrentPerson.DateOfBirth = datePickerViewModel.SelectedDateTime;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
