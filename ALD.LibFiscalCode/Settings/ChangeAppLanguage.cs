using System.Linq;
using ALD.LibFiscalCode.Persistence.Factories;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;

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
            using var db = new AppDataContext();
            db.Settings.FirstOrDefault(s => s.Name == "AppLanguage").StringValue = Target.InternalLanguage.Iso2Code;
            IsCompleted = true;
        }
    }
}