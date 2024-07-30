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
        public static List<EmployeeModel> MapToEmployeeModelList(ObjectResult<GetEmployees_Result> results)
        {
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            foreach(GetEmployees_Result employee in results)
            {
                EmployeeModel employeeModel = new EmployeeModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Position = employee.Position,
                    Office = employee.Office,
                    Salary = employee.Salary,
                    TotalCount = employee.TotalCount.Value
                };

                employeeList.Add(employeeModel);
            }
            return employeeList;
        }
    }
}
