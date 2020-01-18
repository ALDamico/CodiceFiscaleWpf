using ALD.LibFiscalCode.Persistence.Factories;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Settings
{
    public class ChangeAppLanguage : ChangeSettingsBase, IChangeSettingsCommand
    {
        private LanguageInfo newLanguage;

        public ChangeAppLanguage(AppSettings settings, LanguageInfo languageInfo) : base(settings)
        {
            newLanguage = languageInfo;
        }

        public bool IsCompleted { get; set; }

        public void ChangeSetting()
        {
            Target.AppLanguage = CultureInfoFactory.GetCultureInfoFromLanguageInfo(newLanguage);
            IsCompleted = true;
        }
    }
}