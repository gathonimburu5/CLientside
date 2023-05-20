using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly IMeasureService measureService;
        private readonly INotyfService notyfService;
        public ProductController(IProductService productService, ICategoryService categoryService, IMeasureService measureService, INotyfService notyfService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.measureService = measureService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var product = productService.GetAllProducts();
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Product product = new Product();
            product.ProductCode = productService.ProductCode();
            ViewBag.Category = categoryService.GetAllCategories().Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName }).ToList();
            ViewBag.Measure = measureService.GetAllMeasure().Select(x => new SelectListItem { Value = x.MeasureUnitId.ToString(), Text = x.MeasureUnitName }).ToList();
            return View(product);
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            ViewBag.Category = categoryService.GetAllCategories().Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName, Selected = x.CategoryId == product.CategoryId }).ToList();
            ViewBag.Measure = measureService.GetAllMeasure().Select(x => new SelectListItem { Value = x.MeasureUnitId.ToString(), Text = x.MeasureUnitName, Selected = x.MeasureUnitId == product.MeasureUnitId }).ToList();
            if (ModelState.IsValid)
            {
                var result = productService.CreateProduct(product);
                if (result != null)
                {
                    notyfService.Success("Product Successfully Created");
                    return RedirectToAction(nameof(Index), "Product");
                }
                else
                {
                    notyfService.Error("Failed to Create Product!!");
                    return View(product);
                }
            }
            else
            {
                notyfService.Warning("Model Error Occured!!");
                return View(product);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productService.GetProductById(id);
            ViewBag.Category = categoryService.GetAllCategories().Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName, Selected = x.CategoryId == product.CategoryId }).ToList();
            ViewBag.Measure = measureService.GetAllMeasure().Select(x => new SelectListItem { Value = x.MeasureUnitId.ToString(), Text = x.MeasureUnitName, Selected = x.MeasureUnitId == product.MeasureUnitId }).ToList();
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            ViewBag.Category = categoryService.GetAllCategories().Select(x => new SelectListItem { Value = x.CategoryId.ToString(), Text = x.CategoryName, Selected = x.CategoryId == product.CategoryId }).ToList();
            ViewBag.Measure = measureService.GetAllMeasure().Select(x => new SelectListItem { Value = x.MeasureUnitId.ToString(), Text = x.MeasureUnitName, Selected = x.MeasureUnitId == product.MeasureUnitId }).ToList();
            if (ModelState.IsValid)
            {
                var result = productService.UpdateProduct(product);
                if (result != null)
                {
                    notyfService.Success("Product Updated Successfully");
                    return RedirectToAction(nameof(Index), "Product");
                }
                else
                {
                    notyfService.Error("Error Occured while Updating Products Detsils!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model End!!");
                return View(product);
            }
        }
        public IActionResult Delete(int id)
        {
            var result = productService.DeleteProduct(id);
            if (result)
            {
                notyfService.Success("Product Details Deleted Successful");
                return RedirectToAction(nameof(Index), "Product");
            }
            else
            {
                notyfService.Error("Error Occurred while Deleting Products!!");
                return View(result);
            }
        }
    }
}
