using System.Collections.Generic;
using System.Threading.Tasks;

namespace csvToSql.DataFileParser
{
    public interface ICsvFileHandler<T>
    {
        List<T> ReadCsvFile(string filePath);
    }
}
