namespace ALD.LibFiscalCode.Persistence.Interfaces
{
    public interface IImportStrategy
    {
        void ImportData(string fileName, int year);

    }
}