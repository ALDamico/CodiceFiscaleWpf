﻿using System.ComponentModel;

namespace ALD.LibFiscalCode
{
    public abstract class AbstractNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected internal virtual PropertyChangedEventHandler OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return PropertyChanged;
        }
    }
}