using System;

namespace CodiceFiscaleApi.Responses
{
    public class DatabaseStatusResponse
    {
        public string DbName { get; set; }
        public bool PlacesTableExists { get; set; }
        public int PlacesCount { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}