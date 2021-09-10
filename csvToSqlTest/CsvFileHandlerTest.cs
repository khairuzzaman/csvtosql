using csvToSql.DataFileParser;
using csvToSql.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace csvToSqlTest
{
    public class CsvFileHandlerTest
    {
        ICsvFileHandler<Employee> _fileHandler = new CsvFileHandler<Employee>();

        [Fact]
        public void CSV_File_Has_Two_Records()
        {
            var path = Path.GetFullPath("csvDataFile\\employee.csv");
            var data = _fileHandler.ReadCsvFile(path);
            Assert.Equal(2, data.Count());
        }
    }
}
