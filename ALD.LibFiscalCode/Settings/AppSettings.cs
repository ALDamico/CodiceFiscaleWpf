using System;
using System.Globalization;
using System.Linq;
using ALD.LibFiscalCode.Factories;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.StringManipulation;

namespace ALD.LibFiscalCode.Settings
{
    public class AppSettings
    {
        public CultureInfo AppLanguage { get; set; }

        public LanguageInfo InternalLanguage
        {
            get
            {
                using var db = new AppDataContext(DataSourceLocation);
                return db.Languages.FirstOrDefault(l => l.Iso2Code.Equals(AppLanguage.Name));
            }
        }

        public string DataSourceLocation { get; set; }
        public int MaxHistorySize { get; set; }
        public DateTime DefaultDate { get; set; }

        private static AppSettings _instance;

        public static AppSettings GetInstance(AppDataContext dataContext)
        {
            if (_instance == null)
            {
                _instance = AppSettingsFactory(dataContext);
            }

            return _instance;
        }

        public static AppSettings AppSettingsFactory(AppDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var instance = new AppSettings();
            var actualLanguage = SetLanguage(dataContext);

            instance.AppLanguage = CultureInfoFactory.GetCultureInfoFromLanguageInfo(actualLanguage);
            instance.DataSourceLocation = SetDataSourceLocation(dataContext);
            instance.MaxHistorySize = SetMaxHistorySize(dataContext);
            instance.DefaultDate = SetDefaultDate(dataContext);
            return instance;
        }

        private static LanguageInfo SetLanguage(AppDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var languageId = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("AppLanguage")).IntValue;

            var actualLanguage = dataContext.Languages.FirstOrDefault(l => l.Id == languageId);
            return actualLanguage;
        }

        private static string SetDataSourceLocation(AppDataContext dataContext)
        {
            return AppDomain.CurrentDomain.BaseDirectory + "\\DataSource\\app.db";
            /*
             This code is deprecated
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }

            var dataSourceLocation = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("DataSourceLocation"))
                .StringValue;
            if (string.IsNullOrWhiteSpace(dataSourceLocation))
            {
                dataSourceLocation = AppDomain.CurrentDomain.BaseDirectory + "\\DataSource\\app.db";
            }
            return dataSourceLocation;*/
        }

        private static int SetMaxHistorySize(AppDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var maxHistorySize = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("MaxHistorySize"))
                .IntValue;
            return maxHistorySize.GetValueOrDefault();
        }

        private static DateTime SetDefaultDate(AppDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var defaultDateString = dataContext.Settings.FirstOrDefault(s => s.Name.Equals("DefaultDate")).StringValue;
            var result = DateTime.TryParse(defaultDateString, out var defaultDate);
            return !result ? DateTime.Today : defaultDate;
        }

        public void ApplyChanges(AppDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            var settingsPersistence = dataContext.Settings;
            foreach (var setting in settingsPersistence)
            {
                if (setting.Name.Equals("AppLanguage"))
                {
                    setting.StringValue = AppLanguage.Name;
                }

                if (setting.Name.Equals("DataSourceLocation"))
                {
                    setting.StringValue = DataSourceLocation;
                }

                if (setting.Name.Equals("MaxHistorySize"))
                {
                    setting.IntValue = MaxHistorySize;
                }

                if (setting.Name.Equals("DefaultDate"))
                {
                    setting.StringValue = DefaultDate.ToString(DateFormat.RoundTripSchema);
                }
            }

            dataContext.SaveChanges();
        }
    }
}