using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using ALD.LibFiscalCode.Settings;
using CodiceFiscaleUI.Settings;
using Microsoft.Data.Sqlite;

namespace CodiceFiscaleUI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public AppSettings Settings { get; set; }

        public App()
        {
            AppDataContext dataContext = new AppDataContext();

            try
            {
                Settings = AppSettings.AppSettingsFactory(dataContext);
            }
            catch (SqliteException)
            {
                var configWindow = new InitialConfiguration.InitialConfigurationWindow(dataContext);
                configWindow.Show();

                Settings = AppSettings.AppSettingsFactory(dataContext);
            }
            dataContext.Dispose();
        }
    }
}