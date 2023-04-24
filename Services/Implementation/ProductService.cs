using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class ProductService : IProductService
    {
        private string url = "https://localhost:7243/api/Product";
        private HttpClient client = new HttpClient();
        public Product CreateProduct(Product product)
        {
            var Content = new MultipartFormDataContent();
            Content.Add(new StringContent(product.ProductCode), "ProductCode");
            Content.Add(new StringContent(product.ProductName), "ProductName");
            Content.Add(new StringContent(product.ProductDescription), "ProductDescription");
            Content.Add(new StringContent(product.ProductQty.ToString()), "ProductQty");
            Content.Add(new StringContent(product.ProductBuyingPrice.ToString()), "ProductBuyingPrice");
            Content.Add(new StringContent(product.ProductSellingPrice.ToString()), "ProductSellingPrice");
            Content.Add(new StringContent(product.ProductReorderLevel.ToString()), "ProductReorderLevel");
            Content.Add(new StringContent(product.ProductVat.ToString()), "ProductVat");
            Content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");
            Content.Add(new StringContent(product.MeasureUnitId.ToString()), "MeasureUnitId");
            Content.Add(new StringContent(product.ProductSupplier), "ProductSupplier");
            if(product.ProductPhoto != null){
                var fileContent = new StreamContent(product.ProductPhoto.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(product.ProductPhoto.ContentType);
                Content.Add(fileContent, "ProductPhoto", product.ProductPhoto.FileName);
            }
            HttpResponseMessage responseMessage = client.PostAsync(url, Content).Result;
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
            var Content = new MultipartFormDataContent();
            Content.Add(new StringContent(product.ProductCode), "ProductCode");
            Content.Add(new StringContent(product.ProductName), "ProductName");
            Content.Add(new StringContent(product.ProductDescription), "ProductDescription");
            Content.Add(new StringContent(product.ProductQty.ToString()), "ProductQty");
            Content.Add(new StringContent(product.ProductBuyingPrice.ToString()), "ProductBuyingPrice");
            Content.Add(new StringContent(product.ProductSellingPrice.ToString()), "ProductSellingPrice");
            Content.Add(new StringContent(product.ProductReorderLevel.ToString()), "ProductReorderLevel");
            Content.Add(new StringContent(product.ProductVat.ToString()), "ProductVat");
            Content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");
            Content.Add(new StringContent(product.MeasureUnitId.ToString()), "MeasureUnitId");
            Content.Add(new StringContent(product.ProductSupplier), "ProductSupplier");
            if(product.ProductPhoto != null){
                var fileContent = new StreamContent(product.ProductPhoto.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(product.ProductPhoto.ContentType);
                Content.Add(fileContent, "ProductPhoto", product.ProductPhoto.FileName);
            }
            HttpResponseMessage responseMessage = client.PutAsync(url, Content).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API End Point" + result);
            }
            return product;
        }
    }
}
