using System.ComponentModel;

namespace ALD.LibFiscalCode
{
    public class AbstractNotifyPropertyChanged:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
