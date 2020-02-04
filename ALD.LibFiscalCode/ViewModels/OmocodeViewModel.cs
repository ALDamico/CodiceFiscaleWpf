using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ALD.LibFiscalCode.ViewModels
{
    public class OmocodeViewModel : AbstractNotifyPropertyChanged
    {
        public OmocodeViewModel(Person targetPerson, List<FiscalCodeDecorator> fiscalCodes)
        {
            Person = targetPerson;
            Omocodes = new ObservableCollection<FiscalCodeDecorator>(fiscalCodes);
            OnPropertyChanged(nameof(Omocodes));
        }

        public Person Person
        {
            get
            {
                return person;
            }
            set
            {
                person = value;
                OnPropertyChanged(nameof(Person));
            }
        }

        private Person person;
        public ObservableCollection<FiscalCodeDecorator> Omocodes { get; }
    }
}