using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerService;
        private readonly INotyfService notyfService;
        private readonly ICurrencyService currencyService;
        public CustomerController(ICustomerService customerService, INotyfService notyfService, ICurrencyService currencyService)
        {
            this.customerService = customerService;
            this.notyfService = notyfService;
            this.currencyService = currencyService;
        }
        public IActionResult Index()
        {
            var customer = customerService.GetAllCustomer();
            return View(customer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Customer customer = new Customer();
            customer.CustomerCode = customerService.CustomerCode();
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Value = x.CurrencyId.ToString(), Text = x.CurrencyName }).ToList();
            return View(customer);
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Value = x.CurrencyId.ToString(), Text = x.CurrencyName, Selected = x.CurrencyId == customer.CurrencyId }).ToList();
            if (ModelState.IsValid)
            {
                var result = customerService.CreateCustomer(customer);
                if (result != null)
                {
                    notyfService.Success("Customer Created Successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Create Customer!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model EndPoint!!");
                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = customerService.GetCustomerById(id);
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Value = x.CurrencyId.ToString(), Text = x.CurrencyName, Selected = x.CurrencyId == customer.CurrencyId }).ToList();
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Value = x.CurrencyId.ToString(), Text = x.CurrencyName, Selected = x.CurrencyId == customer.CurrencyId }).ToList();
            if (ModelState.IsValid)
            {
                var result = customerService.UpdateCustomer(customer);
                if (result != null)
                {
                    notyfService.Success("Customer Updated Successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Update Customer!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model EndPoint!!");
                return View(customer);
            }
        }
        public IActionResult Delete(int id)
        {
            customerService.DeleteCustomer(id);
            notyfService.Success("Customer Deleted Successfully.");
            return RedirectToAction("Index");
        }
    }
}
