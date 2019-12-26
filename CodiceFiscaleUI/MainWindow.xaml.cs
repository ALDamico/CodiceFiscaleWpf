using ALD.LibFiscalCode.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodiceFiscaleUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(txtFiscalCode.Text);
        }

        private void txtCalendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var datePicker = new DatePicker.DatePicker(viewModel)
            {
                Owner = this,
                //DataContext = this.DataContext
            };
            datePicker.ShowDialog();
        }
        private readonly MainViewModel viewModel;

        private void btnReset_Click(object sender, RoutedEventArgs e)
        { 
            viewModel.ResetPerson();
        }

        private void drpGenderSelector_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            viewModel.SetGender((e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }
    }
}
