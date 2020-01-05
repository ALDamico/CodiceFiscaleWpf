using ALD.LibFiscalCode.Enums;
using ALD.LibFiscalCode.Persistence.EqualityComparers;
using ALD.LibFiscalCode.Persistence.Events;
using System;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class Person : AbstractNotifyPropertyChanged
    {
        public Person()
        {
            Name = "";
            Surname = "";
            DateOfBirth = DateTime.Today;
            Gender = Gender.Unspecified;
            PlaceOfBirth = null;
            equalityComparer = new PersonEqualityComparer();
        }

        public int Id { get; set; }

        public bool Equals(Person other)
        {
            return equalityComparer.Equals(this, other);
        }

        public PersonEqualityComparer GetEqualityComparer()
        {
            return this.equalityComparer;
        }

        private PersonEqualityComparer equalityComparer;

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