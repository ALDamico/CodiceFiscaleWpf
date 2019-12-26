using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.ViewModels
{
    public class DatePickerViewModel : AbstractNotifyPropertyChanged
    {
        public DatePickerViewModel(DateTime startingDate = default)
        {
            this.SelectedDateTime = startingDate;
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
        private DateTime selectedDateTime;
    }
}
