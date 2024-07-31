using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataTableApp.Model;

namespace MyDataTableApp.Db
{
    public class CustomMapper
    {
        public static List<EmployeeModel> MapToEmployeeModelList(List<Employee> results)
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            foreach(Employee employee in results)
            {
                EmployeeModel employeeModel = new EmployeeModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Position = employee.Position,
                    Office = employee.Office,
                    Salary = employee.Salary,
                    TotalCount = employee.TotalCount
                };

                employeeList.Add(employeeModel);
            }
            return employeeList;
        }
    }
}
