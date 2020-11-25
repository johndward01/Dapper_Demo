using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper_Demo
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAllDepartments();
    }
}
