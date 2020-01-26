using System;
using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.StringManipulation;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace ALD.LibFiscalCode.Settings
{
    class ChangeSplittingStrategy: ChangeSettingsBase, IChangeSettingsCommand
    {
        private Type splittingStrategy;

        public ChangeSplittingStrategy(AppSettings settings, Type splittingStrategy):base(settings)
        {
            if (!(splittingStrategy is ISplittingStrategy))
            {
                throw new ArgumentException();
            }

            this.splittingStrategy = splittingStrategy;
        }
        public bool IsCompleted { get; set; }
        public void ChangeSetting()
        {
            
        }
    }
}
