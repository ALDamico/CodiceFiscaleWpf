using System;
using System.Collections.Generic;
using System.Text;
using ALD.LibFiscalCode.Persistence.Events;

namespace ALD.LibFiscalCode.ViewModels
{
    public class ResetViewModel:AbstractNotifyPropertyChanged
    {
        public bool ResetDataSource
        {
            get => _resetDataSource;
            set
            {
                _resetDataSource = value;
                OnPropertyChanged(nameof(ResetDataSource));
                OnPropertyChanged(nameof(CanStartRestore));
            }
        }

        public bool ResetHistory
        {
            get => _resetHistory;
            set
            {
                _resetHistory = value;
                OnPropertyChanged(nameof(ResetHistory));
                OnPropertyChanged(nameof(CanStartRestore));
            }
        }
        public bool CanStartRestore => ResetDataSource || ResetHistory;

        private bool _resetDataSource;
        private bool _resetHistory;
    }
}
