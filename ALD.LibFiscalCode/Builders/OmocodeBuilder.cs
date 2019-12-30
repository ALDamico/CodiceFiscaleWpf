using ALD.LibFiscalCode.Persistence.Models;
using System.Collections.Generic;
using ALD.LibFiscalCode.Lookups;
using ALD.LibFiscalCode.Persistence;
using System;
using ALD.LibFiscalCode.Persistence.Events;

namespace ALD.LibFiscalCode.Builders
{
    public class OmocodeBuilder: AbstractNotifyPropertyChanged
    {
        public OmocodeBuilder(FiscalCodeBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentException("The parameter builder can't be null");
            }
            fiscalCodeBuilder = builder;
            Omocodes = new List<FiscalCodeDecorator>();
            Omocodes.Add(new FiscalCodeDecorator(builder.ComputedFiscalCode));

            string partial = builder.ComputedFiscalCode.Surname + 
                builder.ComputedFiscalCode.Name + 
                builder.ComputedFiscalCode.DateOfBirthAndGender 
                + builder.ComputedFiscalCode.Place;

            foreach (var letter in partial)
            {
                if (char.IsDigit(letter))
                {
                    char newChar = OmocodeLookup.Get(letter);
                    partial = partial.Replace(letter, newChar);
                    FiscalCodeBuilder omocodeBuilder = new FiscalCodeBuilder(partial);
                    Omocodes.Add(new FiscalCodeDecorator(builder.ComputedFiscalCode));
                    OnPropertyChanged(nameof(Omocodes));
                }
            }
            Omocodes[0].IsMain = true;
        }

        public List<FiscalCodeDecorator> Omocodes { get; private set; }
        private FiscalCodeBuilder fiscalCodeBuilder;
    }
}