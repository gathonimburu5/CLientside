using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeClient.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly INotyfService notyfService;
        public BookController(IBookService bookService, INotyfService notyfService)
        {
            this.bookService = bookService;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            List<Book> books = bookService.GetAllBooks();
            return View(books);
        }
        [HttpGet]
        public IActionResult View(int id)
        {
            var book = bookService.GetBookById(id);
            return View(book);
        }
        [HttpGet]
        public IActionResult Create()
        {
            Book book = new Book();
            return View(book);
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var result = bookService.CreateBook(book);
                if (result != null)
                {
                    notyfService.Success("Books Created Successfully.");
                    return RedirectToAction(nameof(Index), "Book");
                }
                else
                {
                    notyfService.Error("Error Occured while Creating Books!!");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Error Occured at the Model!!");
                return View(book);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = bookService.GetBookById(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var result = bookService.UpdateBook(book);
                if (result != null)
                {
                    notyfService.Success("Books Updated Successfully.");
                    return RedirectToAction(nameof(Index), "Book");
                }
                else
                {
                    notyfService.Error("Failed to Update Books");
                    return View(result);
                }
            }
            else
            {
                notyfService.Warning("Model Error Issue!!!");
                return View(book);
            }
        }
        public IActionResult Delete(int id)
        {
            bookService.DeleteBook(id);
            notyfService.Success("Books Deleted Successfully!");
            return RedirectToAction(nameof(Index), "Book");
        }
    }
}
