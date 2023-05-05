using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class SupplierService : ISupplierService
    {
        private readonly string url = "https://localhost:7243/api/Supplier";
        private readonly HttpClient client = new HttpClient();
        public Supplier CreateSupplier(Supplier supplier)
        {
            string json = JsonConvert.SerializeObject(supplier);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Supplier>(result);
                if (data != null) supplier = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occurred at the API EndPoint." + result);
            }
            return supplier;
        }

        public bool DeleteSupplier(int id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync(url + "/" + id).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the API EndPoint" + result);
            }
            return true;
        }

        public string GenerateSupplierCode()
        {
            string code = "";
            var supplierCode = GetAllSuppliers().Max(x => x.SupplierCode);
            if (supplierCode == null)
            {
                code = "SP/0001/" + DateTime.Now.Year;
            }
            else
            {
                int lastDigit = 1;
                if (!string.IsNullOrEmpty(supplierCode))
                {
                    int.TryParse(supplierCode.Substring(3, 4), out lastDigit);
                }
                code = $"SP/{(lastDigit + 1).ToString().PadLeft(4, '0')}/{DateTime.Now.Year}";
            }
            return code;
        }

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Supplier>>(result);
                if (data != null) suppliers = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occurred at the API EndPoint");
            }
            return suppliers;
        }

        public Supplier GetSupplierById(int id)
        {
            Supplier supplier = new Supplier();
            HttpResponseMessage responseMessage = client.GetAsync(url + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Supplier>(result);
                if (data != null) supplier = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occurred at the API EndPoint");
            }
            return supplier;
        }

        public Supplier UpdateSupplier(Supplier supplier)
        {
            string json = JsonConvert.SerializeObject(supplier);
            HttpResponseMessage responseMessage = client.PutAsync(url + "/" + supplier.SupplierId, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occurred at the API EndPoint");
            }
            return supplier;
        }
    }
}
