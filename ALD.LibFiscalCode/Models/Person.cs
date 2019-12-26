using ALD.LibFiscalCode.Enums;
using ALD.LibFiscalCode.Persistence.Models;
using System;

namespace ALD.LibFiscalCode.Models
{
    public class Person : AbstractNotifyPropertyChanged
    {
        public Person()
        {
            Name = "";
            Surname = "";
            DateOfBirth = DateTime.Now;
            Gender = Gender.Unspecified;
            PlaceOfBirth = null;
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public DateTime DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                OnPropertyChanged(nameof(PrettyDate));
            }
        }

        public Place PlaceOfBirth
        {
            get
            {
                return placeOfBirth;
            }
            set
            {
                placeOfBirth = value;
                OnPropertyChanged(nameof(PlaceOfBirth));
            }
        }

        private Place placeOfBirth;
        public Gender Gender { get; set; }

        public string PrettyDate => DateOfBirth.Date.ToShortDateString();

        private string _name;
        private string _surname;
        private DateTime _dateOfBirth;
    }
}