using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;

namespace CodiceFiscaleUI.InitialConfiguration
{
    /// <summary>
    /// Interaction logic for InitialConfigurationWindow.xaml
    /// </summary>
    public partial class InitialConfigurationWindow : Window
    {
        public InitialConfigurationWindow(AppDataContext context)
        {
            //Progbar.BeginAnimation(null, new System.Windows.Media.Animation.());
            InitializeComponent();
            PerformMigration(context).ConfigureAwait(true);
        }

        public async Task PerformMigration(AppDataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            await context.MigrateAsync().ConfigureAwait(true);
            canClose = true;
        }

        private bool canClose;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
            //e.Cancel = true;
        }
    }
}