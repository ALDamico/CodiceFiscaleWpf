using System;
using System.Collections.Generic;
using System.IO;
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

        /*private void SettingsWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            foreach (var lang in viewModel.AvailableLanguages)
            {
                if (lang.Icon == null || lang.Icon.Length == 0) continue;
                var image = new BitmapImage();
                using (var mem = new FileStream())
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    
                    image.EndInit();
                    lang.ActualIcon = image;
                }
            }
        }*/
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            //No validation of settings changed for now.
            Close();
        }
    }
}
