using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvToSql.Queries
{
    public static class EmployeeQuery
    {
        public static string UPDATEQUERY = 
            @$"update employees 
                set FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                PhoneNumber = @PhoneNumber
                where Id = @Id";

        public static string INSERTQUERY =
            $@"insert into employees(Id, FirstName, LastName, Email, PhoneNumber) values(@Id, @FirstName, @LastName, @Email, @PhoneNumber)";
    }
}
