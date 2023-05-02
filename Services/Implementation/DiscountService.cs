using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;

namespace EmployeeClient.Services.Implementation
{
    public class DiscountService : IDiscountService
    {
        private string url = "https://localhost:7243/api/Discount";
        private HttpClient client = new HttpClient();
        public Discount CreateDiscount(Discount discount)
        {
            string json = JsonConvert.SerializeObject(discount);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Discount>(result);
                if (data != null) discount = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint" + result);
            }
            return discount;
        }

        public bool DeleteDiscount(int id)
        {
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("An Error Occured at the API End Points!" + result);
            }
            return true;
        }

        public List<Discount> GetAllDiscountList()
        {
            List<Discount> discount = new List<Discount>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var details = JsonConvert.DeserializeObject<List<Discount>>(result);
                if (details != null) discount = details;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the API EndPoint!" + result);
            }
            return discount;
        }

        public Discount GetDiscountById(int id)
        {
            Discount discount = new Discount();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var details = JsonConvert.DeserializeObject<Discount>(result);
                if (details != null) discount = details;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the API EndPoint!" + result);
            }
            return discount;
        }

        public Discount UpdateDiscount(Discount discount)
        {
            string json = JsonConvert.SerializeObject(discount);
            HttpResponseMessage responseMessage = client.PutAsync(url + "/" + discount.DiscountId, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string response = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoints " + response);
            }
            return discount;
        }
    }
}