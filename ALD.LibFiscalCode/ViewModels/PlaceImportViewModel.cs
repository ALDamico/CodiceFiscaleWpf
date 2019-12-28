using ALD.LibFiscalCode.Persistence.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ALD.LibFiscalCode.ViewModels
{
    public class PlaceImportViewModel
    {
        public string InputFilename { get; set; }
        public ImportMode Mode { get; set; }
        
    }
}
