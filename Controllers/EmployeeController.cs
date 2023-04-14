using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClient.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly INotyfService notyfService;
        public EmployeeController(IEmployeeService employeeService, INotyfService notyfService)
        {
            this.employeeService = employeeService;
            this.notyfService = notyfService;
        }

        public IActionResult Index()
        {
            List<Employee> employee;
            employee = employeeService.GetAll();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = employeeService.CreateEmployee(employee);
                if (result != null)
                {
                    notyfService.Success("Employee Created Successfully.");
                    return RedirectToAction(nameof(Index), "Employee");
                }
                else
                {
                    notyfService.Error("Failed to Create Employee, Check Your Connections");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error Occured at the Model");
                return View(employee);
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            return View(employee);

        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = employeeService.UpdateEmployee(employee);
                if (result != null)
                {
                    notyfService.Success("Successfully Updated Employee.");
                    return RedirectToAction(nameof(Index), "Employee");
                }
                else
                {
                    notyfService.Error("Failed to Update Employee!!");
                    return View(result);
                }
            }
            else
            {
                return View(employee);
            }
        }
        [HttpGet]
        public IActionResult Detail(Guid id)
        {
            Employee employee = employeeService.GetEmployeeById(id);
            return View(employee);
        }
        public IActionResult Delete(Guid id){
            employeeService.DeleteEmployee(id);
            notyfService.Success("Employee Deleted Successfully");
            return RedirectToAction(nameof(Index), "Employee");

        }
    }
}
