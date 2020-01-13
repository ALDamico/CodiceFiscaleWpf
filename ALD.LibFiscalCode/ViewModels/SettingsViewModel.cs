using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using ALD.LibFiscalCode.Persistence;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using ALD.LibFiscalCode.Settings;

namespace ALD.LibFiscalCode.ViewModels
{
    public class SettingsViewModel:AbstractNotifyPropertyChanged
    {
        public SettingsViewModel(AppSettings settings)
        {
            this.settings = settings;
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            changeSettingsInvoker = new ChangeSettingsInvoker(settings);
            using var db = new AppDataContext();
            AvailableLanguages = new ObservableCollection<CultureInfoWithFlag>();
            foreach (var lang in db.Languages)
            {
                AvailableLanguages.Add(CultureInfoWithFlag.FromLanguageInfo(lang));
            }

            CurrentLanguage = AvailableLanguages[0];
            base.OnPropertyChanged(nameof(AvailableLanguages));
            base.OnPropertyChanged(nameof(Settings));
            base.OnPropertyChanged(nameof(CurrentLanguage));
        }

        public AppSettings Settings => settings;
        private AppSettings settings;
        private ChangeSettingsInvoker changeSettingsInvoker;

        internal ChangeSettingsInvoker ChangeSettingsInvoker
        {
            get => changeSettingsInvoker;
            set
            {
                changeSettingsInvoker = value;
                OnPropertyChanged(nameof(ChangeSettingsInvoker));
            }
        }

        public string DataSourceLocation
        {
            get => settings.DataSourceLocation;
            set
            {
                ChangeSettingsInvoker.ChangeDataSourceLocation(value);
                OnPropertyChanged(nameof(DataSourceLocation));
            }
        }

        public CultureInfoWithFlag CurrentLanguage
        {
            get => currentLanguage;
            set
            {
                currentLanguage = value;
                OnPropertyChanged(nameof(CurrentLanguage));
            }
        }
        private CultureInfoWithFlag currentLanguage;

        public ObservableCollection<CultureInfoWithFlag> AvailableLanguages { get; }

        public int MaxHistorySize
        {
            get => settings.MaxHistorySize;
            set
            {
                ChangeSettingsInvoker.ChangeMaxHistorySize(value);
                OnPropertyChanged(nameof(MaxHistorySize));
            }
        }
    }
}