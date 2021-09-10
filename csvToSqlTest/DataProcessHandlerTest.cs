using csvToSql.DataFileParser;
using csvToSql.DataProcessor;
using csvToSql.Models;
using csvToSql.Queries;
using Dapper;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Xunit;

namespace csvToSqlTest
{
    public class DataProcessHandlerTest : IDisposable
    {
        private readonly string _connectionString = @"Data Source =(local); Integrated Security = true; Initial Catalog = csvToSql";
        IDataProcessHandler<Employee> _employeeDataHandler;
        ICsvFileHandler<Employee> _fileHandler;

        public DataProcessHandlerTest()
        {
            _employeeDataHandler = new DataProcessHandler<Employee>();
            _fileHandler = new CsvFileHandler<Employee>();

            using var _connection = new SqlConnection(_connectionString);
            _connection.Execute("create table employees(Id int, FirstName varchar(100), LastName varchar(100), Email varchar(100), PhoneNumber varchar(100))");
            _connection.Execute("insert into employees(Id, FirstName, LastName, Email, PhoneNumber) values(1, 'abu', 'zafor', 'zafor@gmail.com', '01726269947')");
        }
        
        public void Dispose()
        {
            using var _connection = new SqlConnection(_connectionString);
            _connection.Execute("drop table employees");
        }

        [Fact]
        public void Employee_Has_One_Record()
        {
            var data = _employeeDataHandler.ReadData(_connectionString, @"select * from employees");
            Assert.Single(data);
        }

        [Fact]
        public void Process_CsvFile_Add_One_Record_Update_One_Record()
        {
            var path = Path.GetFullPath("csvDataFile\\employee.csv");
            var csvData = _fileHandler.ReadCsvFile(path);

            var ids = csvData.Select(c => c.Id).ToArray();
            var existingRecord = _employeeDataHandler
                .ReadData(_connectionString, @"select id from employees where id IN @ids", new { ids = ids })
                .Select(c => c.Id).ToList();

            var insertRecord = csvData.Where(c => !existingRecord.Contains(c.Id)).ToList();
            var updateRecord = csvData.Where(c => existingRecord.Contains(c.Id)).ToList();

            _employeeDataHandler.UpsertData(_connectionString, EmployeeQuery.INSERTQUERY, EmployeeQuery.UPDATEQUERY, insertRecord, updateRecord);
            var data = _employeeDataHandler.ReadData(_connectionString, @"select * from employees");
            var updatedRecord = data.Where(c => c.Id == 1).FirstOrDefault();

            Assert.Equal(2, data.Count());
            Assert.Equal("Abu", updatedRecord.FirstName);
        }
    }
}
