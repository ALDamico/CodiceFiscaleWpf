﻿using Unidecode.NET;

namespace ALD.LibFiscalCode.StringManipulation
{
    public class UnidecodeSplittingStrategy : ISplittingStrategy
    {
        public string Result { get; private set; }

        public UnidecodeSplittingStrategy(string targetString)
        {
            TargetString = targetString;
            Convert();
        }

        public string TargetString { get; }

        public void Convert()
        {
            Result = TargetString.ToUpperInvariant().Unidecode();
        }
    }
}