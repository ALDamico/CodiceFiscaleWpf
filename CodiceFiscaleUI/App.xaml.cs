using System.Security.Cryptography.X509Certificates;
using ALD.LibFiscalCode.Persistence.Sqlite;
using CodiceFiscaleUI.Settings;

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
            using var dataContext = new AppDataContext();
            Settings = AppSettings.AppSettingsFactory(dataContext);
        }
    }
}