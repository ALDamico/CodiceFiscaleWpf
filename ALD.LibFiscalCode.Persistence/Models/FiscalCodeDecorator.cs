using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCodeDecorator
    {
        public FiscalCodeDecorator()
        {
            FiscalCode = null;
            IsMain = false;
        }
        public FiscalCodeDecorator(FiscalCode code, bool isMain = false)
        {
            FiscalCode = code;
            IsMain = isMain;
        }
        [NotMapped]
        public FiscalCode FiscalCode { get; private set; }
        public bool IsMain { get; set; }
        public static explicit operator FiscalCode(FiscalCodeDecorator decorated)
        {
            return decorated.FiscalCode;
        }

        public static explicit operator FiscalCodeDecorator(FiscalCode code)
        {
            return new FiscalCodeDecorator(code);
        }
    }
}
