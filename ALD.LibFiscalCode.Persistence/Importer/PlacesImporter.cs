using System.IO;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Enums;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public static class PlacesImporter
    {
        public static void Import(string filename, PlacesContext context, ImporterConfiguration configuration, ImportMode mode = ImportMode.Update)
        {
            using (var reader = new StreamReader(filename))
            using (var csv = new CsvReader(reader, configuration.ToConfiguration()))
            {
                var records = csv.GetRecords<Place>();
                if (mode == ImportMode.Overwrite)
                {
                    context.Places.RemoveRange(context.Places);
                }
                foreach (var record in records)
                {
                    if (mode == ImportMode.Update)
                    {
                        if (!context.Places.Contains(record, record.GetEqualityComparer()))
                        {
                            context.Places.Add(record);
                        }
                    }
                    else
                    {
                        context.Places.Add(record);
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
