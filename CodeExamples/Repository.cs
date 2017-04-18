using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExamples
{
    public class Repository
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee { ID = 1, Name = "Alex", DeptID = 1 },
                new Employee { ID = 2, Name = "Finley", DeptID = 1 },
                new Employee { ID = 3, Name = "Elijah", DeptID = 2 }
            };
        }

        public static List<Department> GetDepartments()
        {
            return new List<Department>
            {
                new Department { ID = 1, Name = "Engineering" },
                new Department { ID = 2, Name = "Sales" },
                new Department { ID = 3, Name = "IT" }
            };
        }
    }
}
