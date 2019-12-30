using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCodeEntity
    {
        public string FiscalCode { get; set; }
        public Person Person { get; set; }
    }
}
