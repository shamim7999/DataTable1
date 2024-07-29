using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MyDataTableApp.Db.Repositories;
using MyDataTableApp.Model;
using MyDataTableApp.Helper;


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

        public Tuple<List<EmployeeModel>, int> GetFilteredEmployeesUsingEntityFramework(FilterParameters parameters)
        {
            return _employeeRepository.GetFilteredEmployeesFromStoredProcedure(parameters);
        }
    }
}
