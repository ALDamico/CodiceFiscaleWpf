using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Settings
{
    public class ChangeSettingsInvoker
    {
        public ChangeSettingsInvoker()
        {
            pendingActions = new Queue<IChangeSettingsCommand>();
            target = null;
        }

        public ChangeSettingsInvoker(AppSettings targetSettings) : this()
        {
            target = targetSettings;
        }

        public void ChangeDefaultDate(DateTime newDate)
        {
            pendingActions.Enqueue(new ChangeDefaultDate(target, newDate));
        }

        public void ChangeDataSourceLocation(string path)
        {
            pendingActions.Enqueue(new ChangeDataSourceLocation(target, path));
        }

        public void ChangeMaxHistorySize(int? newSize)
        {
            pendingActions.Enqueue(new ChangeMaxHistorySize(target, newSize));
        }

        public void ChangeAppLanguage(LanguageInfo languageInfo)
        {
            pendingActions.Enqueue(new ChangeAppLanguage(target, languageInfo));
        }

        public void ProcessPendingActions()
        {
            if (target == null)
            {
                throw new NullReferenceException();
            }

            Queue<IChangeSettingsCommand> newQ = new Queue<IChangeSettingsCommand>();
            foreach (var action in pendingActions)
            {
                action.ChangeSetting();

                var result = pendingActions.Dequeue();
                if (action.IsCompleted == false)
                {
                    newQ.Enqueue(result);
                }
            }

            pendingActions = newQ;
        }

        public bool HasPendingActions => pendingActions.Count != 0;

        private readonly AppSettings target;
        private Queue<IChangeSettingsCommand> pendingActions;
    }
}