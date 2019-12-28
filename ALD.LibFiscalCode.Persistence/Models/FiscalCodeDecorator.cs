using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCodeDecorator
    {
        public FiscalCodeDecorator(FiscalCode code, bool isMain = false)
        {
            FiscalCode = code;
            IsMain = isMain;
        }
        public FiscalCode FiscalCode { get; private set; }
        public bool IsMain { get; set; }
    }
}
