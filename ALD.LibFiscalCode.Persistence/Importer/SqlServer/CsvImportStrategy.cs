using System.IO;
using System.Threading.Tasks;
using ALD.LibFiscalCode.Persistence.Interfaces;

namespace ALD.LibFiscalCode.Persistence.Importer.SqlServer
{
    public class CsvImportStrategy:IImportStrategy
    {
        public CsvImportStrategy(Stream inputFile, int year)
        {
            Year = year;
            stream = stream;
        }
        public int Year { get; }
        private Stream stream;
        public void ImportData(string fileName, int year)
        {
            throw new System.NotImplementedException();
        }

        public Task ImportDataAsync(string fileName, int year)
        {
            throw new System.NotImplementedException();
        } 
    }
}