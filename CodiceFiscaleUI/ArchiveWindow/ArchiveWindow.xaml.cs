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

namespace CodiceFiscaleUI.ArchiveWindow
{
    /// <summary>
    /// Interaction logic for ArchiveWindow.xaml
    /// </summary>
    public partial class ArchiveWindow : Window
    {
        private ArchiveViewModel viewModel;
        public ArchiveWindow()
        {
            viewModel = new ArchiveViewModel();
            DataContext = viewModel;
            InitializeComponent();
            
        }
    }
}
