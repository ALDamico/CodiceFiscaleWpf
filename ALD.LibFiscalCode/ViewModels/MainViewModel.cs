using ALD.LibFiscalCode.Models;

namespace ALD.LibFiscalCode.ViewModels
{
    public class MainViewModel : AbstractNotifyPropertyChanged
    {
        public MainViewModel()
        {
            CurrentPerson = new Person();
        }

        public Person CurrentPerson
        {
            get => _currentPerson;
            set
            {
                _currentPerson = value;
                OnPropertyChanged(nameof(CurrentPerson));
            }
        }

        private Person _currentPerson;

        public void SetGender(string gender)
        {
            if (gender == "M")
            {
                CurrentPerson.Gender = Enums.Gender.Male;
            }
            else if (gender == "F")
            {
                CurrentPerson.Gender = Enums.Gender.Female;
            }
            else
            {
                CurrentPerson.Gender = Enums.Gender.Unspecified;
            }
        }

        public void ResetPerson()
        {
            CurrentPerson = new Person();
        }
    }
}