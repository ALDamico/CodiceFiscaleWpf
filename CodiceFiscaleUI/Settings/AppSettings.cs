using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using ALD.LibFiscalCode.Factories;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;

namespace CodiceFiscaleUI.Settings
{
    public class AppSettings
    {
        public CultureInfo AppLanguage { get; set; }
        public string DataSourceLocation { get; set; }
        public int MaxHistorySize { get; set; }
        public DateTime DefaultDate { get; set; }

        public static AppSettings AppSettingsFactory(AppDataContext dataContext)
        {
            var instance = new AppSettings();
            var actualLanguage = AppSettings.SetLanguage(dataContext);

            instance.AppLanguage = CultureInfoFactory.GetCultureInfoFromLanguageInfo(actualLanguage);
            instance.DataSourceLocation = SetDataSourceLocation(dataContext);
            instance.MaxHistorySize = SetMaxHistorySize(dataContext);
            instance.DefaultDate = SetDefaultDate(dataContext);
            return instance;
        }

        private static LanguageInfo SetLanguage(AppDataContext dataContext)
        {
            var languageId = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("AppLanguage")).IntValue;
            var actualLanguage = dataContext.Languages.FirstOrDefault(l => l.Id == languageId);
            return actualLanguage;
        }

        private static string SetDataSourceLocation(AppDataContext dataContext)
        {
            var dataSourceLocation = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("DataSourceLocation"))
                .StringValue;
            if (string.IsNullOrWhiteSpace(dataSourceLocation))
            {
                dataSourceLocation = AppDomain.CurrentDomain.BaseDirectory + "\\DataSource\\app.db";
            }
            return dataSourceLocation;
        }

        private static int SetMaxHistorySize(AppDataContext dataContext)
        {
            var maxHistorySize = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("MaxHistorySize"))
                .IntValue;
            return maxHistorySize.GetValueOrDefault();
        }

        private static DateTime SetDefaultDate(AppDataContext dataContext)
        {
            var defaultDateString = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("DefaultDate")).StringValue;
            var result = DateTime.TryParse(defaultDateString, out var defaultDate);
            return !result ? DateTime.Today : defaultDate;
        }
    }
}