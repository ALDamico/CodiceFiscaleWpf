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

namespace CodiceFiscaleUI.AboutView
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        private AboutWindowViewModel viewModel;

        public AboutWindow()
        {
            viewModel = new AboutWindowViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}