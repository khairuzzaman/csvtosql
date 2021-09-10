using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvToSql.DataProcessor
{
    public interface IDataProcessHandler<T>
    {
        List<T> ReadData(string connectionString, string query, object param = null);
        void UpsertData(string connectionString, string insertQuery, string updateQuery = null, object insertData = null, object updateData = null);
    }
}
