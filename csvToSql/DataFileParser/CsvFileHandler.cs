using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace csvToSql.DataFileParser
{
    public class CsvFileHandler<T> : ICsvFileHandler<T>
    {
        public List<T> ReadCsvFile(string filePath)
        {
            try
            {
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true
                };
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, csvConfig))
                {
                    var records = csv.GetRecords<T>();
                    return records.ToList();
                }
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
