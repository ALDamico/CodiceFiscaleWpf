using ALD.LibFiscalCode.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.Models
{
    public class Person
    {
        public Person()
        {
            Name = "";
            Surname = "";
            DateOfBirth = DateTime.Now;
            Gender = Gender.Unspecified;
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
    }
}
