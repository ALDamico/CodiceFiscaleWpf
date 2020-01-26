﻿namespace ALD.LibFiscalCode.Persistence.Models
{
    public class FiscalCodeEntity
    {
        public string FiscalCode { get; set; }
        public Person Person { get; set; }

        public override string ToString()
        {
            return FiscalCode;
        }
    }
}