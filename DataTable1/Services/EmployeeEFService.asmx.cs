using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyDataTableApp.Db;
using MyDataTableApp.Db.Repositories;
using MyDataTableApp.Model;


namespace DataTable1.Services
{
    /// <summary>
    /// Summary description for EmployeeEFService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmployeeEFService : System.Web.Services.WebService
    {

        private readonly EmployeeRepository _employeeRepository;

        public EmployeeEFService()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public EmployeeModel AddNewEmployeeUsingEntityFramework(EmployeeModel employeeModel)
        {
            var employee = _employeeRepository.AddNewEmployee(employeeModel);

            return employee;
        }
    }
}
