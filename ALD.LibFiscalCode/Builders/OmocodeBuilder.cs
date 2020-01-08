using System;
using System.Collections.Generic;
using ALD.LibFiscalCode.Lookups;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;

namespace ALD.LibFiscalCode.Builders
{
    public class OmocodeBuilder : AbstractNotifyPropertyChanged
    {
        private FiscalCodeBuilder fiscalCodeBuilder;

        public OmocodeBuilder(FiscalCodeBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentException("The parameter builder can't be null");
            }

            fiscalCodeBuilder = builder;
            Omocodes = new List<FiscalCodeDecorator>();
            Omocodes.Add(new FiscalCodeDecorator(builder.ComputedFiscalCode));

            var partial = builder.ComputedFiscalCode.Surname +
                          builder.ComputedFiscalCode.Name +
                          builder.ComputedFiscalCode.DateOfBirthAndGender
                          + builder.ComputedFiscalCode.PlaceCode;

            foreach (var letter in partial)
            {
                if (char.IsDigit(letter))
                {
                    var newChar = OmocodeLookup.Get(letter);
                    partial = partial.Replace(letter, newChar);
                    var omocodeBuilder = new FiscalCodeBuilder(partial);
                    Omocodes.Add(new FiscalCodeDecorator(builder.ComputedFiscalCode));
                    OnPropertyChanged(nameof(Omocodes));
                }
            }

            Omocodes[0].IsMain = true;
        }

        public List<FiscalCodeDecorator> Omocodes { get; }
    }
}