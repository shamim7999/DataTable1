using DataTable1.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using DataTable1.Services;
using System.Diagnostics;
using MyDataTableApp.Model;
using MyDataTableApp.Helper;

namespace DataTable1.Controllers
{
    //[RoutePrefix("Employee")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly EmployeeEFService _employeeEFService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
            _employeeEFService = new EmployeeEFService();
        }

        //[Route("Test")]
        public string Test()
        {
            return "TEST";
        }

        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Route("AddNew")]
        public ActionResult AddNewEmployee()
        {
            return View();
        }

        [HttpPost]
        //[Route("AddNew")]
        public ActionResult AddNewEmployee(Employee employee)
        {
            employee.Id = 0;
            if(ModelState.IsValid) {
                Debug.WriteLine($"Name: {_employeeService.AddNewEmployee(employee).Name}");
                return RedirectToAction("Index");
            }
            return View();
            
        }

        [HttpPost]
        //[Route("GetEmployeeList")]
        public ActionResult GetList(FilterParameters parameters)
        {
            
            int totalRows = _employeeService.GetTotalEmployees();
            Tuple<List<Employee>, int> result = _employeeService.GetFilteredEmployees(parameters);

            return Json(
                new
                {
                    data = result.Item1,
                    draw = parameters.draw,
                    recordsTotal = totalRows,
                    recordsFiltered = result.Item2

                }, JsonRequestBehavior.AllowGet
            );
        }

        [HttpGet]
        //[Route("UpdateEmployee/{id:int}")]
        public ActionResult UpdateEmployee(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            if(employee == null)
            {
                return HttpNotFound();
            }
            Debug.WriteLine($"in UpdateEmployee(GET) --> Employee Name: {employee.Name}");
            return View(employee);
        }

        [HttpPost]
        //[Route("UpdateEmployee")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            Employee updatedEmployee = _employeeService.UpdateEmployeeById(employee);
            Debug.WriteLine($"in UpdateEmployee(POST) --> Employee Name: {updatedEmployee.Name}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Route("DeleteEmployee/{id:int}")]
        public ActionResult DeleteEmployee(int id)
        {
            Employee employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            _employeeService.DeleteEmployeeById(id);
            return View("Index");
        }

        ///////////////// Using of Entity Framework  //////////////////////

        [HttpGet]
        public ActionResult AddNewEmployeeUsingEntityFramework()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddNewEmployeeUsingEntityFramework(EmployeeModel employeeModel)
        {
            employeeModel.Id = 0;
            if (ModelState.IsValid)
            {
                Debug.WriteLine($"Name: {_employeeEFService.AddNewEmployeeUsingEntityFramework(employeeModel).Name}");
                return RedirectToAction("Index");
            }
            return View();

        }

        [HttpPost]
        public ActionResult GetListUsingEntityFramework(FilterParameters parameters)
        {

            int totalRows = _employeeService.GetTotalEmployees();
            Tuple<List<EmployeeModel>, int> result = _employeeEFService.GetFilteredEmployeesUsingEntityFramework(parameters);

            return Json(
                new
                {
                    data = result.Item1,
                    draw = parameters.draw,
                    recordsTotal = totalRows,
                    recordsFiltered = result.Item2

                }, JsonRequestBehavior.AllowGet
            );
        }

    }
}