using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.Models;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Sqlite;

namespace ALD.LibFiscalCode.Settings
{
    public class ChangeSettingsInvoker
    {
        public ChangeSettingsInvoker()
        {
            pendingActions = new List<IChangeSettingsCommand>();
            target = null;
            PreviewChanges = new Dictionary<string, object>();
        }

        public Dictionary<string, object> PreviewChanges { get; private set; }

        public ChangeSettingsInvoker(AppSettings targetSettings) : this()
        {
            target = targetSettings;
        }

        public void ChangeDefaultDate(DateTime newDate)
        {
            PreviewChanges["DefaultDate"] = newDate;
            pendingActions.Add(new ChangeDefaultDate(target, newDate));
        }

        public void ChangeDataSourceLocation(string path)
        {
            pendingActions.Add(new ChangeDataSourceLocation(target, path));
            PreviewChanges["DataSourceLocation"] = path;
        }

        public void ChangeMaxHistorySize(int? newSize)
        {
            pendingActions.Add(new ChangeMaxHistorySize(target, newSize));
            PreviewChanges["MaxHistorySize"] = newSize;
        }

        public void ChangeAppLanguage(LanguageInfo languageInfo)
        {
            pendingActions.Add(new ChangeAppLanguage(target, languageInfo));
            PreviewChanges["AppLanguage"] = languageInfo;
        }

        public void ProcessPendingActions(AppSettings settings)
        {
            if (target == null)
            {
                throw new NullReferenceException();
            }


            foreach (var action in pendingActions)
            {
                action.ChangeSetting();


            }
            using var dataContext = new AppDataContext(settings.DataSourceLocation);
            settings.ApplyChanges(dataContext);
        }

        public bool HasPendingActions => pendingActions.Any(a => a.IsCompleted == false);

        private readonly AppSettings target;
        private ICollection<IChangeSettingsCommand> pendingActions;
    }
}