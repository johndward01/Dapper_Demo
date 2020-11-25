using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper_Demo
{
    class DapperDepartmentRepository : IDepartmentRepository
    {
        private readonly IDbConnection _connection;
        //Constructor

        public DapperDepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM departments;");
        }

        public void InsertDepartment(string newDepartmentName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
            new { departmentName = newDepartmentName });
        }

        public void UpdateDepartment(string updatedName, int id)
        {
            _connection.Execute("UPDATE departments SET Name = @name WHERE DepartmentID = @id;", new { name = updatedName, id = id });
        }

        public void DeleteDepartment(int id)
        {
            _connection.Execute("DELETE FROM departments WHERE DepartmentID = @id;", new { id = id });
        }

    }
}
