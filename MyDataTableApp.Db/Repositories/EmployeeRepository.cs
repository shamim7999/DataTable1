using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using MyDataTableApp.Model;
using MyDataTableApp.Helper;

namespace MyDataTableApp.Db.Repositories
{
    public class EmployeeRepository
    {
        public EmployeeModel AddNewEmployee(EmployeeModel employeeModel)
        {
            using(var context = new MyEmployeeDBEntities())
            {
                Employee employee = new Employee()
                {
                    Name = employeeModel.Name,
                    Office = employeeModel.Office,
                    Position = employeeModel.Position,
                    Salary = employeeModel.Salary,
                    Age = employeeModel.Age
                };
                context.Employee.Add(employee);
                context.SaveChanges();
                return employeeModel;
            }
        }

        public List<EmployeeModel> GetAllEmployees()
        {
            using(var context = new MyEmployeeDBEntities())
            {
                var result = context.Employee
                    .Select(employee => new EmployeeModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Age = employee.Age,
                        Office = employee.Office,
                        Position = employee.Position,
                        Salary = employee.Salary

                    }).ToList();

                return result;
            }
        }

        public Tuple<List<EmployeeModel>, int> GetFilteredEmployees(FilterParameters parameter)
        {
            int start = parameter.start;
            int length = parameter.length;
            string name = parameter.name?.Trim();
            string searchValue = parameter.search?.value?.Trim();
            string sortColumnName = parameter.order != null && parameter.order[0].name != null ? parameter.order[0].name.Trim() : null;
            string sortDirection = parameter.order != null && parameter.order[0].dir != null ? parameter.order[0].dir.Trim() : null;
            string position = parameter.position?.Trim();
            string office = parameter.office?.Trim();
            int? age = parameter.age != null ? Convert.ToInt32(parameter.age) : (int?)null;
            int? id = parameter.id != null ? Convert.ToInt32(parameter.id) : (int?)null;
            int? salary = parameter.salary != null ? Convert.ToInt32(parameter.salary) : (int?)null;

            using (var context = new MyEmployeeDBEntities())
            {
                var query = context.Employee.AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(searchValue))
                {
                    query = query.Where(e => e.Name.Contains(searchValue) || e.Position.Contains(searchValue) || e.Office.Contains(searchValue));
                }
                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(e => e.Name.Contains(name));
                }
                if (!string.IsNullOrEmpty(position))
                {
                    query = query.Where(e => e.Position.Contains(position));
                }
                if (!string.IsNullOrEmpty(office))
                {
                    query = query.Where(e => e.Office.Contains(office));
                }
                if (age.HasValue)
                {
                    query = query.Where(e => e.Age == age.Value);
                }
                if (id.HasValue)
                {
                    query = query.Where(e => e.Id == id.Value);
                }
                if (salary.HasValue)
                {
                    query = query.Where(e => e.Salary == salary.Value);
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
                {
                    query = query.OrderBy($"{sortColumnName} {sortDirection}");
                }
                else
                {
                    query = query.OrderBy(e => e.Id); // Default sort
                }

                // Get the total number of records (before pagination)
                int totalRecords = query.Count();

                // Apply pagination
                List<EmployeeModel> employeeModelList = query
                    .Select(e => new EmployeeModel()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Position = e.Position,
                        Office = e.Office,
                        Age = e.Age,
                        Salary = e.Salary
                    })
                    .Skip(start)
                    .Take(length)
                    .ToList();

                return Tuple.Create(employeeModelList, totalRecords);
            }
        }
    }
}
