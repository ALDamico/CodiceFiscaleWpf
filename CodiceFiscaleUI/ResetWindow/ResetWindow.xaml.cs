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
using ALD.LibFiscalCode.ViewModels;

namespace CodiceFiscaleUI.ResetWindow
{
    /// <summary>
    /// Logica di interazione per ResetWindow.xaml
    /// </summary>
    public partial class ResetWindow : Window
    {
        public ResetWindow()
        {
            InitializeComponent();
            viewModel = new ResetViewModel();
            DataContext = viewModel;
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private ResetViewModel viewModel;

        private void BtnStartRestore_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
