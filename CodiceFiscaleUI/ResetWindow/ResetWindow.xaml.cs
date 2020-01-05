using System.Windows;
using ALD.LibFiscalCode.Progress;
using ALD.LibFiscalCode.ViewModels;

namespace CodiceFiscaleUI.ResetWindow
{
    /// <summary>
    ///     Logica di interazione per ResetWindow.xaml
    /// </summary>
    public partial class ResetWindow
    {
        private readonly ResetViewModel viewModel;

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

        private void BtnStartRestore_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentProgress.ProgressChanged += ReportProgress;
            viewModel.DropHistory(viewModel.CurrentProgress);
        }

        private void ReportProgress(object sender, ResetProgress e)
        {
            PrgRestoreProgressBar.Value = e.Percentage;
            LblCurrentProgress.Content = e.CurrentTask;
        }
    }
}