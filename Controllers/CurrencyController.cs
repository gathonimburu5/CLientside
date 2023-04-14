using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClient.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService currencyService;
        private readonly INotyfService notyfService;
        public CurrencyController(ICurrencyService currencyService, INotyfService notyfService)
        {
            this.currencyService = currencyService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var currency = currencyService.GetAllCurrency();
            return View(currency);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Currency currency = new Currency();
            return View(currency);
        }
        [HttpPost]
        public IActionResult Create(Currency currency)
        {
            if (ModelState.IsValid)
            {
                var result = currencyService.CreateCurrency(currency);
                if (result != null)
                {
                    notyfService.Success("Currency Created Successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Create Currency!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model End.");
                return View(currency);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var currency = currencyService.GetCurrencyById(id);
            return View(currency);
        }
        [HttpPost]
        public IActionResult Edit(Currency currency)
        {
            if (ModelState.IsValid)
            {
                var result = currencyService.UpdateCurrency(currency);
                if (result != null)
                {
                    notyfService.Success("Currency Updated Successfully");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Update Currency!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model End.");
                return View(currency);
            }
        }
        public IActionResult Delete(int id)
        {
            currencyService.DeleteCurrency(id);
            notyfService.Success("Currency Deleted Successful");
            return RedirectToAction("Index");
        }
    }
}
