using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private string url = "https://localhost:7243/api/Customer";
        private HttpClient client = new HttpClient();
        public Customer CreateCustomer(Customer customer)
        {
            string json = JsonConvert.SerializeObject(customer);
            HttpResponseMessage respo = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (respo.IsSuccessStatusCode)
            {
                string result = respo.Content.ReadAsStringAsync().Result;
                var detail = JsonConvert.DeserializeObject<Customer>(result);
                if (detail != null) customer = detail;
            }
            else
            {
                string result = respo.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the End Point." + result);
            }
            return customer;
        }

        public string CustomerCode()
        {
            string code = "";
            var customerCodes = GetAllCustomer().Max(x => x.CustomerCode);
            if (customerCodes == null)
            {
                code = "CUSM/0001/" + DateTime.Now.Year;
            }
            else
            {
                int countDigit = 1;
                int.TryParse(customerCodes.Substring(5, 4), out countDigit);
                code = $"CUSM/{(countDigit + 1).ToString().PadLeft(4, '0')}/{DateTime.Now.Year}";
            }
            return code;
        }

        public bool DeleteCustomer(int id)
        {
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API End Point." + result);
            }
            return true;
        }

        public List<Customer> GetAllCustomer()
        {
            List<Customer> customer = new List<Customer>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string res = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Customer>>(res);
                if (data != null) customer = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API End Point." + result);
            }
            return customer;
        }

        public Customer GetCustomerById(int id)
        {
            Customer customer = new Customer();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var dat = JsonConvert.DeserializeObject<Customer>(result);
                if (dat != null) customer = dat;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API End Point." + result);
            }
            return customer;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            int id = customer.CustomerId;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(customer);
            HttpResponseMessage responseMessage = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string item = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the End Point!!" + item);
            }
            return customer;
        }
    }
}
