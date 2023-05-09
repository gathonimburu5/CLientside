using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClient.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService supplierService;
        private readonly ICurrencyService currencyService;
        private readonly INotyfService notyfService;
        public SupplierController(ISupplierService supplierService, ICurrencyService currencyService, INotyfService notyfService)
        {
            this.supplierService = supplierService;
            this.currencyService = currencyService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var result = supplierService.GetAllSuppliers();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Supplier supplier = new Supplier();
            supplier.SupplierCode = supplierService.GenerateSupplierCode();
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Text = x.CurrencyName, Value = x.CurrencyId.ToString() }).ToList();
            return View(supplier);
        }
        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Text = x.CurrencyName, Value = x.CurrencyId.ToString(), Selected = x.CurrencyId == supplier.CurrencyId }).ToList();
            if (ModelState.IsValid)
            {
                var result = supplierService.CreateSupplier(supplier);
                if (result != null)
                {
                    notyfService.Success("Supplier Created Successfully.");
                    return RedirectToAction(nameof(Index), "Supplier");
                }
                else
                {
                    notyfService.Error("Error Occurred while Creating Supplier!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error Occurred at the Model");
                return View(supplier);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = supplierService.GetSupplierById(id);
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Text = x.CurrencyName, Value = x.CurrencyId.ToString(), Selected = x.CurrencyId == supplier.CurrencyId }).ToList();
            return View(supplier);
        }
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            ViewBag.Currency = currencyService.GetAllCurrency().Select(x => new SelectListItem { Text = x.CurrencyName, Value = x.CurrencyId.ToString(), Selected = x.CurrencyId == supplier.CurrencyId }).ToList();
            if (ModelState.IsValid)
            {
                var result = supplierService.UpdateSupplier(supplier);
                if (result != null)
                {
                    notyfService.Success("Supplier Updated Successfully");
                    return RedirectToAction(nameof(Index), "Supplier");
                }
                else
                {
                    notyfService.Error("Error Occurred while Updating Supplier");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error Occurred on the Model");
                return View(supplier);
            }
        }
        public IActionResult Delete(int id)
        {
            var details = supplierService.DeleteSupplier(id);
            if (details)
            {
                notyfService.Success("Supplier Deleted Successfully.");
                return RedirectToAction(nameof(Index), "Supplier");
            }
            else
            {
                notyfService.Error("Error Occurred while Deleting Supplier");
                return RedirectToAction("Index");
            }
        }
    }
}
