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
    public class SettingsViewModel : AbstractNotifyPropertyChanged
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
            startDetectingChanges = true;
        }

        public bool ChangeSettings()
        {
            changeSettingsInvoker.ProcessPendingActions(settings);
            if (changeSettingsInvoker.HasPendingActions)
            {
                return false;
            }

            return true;
        }

        private bool startDetectingChanges;

        public AppSettings Settings => settings;
        private readonly AppSettings settings;
        private ChangeSettingsInvoker changeSettingsInvoker;
        private string splittingStrategy;

        public string SplittingStrategy
        {
            get
            {
                if (changeSettingsInvoker.PreviewChanges.ContainsKey("SplittingStrategy"))
                {
                    return (string) ChangeSettingsInvoker.PreviewChanges[nameof(Settings.SplittingStrategy)];
                }

                return splittingStrategy;
            }
            set 
            {
                splittingStrategy = value;
                ChangeSettingsInvoker.ChangeSplittingStrategy(value);

                if (startDetectingChanges)
                {
                    OnPropertyChanged(nameof(SplittingStrategy));
                }
                
            }
        }

        public int SelectedSplittingStrategy
        {
            get
            {
                if (splittingStrategy != null)
                {
                    return splittingStrategy == "FAST" ? 0 : 1;
                }

                return settings.SplittingStrategy == "FAST" ? 0 : 1;
            }
        }

        public ChangeSettingsInvoker ChangeSettingsInvoker
        {
            get => changeSettingsInvoker;
            internal set
            {
                changeSettingsInvoker = value;
                OnPropertyChanged(nameof(ChangeSettingsInvoker));
            }
        }
        /*
        public string DataSourceLocation
        {
            get => settings.DataSourceLocation;
            set
            {
                ChangeSettingsInvoker.ChangeDataSourceLocation(value);
                OnPropertyChanged(nameof(DataSourceLocation));
            }
        }*/

        public CultureInfoWithFlag CurrentLanguage
        {
            get
            {
                if (ChangeSettingsInvoker.PreviewChanges.ContainsKey(nameof(Settings.AppLanguage)))
                {
                    return (CultureInfoWithFlag)ChangeSettingsInvoker.PreviewChanges[nameof(Settings.AppLanguage)];
                }

                return currentLanguage;
            }
            set
            {
                currentLanguage = value;
                OnPropertyChanged(nameof(CurrentLanguage));
            }
        }

        private CultureInfoWithFlag currentLanguage;

        public DateTime DefaultDate
        {
            get
            {
                if (ChangeSettingsInvoker.PreviewChanges.ContainsKey(nameof(DefaultDate)))
                {
                    return (DateTime)ChangeSettingsInvoker.PreviewChanges[nameof(DefaultDate)];
                }

                return settings.DefaultDate;
            }
            set
            {
                ChangeSettingsInvoker.ChangeDefaultDate(value);
                OnPropertyChanged(nameof(DefaultDate));
            }
        }

        public ObservableCollection<CultureInfoWithFlag> AvailableLanguages { get; }

        public int MaxHistorySize
        {
            get
            {
                if (ChangeSettingsInvoker.PreviewChanges.ContainsKey(nameof(MaxHistorySize)))
                {
                    return (int)ChangeSettingsInvoker.PreviewChanges[nameof(MaxHistorySize)];
                }

                return settings.MaxHistorySize;
            }
            set
            {
                ChangeSettingsInvoker.ChangeMaxHistorySize(value);
                OnPropertyChanged(nameof(MaxHistorySize));
            }
        }
    }
}