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

namespace CodiceFiscaleUI.Omocodes
{
    /// <summary>
    /// Interaction logic for OmocodeWindow.xaml
    /// </summary>
    public partial class OmocodeWindow : Window
    {
        private OmocodeViewModel viewModel;

        public OmocodeWindow(MainViewModel parentViewModel)
        {
            viewModel = new OmocodeViewModel(parentViewModel.CurrentPerson, parentViewModel.Omocodes);
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}