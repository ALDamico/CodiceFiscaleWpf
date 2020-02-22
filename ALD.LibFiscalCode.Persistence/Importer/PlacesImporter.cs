using ALD.LibFiscalCode.Persistence.Interfaces;
using System.Text;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public class PlacesImporter:IPlaceImporter
    {
        public PlacesImporter(string filename, IImportStrategy strategy, int year)
        {
            //Required for compatibility with .NET Core
            Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Path = filename;
            ImportStrategy = strategy;
            Year = year;
            ExecuteImport();
        }

        public int Year { get; }
        public string Path { get; }
        public IImportStrategy ImportStrategy { get; }
        
        public void ExecuteImport()
        {
            ImportStrategy.ImportData(Path, Year);
        }
    }
}