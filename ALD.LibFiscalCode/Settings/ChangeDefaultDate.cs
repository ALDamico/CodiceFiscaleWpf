using System;

namespace ALD.LibFiscalCode.Settings
{
    internal class ChangeDefaultDate : ChangeSettingsBase, IChangeSettingsCommand
    {
        private DateTime defaultDate;

        public ChangeDefaultDate(AppSettings settings, DateTime defaultDate) : base(settings)
        {
            Target = settings;
            if (Target != null)
            {
                this.defaultDate = defaultDate;
            }
            else
            {
                throw new ArgumentNullException(nameof(settings));
            }
        }

        public bool IsCompleted { get; set; }

        public void ChangeSetting()
        {
            Target.DefaultDate = defaultDate;
            IsCompleted = true;
        }
    }
}