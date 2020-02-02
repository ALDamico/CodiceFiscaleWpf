using System;
using System.IO;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using ALD.LibFiscalCode.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ALD.LibFiscalCode.Exporters
{
    public class JsonExporter : IExportStrategy
    {
        public void Export(Person targetPerson, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            var exportedObject = new PersonJson(targetPerson);
            var outputJson = JsonConvert.SerializeObject(exportedObject, Formatting.Indented);
            File.WriteAllLines(fileName, new[] { outputJson });
        }

        public void Export(IEnumerable<PersonJson> peopleList, string fileName)
        {
            var outputJson = JsonConvert.SerializeObject(peopleList, Formatting.Indented);
            File.WriteAllLines(fileName, new[] { outputJson });
        }

        public void Export(IEnumerable<Person> peopleList, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (peopleList == null)
            {
                throw new ArgumentNullException(nameof(peopleList));
            }

            var personJsonList = new List<PersonJson>();

            foreach (var person in peopleList)
            {
                var newPerson = new PersonJson(person, person.FiscalCode);
                personJsonList.Add(newPerson);
            }

            var outputJson = JsonConvert.SerializeObject(peopleList, Formatting.Indented);
            File.WriteAllLines(fileName, new[] { outputJson });
        }
    }
}