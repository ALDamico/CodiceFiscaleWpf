using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.ViewModels;
using System.Collections.Generic;

namespace ALD.LibFiscalCode.Interfaces
{
    public interface IExportStrategy
    {
        void Export(Person person, string path);

        void Export(IEnumerable<Person> peopleList, string fileName);
    }
}