using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using MyDataTableApp.Model;
using MyDataTableApp.Helper;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Entity.Core.Objects;

namespace MyDataTableApp.Db.Repositories
{
    public class EmployeeRepository
    {

        private IQueryable<Employee> ApplyFilters(IQueryable<Employee> query, FilterParameters parameter)
        {

            int start = parameter.start;
            int length = parameter.length;
            string name = parameter.name?.Trim();
            string searchValue = parameter.search?.value?.Trim();
            string position = parameter.position?.Trim();
            string office = parameter.office?.Trim();
            int? age = parameter.age != null ? Convert.ToInt32(parameter.age) : (int?)null;
            int? id = parameter.id != null ? Convert.ToInt32(parameter.id) : (int?)null;
            int? salary = parameter.salary != null ? Convert.ToInt32(parameter.salary) : (int?)null;


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

            return query;
        }

        private IQueryable<Employee> ApplySorting(IQueryable<Employee> query, FilterParameters parameter)
        {
            string sortColumnName = parameter.order?.FirstOrDefault()?.name?.Trim();
            string sortDirection = parameter.order != null && parameter.order[0].dir != null ? parameter.order[0].dir.Trim() : null;

            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy($"{sortColumnName} {sortDirection}");
            }
            else
            {
                query = query.OrderBy(e => e.Id); // Default sort
            }

            return query;
        }

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
            
            using (var context = new MyEmployeeDBEntities())
            {
                var query = context.Employee.AsQueryable();

                query = ApplyFilters(query, parameter);
                query = ApplySorting(query, parameter);

                int totalRecords = query.Count();

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
                    .Skip(parameter.start)
                    .Take(parameter.length)
                    .ToList();

                return Tuple.Create(employeeModelList, totalRecords);
            }

        }

        public ObjectResult<GetEmployees_Result> GetFilteredEmployeesFromStoredProcedure(FilterParameters parameters)
        {
            using (var context = new MyEmployeeDBEntities())
            
               return context.GetEmployees(parameters);
            }
        }

    }
}
