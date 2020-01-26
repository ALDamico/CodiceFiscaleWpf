using System;
using ALD.LibFiscalCode.Persistence.Enums;
using ALD.LibFiscalCode.Persistence.EqualityComparers;
using ALD.LibFiscalCode.Persistence.Events;

namespace ALD.LibFiscalCode.Persistence.Models
{
    public class Person : AbstractNotifyPropertyChanged
    {
        private readonly PersonEqualityComparer equalityComparer;
        private DateTime _dateOfBirth;

        private string _name;
        private string _surname;

        private Place placeOfBirth;

        public Person(DateTime defaultDateOfBirth) : this()
        {
            DateOfBirth = defaultDateOfBirth;
        }

        public Person()
        {
            Name = "";
            Surname = "";

            DateOfBirth = DateTime.Today;
            Gender = Gender.Unspecified;
            PlaceOfBirth = null;
            equalityComparer = new PersonEqualityComparer();
        }

        public FiscalCodeEntity FiscalCode { get; set; }

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
                OnPropertyChanged(nameof(PrettyDate));
            }
        }

        public Place PlaceOfBirth
        {
            get => placeOfBirth;
            set
            {
                placeOfBirth = value;
                OnPropertyChanged(nameof(PlaceOfBirth));
            }
        }

        public Gender Gender { get; set; }

        public string PrettyDate => DateOfBirth.Date.ToShortDateString();

        public bool Equals(Person other)
        {
            return equalityComparer.Equals(this, other);
        }

        public PersonEqualityComparer GetEqualityComparer()
        {
            return equalityComparer;
        }
    }
}