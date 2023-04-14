using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private string url = "https://localhost:7243/api/Category";
        private HttpClient client = new HttpClient();
        public Category CreateCategory(Category category)
        {
            string json = JsonConvert.SerializeObject(category);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Category>(result);
                if (data != null) category = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the End Point." + result);
            }
            return category;
        }

        public bool DeleteCategory(int id)
        {
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the End Point." + result);
            }
            return true;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Category>>(result);
                if (data != null) categories = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error occured at the End-Point." + result);
            }
            return categories;
        }

        public Category GetCategoryById(int id)
        {
            Category category = new Category();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Category>(result);
                if (data != null) category = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the Api EndPoint" + result);
            }
            return category;
        }

        public Category UpdateCategory(Category category)
        {
            int id = category.CategoryId;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(category);
            HttpResponseMessage responseMessage = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the Api EndPoint." + result);
            }
            return category;
        }
    }
}
