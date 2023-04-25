using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClient.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService invoiceService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;
        private readonly INotyfService notyfService;
        public InvoiceController(IInvoiceService invoiceService, IProductService productService, ICustomerService customerService, INotyfService notyfService)
        {
            this.invoiceService = invoiceService;
            this.productService = productService;
            this.customerService = customerService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var result = invoiceService.GetAllInvoices();
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Invoice invoice = new Invoice();
            ViewBag.Customer = customerService.GetAllCustomer().Select(x => new SelectListItem { Value = x.CustomerId.ToString(), Text = x.FirstName + " " + x.LastName }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.ProductName }).ToList();
            ViewBag.GetBuyingPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.getVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            invoice.InvoiceDetails.Add(new InvoiceDetail() { InvoiceDetailId = 1 });
            invoice.InvoiceCode = invoiceService.InvoiceCodes();
            return View(invoice);
        }
        [HttpPost]
        public IActionResult Create(Invoice invoice)
        {
            ViewBag.Customer = customerService.GetAllCustomer().Select(x => new SelectListItem { Value = x.CustomerId.ToString(), Text = x.FirstName + " " + x.LastName, Selected = x.CustomerId == invoice.CustomerId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.ProductName }).ToList();
            ViewBag.GetBuyingPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.getVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            if (ModelState.IsValid)
            {
                var result = invoiceService.CreateInvoice(invoice);
                if (result != null)
                {
                    notyfService.Success("Invoice Created Successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Create Invoice!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Model Error!!");
                return View(invoice);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var invoice = invoiceService.GetInvoiceById(id);
            ViewBag.Customer = customerService.GetAllCustomer().Select(x => new SelectListItem { Value = x.CustomerId.ToString(), Text = x.FirstName + " " + x.LastName, Selected = x.CustomerId == invoice.CustomerId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.ProductName }).ToList();
            ViewBag.GetBuyingPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.getVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            return View(invoice);
        }
        [HttpPost]
        public IActionResult Edit(Invoice invoice)
        {
            ViewBag.Customer = customerService.GetAllCustomer().Select(x => new SelectListItem { Value = x.CustomerId.ToString(), Text = x.FirstName + " " + x.LastName, Selected = x.CustomerId == invoice.CustomerId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Value = x.ProductId.ToString(), Text = x.ProductName }).ToList();
            ViewBag.GetBuyingPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.getVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            if (ModelState.IsValid)
            {
                var result = invoiceService.UpdateInvoice(invoice);
                if (result != null)
                {
                    notyfService.Success("Invoice Updated Successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Update Invoices!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Model is Having Errors!!");
                return View(invoice);
            }
        }
        public IActionResult Delete(int id)
        {
            invoiceService.DeleteInvoice(id);
            notyfService.Success("Invoices Deleted Successfully.");
            return RedirectToAction("Index");
        }
    }
}
