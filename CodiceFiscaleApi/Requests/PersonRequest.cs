using System;

namespace CodiceFiscaleApi.Requests
{
    public class PersonRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int BirthPlaceId { get; set; }
        public string Gender { get; set; }
    }
}