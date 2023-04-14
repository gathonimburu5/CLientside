using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class ProductService : IProductService
    {
        private string url = "https://localhost:7243/api/Product";
        private HttpClient client = new HttpClient();
        public Product CreateProduct(Product product)
        {
            string json = JsonConvert.SerializeObject(product);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Product>(content);
                if (data != null) product = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the Api End Point" + result);
            }
            return product;
        }

        public bool DeleteProduct(int id)
        {
            Product product = new Product();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string item = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the API" + item);
            }
            return true;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Product>>(response);
                if (data != null) products = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API" + result);
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            Product product = new Product();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string item = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Product>(item);
                if (data != null) product = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the API" + result);
            }
            return product;
        }

        public string ProductCode()
        {
            string code = "";
            var productCode = GetAllProducts().Max(x => x.ProductCode);
            if (productCode == null)
            {
                code = "PROD/0001" + DateTime.Now.Year;
            }
            else
            {
                int lastDigit = 1;
                int.TryParse(productCode.Substring(5, 4), out lastDigit);
                code = $"PROD/{(lastDigit + 1).ToString().PadLeft(4, '0')}/{DateTime.Now.Year}";
            }
            return code;
        }

        public Product UpdateProduct(Product product)
        {
            int id = product.ProductId;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(product);
            HttpResponseMessage responseMessage = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API" + result);
            }
            return product;
        }
    }
}
