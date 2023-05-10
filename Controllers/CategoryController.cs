using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly INotyfService notyfService;
        public CategoryController(ICategoryService categoryService, INotyfService notyfService)
        {
            this.categoryService = categoryService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            var currency = categoryService.GetAllCategories();
            return View(currency);
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            var result = categoryService.GetCategoryById(id);
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Category category = new Category();
            category.Status = "In-Active";
            return View(category);
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = categoryService.CreateCategory(category);
                if (result != null)
                {
                    notyfService.Success("Category Created Successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Create Category!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model EndPoint!!");
                return View(category);
            }
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = categoryService.GetCategoryById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = categoryService.UpdateCategory(category);
                if (result != null)
                {
                    notyfService.Success("Category Updated Successfully.");
                    return RedirectToAction("Index");
                }
                else
                {
                    notyfService.Error("Failed to Update Category!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error at the Model EndPoint!!");
                return View(category);
            }
        }
        public IActionResult Delete(int id)
        {
            categoryService.DeleteCategory(id);
            notyfService.Success("Category Deleted Successfully");
            return RedirectToAction("Index");
        }
    }
}
