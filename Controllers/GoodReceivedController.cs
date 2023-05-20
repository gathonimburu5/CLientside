using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClient.Controllers
{
    public class GoodReceivedController : Controller
    {
        private readonly IReceivedService receivedService;
        private readonly ISupplierService supplierService;
        private readonly IHeaderInvoiceService invoiceService;
        private readonly IProductService productService;
        private readonly INotyfService notification;
        public GoodReceivedController(IReceivedService receivedService, ISupplierService supplierService, IHeaderInvoiceService invoiceService, IProductService productService, INotyfService notification)
        {
            this.receivedService = receivedService;
            this.supplierService = supplierService;
            this.invoiceService = invoiceService;
            this.productService = productService;
            this.notification = notification;
        }
        public IActionResult Index()
        {
            var goods = receivedService.GetAllGoodreceived();
            return View(goods);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var header = receivedService.GetGoodreceivedById(id);
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString() }).ToList();
            ViewBag.Invoice = invoiceService.GetAllHeaderList().Select(x => new SelectListItem { Text = x.InvoiceCode, Value = x.PInvoiceHeaderId.ToString(), Selected = x.PInvoiceHeaderId == header.PInvoiceHeaderId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            return View(header);
        }
        [HttpGet]
        public IActionResult Create()
        {
            GoodReceivedHeader header = new GoodReceivedHeader();
            header.GoodReceivedDetails.Add(new GoodReceivedDetail() { GoodReceivedDetailId = 1 });
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString() }).ToList();
            ViewBag.SupplierInvoice = invoiceService.GetAllHeaderList().Select(x => new SelectListItem { Text = x.Supplier.SupplierName, Value = x.PInvoiceHeaderId.ToString(), Selected = x.PInvoiceHeaderId == header.PInvoiceHeaderId }).ToList();
            ViewBag.InvoiceCode = invoiceService.GetAllHeaderList().Select(x => new SelectListItem { Text = x.InvoiceCode, Value = x.PInvoiceHeaderId.ToString() }).ToList();
            ViewBag.Invoice = invoiceService.GetAllHeaderList().Select(x => new SelectListItem { Text = x.InvoiceCode, Value = x.PInvoiceHeaderId.ToString() }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            return View(header);
        }
        [HttpPost]
        public IActionResult Create(GoodReceivedHeader header)
        {
            if (ModelState.IsValid)
            {
                var goods = receivedService.CreateGoodReceived(header);
                if (goods != null)
                {
                    notification.Success("Good Received Successfully Created");
                    return RedirectToAction(nameof(Index), "GoodReceived");
                }
                else
                {
                    notification.Error("Failed to Create Goods Received!!");
                    return View(goods);
                }
            }
            else
            {
                notification.Warning("Model Error Occurred!!");
                return View(header);
            }
        }
    }
}
