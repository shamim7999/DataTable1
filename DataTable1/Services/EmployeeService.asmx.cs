using DataTable1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using DataTable1.Helper_Class;
using System.Diagnostics;

namespace DataTable1.Services
{
    /// <summary>
    /// Summary description for EmployeeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmployeeService : System.Web.Services.WebService
    {

        private readonly CustomDBConfig _dbConfig;

        public EmployeeService()
        {
            _dbConfig = new CustomDBConfig();
        }

        [WebMethod]
        public Tuple<List<Employee>, int> GetFilteredEmployees(FilterParameters parameter)
        {
            int start = parameter.start;
            int length = parameter.length;
            string name = parameter.name != null ? parameter.name.Trim() : null;
            string searchValue = parameter.search.value != null ? parameter.search.value.Trim() : null;
            string sortColumnName = parameter.order != null && parameter.order[0].name != null ? parameter.order[0].name.Trim() : null;
            string sortDirection = parameter.order != null && parameter.order[0].dir != null ? parameter.order[0].dir.Trim() : null;
            string position = parameter.position != null ? parameter.position.Trim() : null;
            string office = parameter.office != null ? parameter.office.Trim() : null;
            int? age = parameter.age != null ? Convert.ToInt32(parameter.age) : (int?)null;
            int? id = parameter.id != null ? Convert.ToInt32(parameter.id) : (int?)null;
            int? salary = parameter.salary != null ? Convert.ToInt32(parameter.salary) : (int?)null;

            List<Employee> employees = new List<Employee>();
            int filteredRows = 0;

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@searchValue", searchValue),
                new SqlParameter("@name", name),
                new SqlParameter("@position", position),
                new SqlParameter("@office", office),
                new SqlParameter("@id", id),
                new SqlParameter("@age", age),
                new SqlParameter("@salary", salary),
                new SqlParameter("@sortColumnName", sortColumnName),
                new SqlParameter("@sortDirection", sortDirection),
                new SqlParameter("@start", start),
                new SqlParameter("@length", length)
            };

            using (var reader = _dbConfig.ExecuteStoredProcedureWithReader("spGetEmployees", parameters))
            {
                while (reader.Read())
                {
                    Employee emp = new Employee
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Position = reader.GetString(2).Trim(),
                        Office = reader.GetString(3).Trim(),
                        Age = reader.GetInt32(4),
                        Salary = reader.GetInt32(5)
                    };

                    employees.Add(emp);
                    filteredRows = reader.GetInt32(6);
                }
            }

            return Tuple.Create(employees, filteredRows);
        }


        public int GetTotalEmployees()
        {

            int totalRows = 0;
            string query = "SELECT COUNT(*) AS totalEmployees FROM tblEmployee";

            using (var reader = _dbConfig.ExecuteQueryWithReader(query, null))
            {
                if (reader.Read())
                {
                    totalRows = reader.GetInt32(0);
                }
            }

            return totalRows;
        }


        public Employee GetEmployeeById(int id)
        {
            Employee employee = null;

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            using (var reader = _dbConfig.ExecuteStoredProcedureWithReader("getEmployeeById", parameters))
            {
                if (reader.Read())
                {
                    employee = new Employee
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString().Trim(),
                        Position = reader["Position"].ToString().Trim(),
                        Office = reader["Office"].ToString().Trim(),
                        Age = Convert.ToInt32(reader["Age"]),
                        Salary = Convert.ToInt32(reader["Salary"])
                    };
                }
            }

            return employee;
        }

        public Employee AddNewEmployee(Employee employee)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", employee.Name),
                new SqlParameter("@position", employee.Position),
                new SqlParameter("@office", employee.Office),
                new SqlParameter("@age", employee.Age),
                new SqlParameter("@salary", employee.Salary)
            };
            _dbConfig.ExecuteStoredProcedure("addNewEmployee", parameters);
            return employee;
        }


        public Employee UpdateEmployeeById(Employee employee)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", employee.Id),
                new SqlParameter("@name", employee.Name),
                new SqlParameter("@position", employee.Position),
                new SqlParameter("@office", employee.Office),
                new SqlParameter("@age", employee.Age),
                new SqlParameter("@salary", employee.Salary)
            };

            _dbConfig.ExecuteStoredProcedure("updateEmployeeById", parameters);
            return employee;
        }


        public void DeleteEmployeeById(int id)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };

            _dbConfig.ExecuteStoredProcedure("deleteEmployeeById", parameters);
            Debug.WriteLine($"Deleted with ID: {id}");
        }
    }
}
