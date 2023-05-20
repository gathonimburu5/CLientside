using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EmployeeClient.Controllers
{
    public class HeaderInvoiceController : Controller
    {
        private readonly IHeaderInvoiceService headerService;
        private readonly ISupplierService supplierService;
        private readonly IProductService productService;
        private readonly IReceivedService receivedService;
        private readonly INotyfService notificationService;
        public HeaderInvoiceController(IHeaderInvoiceService headerService, ISupplierService supplierService, IProductService productService, IReceivedService receivedService, INotyfService notificationService)
        {
            this.headerService = headerService;
            this.supplierService = supplierService;
            this.productService = productService;
            this.receivedService = receivedService;
            this.notificationService = notificationService;
        }
        public IActionResult Index()
        {
            var header = headerService.GetAllHeaderList();
            return View(header);
        }
        [HttpGet]
        public IActionResult Create()
        {
            PInvoiceHeader header = new PInvoiceHeader();
            header.InvoiceCode = headerService.GenerateInvoiceCode();
            header.PInvoiceDetails.Add(new PInvoiceDetail() { PInvoiceDetailId = 1 });
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString() }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductCode = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductCode, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.UnitPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            return View(header);
        }
        [HttpPost]
        public IActionResult Create(PInvoiceHeader model)
        {
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString(), Selected = x.SupplierId == model.SupplierId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductCode = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductCode, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.UnitPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            if (ModelState.IsValid)
            {
                var result = headerService.CreateHeader(model);
                if (result != null)
                {
                    notificationService.Success("Purchase Invoices Created Successfully.");
                    return RedirectToAction(nameof(Index), "HeaderInvoice");
                }
                else
                {
                    notificationService.Error("Error Occurred while Creating Purchase Invoice!!");
                    return View(result);
                }
            }
            else
            {
                notificationService.Warning("Model Error Occurred, Please Check Your Model!!!");
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult GoodReceived(int id)
        {
            PInvoiceHeader header = headerService.GetHeaderById(id);
            if (header == null) return BadRequest();
            var supplierName = supplierService.GetSupplierById(header.SupplierId);
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            GoodReceivedHeader goods = new GoodReceivedHeader
            {
                PInvoiceHeader = header,
                SupplierName = supplierName.SupplierName,
            };
            goods.GoodReceivedDetails.Add(new GoodReceivedDetail() { GoodReceivedDetailId = 1 });
            return View(goods);
        }
        [HttpPost]
        public IActionResult GoodReceived(GoodReceivedHeader header)
        {
            if (ModelState.IsValid)
            {
                var goods = headerService.CreateGood(header);
                if (goods != null)
                {
                    notificationService.Success("Goods Received Generated Successfully.");
                    return RedirectToAction(nameof(Index), "GoodReceived");
                }
                else
                {
                    notificationService.Error("Error Occurred while Generating Goods Received!!!");
                    return View(goods);
                }
            }
            else
            {
                return View(header);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var header = headerService.GetHeaderById(id);
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString(), Selected = x.SupplierId == header.SupplierId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductCode = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductCode, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.UnitPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            return View(header);
        }
        [HttpPost]
        public IActionResult Edit(PInvoiceHeader header)
        {
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString(), Selected = x.SupplierId == header.SupplierId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductCode = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductCode, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.UnitPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            if (ModelState.IsValid)
            {
                var result = headerService.UpdateHeader(header);
                if (result != null)
                {
                    notificationService.Success("Purchases Invoices Updated Successfully.");
                    return RedirectToAction(nameof(Index), "HeaderInvoice");
                }
                else
                {
                    notificationService.Error("Error Occurred while Updating Purchases Invoices!!");
                    return View(result);
                }
            }
            else
            {
                notificationService.Warning("Model Error! Please Check your Model Fields!!");
                return View(header);
            }
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var header = headerService.GetHeaderById(id);
            ViewBag.Supplier = supplierService.GetAllSuppliers().Select(x => new SelectListItem { Text = x.SupplierName, Value = x.SupplierId.ToString(), Selected = x.SupplierId == header.SupplierId }).ToList();
            ViewBag.Product = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductName, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductCode = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductCode, Value = x.ProductId.ToString() }).ToList();
            ViewBag.ProductVat = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductVat.ToString(), Value = x.ProductId.ToString() }).ToList();
            ViewBag.UnitPrice = productService.GetAllProducts().Select(x => new SelectListItem { Text = x.ProductBuyingPrice.ToString(), Value = x.ProductId.ToString() }).ToList();
            return View();
        }
        public IActionResult Delete(int id)
        {
            var result = headerService.DeleteHeader(id);
            if (result)
            {
                notificationService.Success("Purchases Invoice Deleted Successfully.");
                return RedirectToAction(nameof(Index), "HeaderInvoice");
            }
            else
            {
                notificationService.Error("Failed to Delete Purchase Invoice!!");
                return RedirectToAction("Index");
            }
        }
    }
}