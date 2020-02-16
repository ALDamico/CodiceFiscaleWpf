using System;
using System.IO;

namespace ALD.LibFiscalCode.Settings
{
    /*
    public class ChangeDataSourceLocation : ChangeSettingsBase, IChangeSettingsCommand
    {
        private string targetPath;

        public ChangeDataSourceLocation(AppSettings settings, string targetPath) : base(settings)
        {
            this.targetPath = targetPath;
            IsCompleted = false;
        }

        public bool IsCompleted { get; set; }

        public void ChangeSetting()
        {
            
            if (File.Exists(targetPath))
            {
                Target.DataSourceLocation = targetPath;
                IsCompleted = true;
            }
            else
            {
                IsCompleted = false;
            }
        }
    }
    */
}