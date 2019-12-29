using System.IO;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.Sqlite;
using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ALD.LibFiscalCode.Persistence.Enums;
using System.Threading.Tasks;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public static class PlacesImporter
    {
        public static async Task Import(string filename, ImporterConfiguration configuration, ImportMode mode = ImportMode.Update)
        {
            try
            {
                using (var reader = new StreamReader(filename))
                using (var csv = new CsvReader(reader, configuration.ToConfiguration()))
                using (var context = new PlacesContext())
                {
                    var records = csv.GetRecords<Place>();
                    csv.Configuration.RegisterClassMap(configuration.ClassMap);

                    if (mode == ImportMode.Overwrite)
                    {
                        context.Places.RemoveRange(context.Places);
                    }
                    var dbData = context.Places.ToList();
                    foreach (var record in records)
                    {
                        if (mode == ImportMode.Update)
                        {
                            if (!dbData.Contains(record, record.GetEqualityComparer()))
                            {
                                context.Places.Add(record);
                            }
                        }
                        else
                        {
                            context.Places.Add(record);
                        }
                    }
                    await context.SaveChangesAsync();
                }
                
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
    }
}
