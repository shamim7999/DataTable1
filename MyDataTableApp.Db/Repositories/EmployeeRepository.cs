using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataTableApp.Model;

namespace MyDataTableApp.Db.Repositories
{
    public class EmployeeRepository
    {
        public tblEmployee AddNewEmployee(EmployeeModel employeeModel)
        {
            using(var context = new MyEmployeeDBEntities())
            {
                tblEmployee employee = new tblEmployee()
                {
                    Name = employeeModel.Name,
                    Office = employeeModel.Office,
                    Position = employeeModel.Position,
                    Salary = employeeModel.Salary,
                    Age = employeeModel.Age
                };
                context.tblEmployee.Add(employee);
                context.SaveChanges();
                return employee;
            }
        }
    }
}
