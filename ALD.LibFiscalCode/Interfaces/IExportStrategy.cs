using ALD.LibFiscalCode.ViewModels;

namespace ALD.LibFiscalCode.Interfaces
{
    public interface IExportStrategy
    {
        void Export(MainViewModel viewModel, string path);
    }
}