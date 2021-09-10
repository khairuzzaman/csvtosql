using csvToSql.DataFileParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace csvToSql.DataProcessor
{
    public class DataProcessHandler<T> : IDataProcessHandler<T>
    {
        private ICsvFileHandler<T> _csvFileHandler;
        public DataProcessHandler()
        {
            _csvFileHandler = new CsvFileHandler<T>();
        }
        
        public List<T> ReadData(string connectionString, string query, object param = null)
        {
            using var _connection = new SqlConnection(connectionString);
            var data = _connection.Query<T>(query, param);
            return data.ToList();
        }

        public void UpsertData(string connectionString, string insertQuery, string updateQuery = null, object insertData = null, object updateData = null)
        {
            using var _connection = new SqlConnection(connectionString);
            if (!string.IsNullOrWhiteSpace(updateQuery))
            {
                _connection.Execute(updateQuery, updateData);
            }
            _connection.Execute(insertQuery, insertData);
        }
    }
}
