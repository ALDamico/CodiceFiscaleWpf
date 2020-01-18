using System;
using System.Globalization;
using ALD.LibFiscalCode.Persistence.Events;
using ALD.LibFiscalCode.Persistence.Localization;

namespace ALD.LibFiscalCode.ViewModels
{
    public class DatePickerViewModel : AbstractNotifyPropertyChanged
    {
        private DateTime selectedDateTime;

        public DatePickerViewModel(DateTime startingDate = default)
        {
            SelectedDateTime = startingDate;
        }

        

        public DateTime SelectedDateTime
        {
            get => selectedDateTime;
            set
            {
                selectedDateTime = value;
                OnPropertyChanged(nameof(SelectedDateTime));
            }
        }
    }
}