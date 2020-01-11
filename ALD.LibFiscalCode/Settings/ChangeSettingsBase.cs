using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Settings
{
    public abstract class ChangeSettingsBase
    {
        protected ChangeSettingsBase(AppSettings settings)
        {
            Target = settings;
            if (Target == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
        }

        protected AppSettings Target { get; set; }
    }
}