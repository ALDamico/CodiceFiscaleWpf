using ALD.LibFiscalCode.Persistence.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.Persistence.ORM.Sqlite;
using ExcelDataReader;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ALD.LibFiscalCode.Persistence.Importer
{
    public class CsvImportStrategy:IImportStrategy
    {
        public void ImportData(string fileName, int year)
        {
            bool headerLine = true;
            List<Place> newPlaces = new List<Place>();
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx)
                using (var reader = ExcelReaderFactory.CreateCsvReader(stream))
                {
                    // Choose one of either 1 or 2:

                    // 1. Use the reader methods
                    do
                    {
                        while (reader.Read())
                        {
                            if (!headerLine)
                            {
                                var name = reader.GetString(5);
                                var region = reader.GetString(10);
                                var province = reader.GetString(11);
                                var provinceAbbreviation = reader.GetString(13);
                                var code = reader.GetString(18);
                                var newPlace = new Place()
                                {
                                    Name = name,
                                    Province = province,
                                    ProvinceAbbreviation = provinceAbbreviation,
                                    Code = code,
                                    Region = region
                                };
                                newPlaces.Add(newPlace);
                            }
                            headerLine = false;
                        }
                    } while (reader.NextResult());
                }

                using var dbContext = new AppDataContext();
                foreach (var place in newPlaces)
                {
                    if (!dbContext.Places.Contains(place, place.GetEqualityComparer()))
                    {
                        Task.Run(() => dbContext.AddAsync(place));
                    }
                }
                dbContext.SaveChanges();
            }
        }
    }
}