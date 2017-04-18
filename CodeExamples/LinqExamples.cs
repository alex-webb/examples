using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;

namespace CodeExamples
{
    public class LinqExamples
    {
        //grouping
        public static void Grouping()
        {
            var employees = Repository.GetEmployees();

            // just group together by dept id
            var queryByDept =
                from e in employees
                group e by e.DeptID;

            // do more sorting in the group with into
            var queryByDeptInto =
                from e in employees
                group e by e.DeptID 
                    into eGroup
                    orderby eGroup.Key descending
                    where eGroup.Key < 3 // would use HAVING in SQL as it's filtering after aggregation
                    select eGroup;

            // group by multiple fields
            var queryByDeptIntoProjection =
                from e in employees
                group e by e.DeptID
                    into eGroup
                    orderby eGroup.Key descending
                    where eGroup.Key < 3
                    select new
                    {
                        DepartmentID = eGroup.Key,
                        Count = eGroup.Count(),
                        Employees = eGroup
                    };

            foreach(var group in queryByDeptIntoProjection)
            {
                Console.WriteLine("DeptID: {0}, Count: {1}",
                group.DepartmentID,
                group.Count);

                foreach (var employee in group.Employees)
                {
                    Console.WriteLine("\t{0}:{1}",
                        employee.DeptID,
                        employee.Name);
                }
            }

            // SAME AS EXTENSION METHODS
            var queryByDeptIntoProjectionExt =
                employees.GroupBy(e => e.DeptID)
                         .OrderByDescending(g => g.Key)
                         .Where(g => g.Key < 3)
                         .Select(g =>
                         new
                         {
                             DepartmentID = g.Key,
                             Count = g.Count(),
                             Employees = g
                         });
        }

        /// <summary>
        /// linq joins
        /// </summary>
        public static void Joins()
        {
            var employees = Repository.GetEmployees();
            var departments = Repository.GetDepartments();

            // comprehension syntax join = inner join with matches on outer & inner 
            var comprehensionSyntaxJoin =
                from d in departments
                join e in employees on d.ID equals e.DeptID
                select new
                {
                    DepartmentName = d.Name,
                    EmployeeName = e.Name
                };

            // extension method syntax join = inner join with matches on outer & inner 
            var extensionMethodSyntaxJoin =
                departments.Join(employees,
                                d => d.ID,
                                e => e.DeptID,
                                (d, e) => new
                                {
                                    DepartmentName = d.Name,
                                    EmployeeName = e.Name
                                });

            // comprehension syntax group join = outer join
            var comprehensionSyntaxGroupJoin =
                from d in departments
                join e in employees on d.ID equals e.DeptID
                    into eg
                select new
                {
                    DepartmentName = d.Name,
                    Employees = eg
                };


            // extension method syntax group join = outer join
            var extensionMethodSyntaxGroupJoin =
                departments.GroupJoin(employees,
                                d => d.ID,
                                e => e.DeptID,
                                (d, eg) => new
                                {
                                    DepartmentName = d.Name,
                                    Employees = eg
                                });

            // join on more fields using annonymous types
            //var comprehensionSyntaxGroupJoinOnMoreFields =
            //    from d in departments
            //    join e in employees on new { d.ID, d.Name } equals new { e.DeptID, e.Name }
            //        into eg
            //    select new
            //    {
            //        DepartmentName = d.Name,
            //        Employees = eg
            //    };

            // grouping
            var grouping = from e in employees
                           group e by e.DeptID;

            // grouping and projecting into annonymous type | GroupBy() = deferred (lazy operator) - LookUp = execution immediate (greedy operator)
            var groupingAndProjection = from e in employees
                                        group e by e.DeptID into eg
                                        select new
                                        {
                                            DepartmentID = eg,
                                            Employees = eg
                                        };

            // grouping and projection with extension method syntax
            var groupAndProjectExtensionMethodSyntax =
                employees.GroupBy(e => e.DeptID)
                         .Select(eg => new
                         {
                             DepartmentID = eg.Key,
                             Employees = eg
                         });

            // return all records in the join (outer join)
            var defaultIfEmpty =
                from d in departments
                join e in employees on d.ID equals e.DeptID
                    into eg
                from e in eg.DefaultIfEmpty()
                select new
                {
                    DepartmentName = d.Name,
                    Employee = e == null ? "" : e.Name
                };

            // Concat & Union
            string[] firstNames = { "Alex", "Christie", "Finley", "Elijah" };
            string[] lastNames = { "Finley", "Christie", "Webb", "Parker" };

            var concatNames = firstNames.Concat(lastNames).OrderBy(n => n);
            var unionNames = firstNames.Union(lastNames).OrderBy(n => n);
        }
        


        /// <summary>
        /// Check business rules with All()
        /// </summary>
        public static void AllQuantifier()
        {
            Book book = new Book { Author = "Herman", Name = "Moby Dick" };

            var bookValidationRules = new List<Func<Book, bool>>()
            {
                b => !String.IsNullOrEmpty(b.Name),
                b => !String.IsNullOrEmpty(b.Author)
            };

            bool isBookValid = bookValidationRules.All(rule => rule(book));
        }
        class Book
        {
            public string Author { get; set; }
            public string Name { get; set; }
        }
        // business rules Func
        public static void BusinessRules()
        {
            Employee employee = new Employee();
            //Employee employee = new Employee { ID = 2 };

            var rules = new List<Rule<Employee>>()
            {
                new Rule<Employee> { Test = e => !String.IsNullOrEmpty(e.Name),
                                     Message = "Employee name cannot be empty" },

                new Rule<Employee> { Test = e => e.DeptID > 0,
                                     Message = "Employee must have an assigned department"},

                new Rule<Employee> { Test = e => e.ID > 0,
                                     Message = "Employee must have an ID"}
            };

            // see if any rules fail
            bool isValid = rules.All(r => r.Test(employee));

            // find which rules failed
            if (!isValid)
            {
                var failedRules = rules.Where(r => r.Test(employee) == false);

                string errorMessage =
                    failedRules.Aggregate(new StringBuilder(),
                               (sb, r) => sb.AppendLine(r.Message),
                               sb => sb.ToString());
            }
        }
        class Rule<T>
        {
            public Func<T, bool> Test { get; set; }
            public string Message { get; set; }
        }


        /// <summary>
        /// Dynamic queries
        /// </summary>
        public static void DynamicQuery()
        {
            var employees = Repository.GetEmployees();

            var query = employees.AsQueryable()
                                 .OrderBy("Name")
                                 .Where("DeptID = 1");

            Console.WriteLine(query);
        }

    }
}
