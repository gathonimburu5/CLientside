using EmployeeClient.Models.Domain;

namespace EmployeeClient.Services.Interface
{
    public interface IBookService
    {
        Book CreateBook(Book book);
        Book UpdateBook(Book book);
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        void DeleteBook(int id);
    }
}
