using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class BookService : IBookService
    {
        private string url = "https://localhost:7243/api/Book";
        HttpClient client = new HttpClient();
        public Book CreateBook(Book book)
        {
            string json = JsonConvert.SerializeObject(book);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Book>(result);
                if (data != null)
                    book = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint.." + result);
            }
            return book;
        }

        public void DeleteBook(int id)
        {
            url = url + "/" + id;
            Book book = new Book();
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API End Points.." + result);
            }
            return;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Book>>(result);
                if (data != null)
                    books = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the Api End point" + result);
            }
            return books;
        }

        public Book GetBookById(int id)
        {
            Book book = new Book();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var items = JsonConvert.DeserializeObject<Book>(result);
                if (items != null)
                    book = items;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error from the API EndPoints." + result);
            }
            return book;
        }

        public Book UpdateBook(Book book)
        {
            int id = book.Id;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(book);
            HttpResponseMessage responseMessage = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint.." + result);
            }
            return book;
        }
    }
}
