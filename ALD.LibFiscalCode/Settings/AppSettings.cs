using System;
using System.Globalization;
using System.Linq;
using ALD.LibFiscalCode.Factories;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
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
                using var db = new AppDataContext();
                return db.Languages.FirstOrDefault(l => l.Iso2Code.Equals(AppLanguage.Name));
            }
        }

        //public string DataSourceLocation { get; set; }
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
            //instance.DataSourceLocation = SetDataSourceLocation(dataContext);
            instance.MaxHistorySize = SetMaxHistorySize(dataContext);
            instance.DefaultDate = SetDefaultDate(dataContext);
            instance.SplittingStrategyName = SetSplittingStrategy(dataContext);
            return instance;
        }

        private static string SetSplittingStrategy(AppDataContext dataContext)
        {
            if (dataContext == null)
            {
                throw new ArgumentNullException(nameof(dataContext));
            }
            return dataContext.Settings.FirstOrDefault(s => s.Name.Equals("SplittingStrategy"))?.StringValue;
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

            var maxHistorySizeSetting = settingsPersistence.FirstOrDefault(s => s.Name.Equals("MaxHistorySize"));
            if (maxHistorySizeSetting != null)
            {
                maxHistorySizeSetting.IntValue = MaxHistorySize;
            }
            

            var appLanguageSetting = settingsPersistence.FirstOrDefault(s => s.Name.Equals("AppLanguage"));
            if (appLanguageSetting != null)
            {
                appLanguageSetting.StringValue = AppLanguage.Name;
            }

            var defaultDateSetting = settingsPersistence.FirstOrDefault(s => s.Name.Equals("DefaultDate"));
            if (defaultDateSetting != null)
            {
                defaultDateSetting.StringValue = DefaultDate.ToString(DateFormat.RoundTripSchema);
            }

            var splittingStrategy = settingsPersistence.FirstOrDefault(s => s.Name.Equals("SplittingStrategy"));
            if (splittingStrategy != null)
            {
                splittingStrategy.StringValue = SplittingStrategyName;
            }

            dataContext.SaveChanges();
        }

        public ISplittingStrategy GetSplittingStrategy(string output)
        {
            using var db = new AppDataContext();
            var result = db.Settings.FirstOrDefault(s => s.Name.Equals("SplittingStrategy"));
            if (result == null)
            {
                return new UnidecodeSplittingStrategy(output);
            }
            if (result.StringValue.ToUpper(CultureInfo.InvariantCulture).Equals("FAST", StringComparison.InvariantCulture))
            {
                return new FastSplittingStrategy(output);
            }
            return new UnidecodeSplittingStrategy(output);
        }

        public string SplittingStrategyName { get; set; }
    }
}
