using ALD.LibFiscalCode.ViewModels;

namespace CodiceFiscaleUI.AboutView
{
    /// <summary>
    ///     Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow
    {
        private readonly AboutWindowViewModel viewModel;

        public AboutWindow()
        {
            viewModel = new AboutWindowViewModel();
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}