using System;

namespace ALD.LibFiscalCode.Settings
{
    public class ChangeMaxHistorySize : ChangeSettingsBase, IChangeSettingsCommand
    {
        private int? maxHistorySize;

        public ChangeMaxHistorySize(AppSettings settings, int? maxHistorySize) : base(settings)
        {
            this.maxHistorySize = maxHistorySize;
            IsCompleted = false;
        }

        public bool IsCompleted { get; set; }

        public void ChangeSetting()
        {
            Target.MaxHistorySize = maxHistorySize.GetValueOrDefault(0);
            IsCompleted = true;
        }
    }
}