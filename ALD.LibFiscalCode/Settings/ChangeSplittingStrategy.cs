using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ALD.LibFiscalCode.StringManipulation;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;

namespace ALD.LibFiscalCode.Settings
{
    internal class ChangeSplittingStrategy : ChangeSettingsBase, IChangeSettingsCommand
    {
        private string method;

        public ChangeSplittingStrategy(AppSettings settings, string method) : base(settings)
        {
            this.method = method;
        }

        public bool IsCompleted { get; set; }

        public void ChangeSetting()
        {
            if (method.ToUpper(CultureInfo.InvariantCulture) != "FAST" && method.ToUpper(CultureInfo.InvariantCulture) != "UNIDECODE")
            {
                throw new ApplicationException();
            }
            else
            {
                using var db = new AppDataContext();
                db.Settings.FirstOrDefault(s => s.Name == "SplittingMethod").StringValue = method;
                Target.SplittingStrategy = method;
            }
        }
    }
}