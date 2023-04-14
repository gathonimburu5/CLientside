using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class CurrencyService : ICurrencyService
    {
        private string url = "https://localhost:7243/api/Currency";
        private HttpClient client = new HttpClient();
        public Currency CreateCurrency(Currency currency)
        {
            string json = JsonConvert.SerializeObject(currency);
            HttpResponseMessage response = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (response.IsSuccessStatusCode)
            {
                string content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Currency>(content);
                if (data != null) currency = data;
            }
            else
            {
                string result = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Error occured at the Api End Point" + result);
            }
            return currency;
        }

        public bool DeleteCurrency(int id)
        {
            url = url + "/" + id;
            Currency currency = new Currency();
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the End Point" + result);
            }
            return true;
        }

        public List<Currency> GetAllCurrency()
        {
            List<Currency> list = new List<Currency>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Currency>>(result);
                if (data != null) list = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the Api End Point" + result);
            }
            return list;
        }

        public Currency GetCurrencyById(int id)
        {
            url = url + "/" + id;
            Currency currency = new Currency();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Currency>(result);
                if (data != null) currency = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the EndPoint." + result);
            }
            return currency;
        }

        public Currency UpdateCurrency(Currency currency)
        {
            int id = currency.CurrencyId; url = url + "/" + id;
            string json = JsonConvert.SerializeObject(currency);
            HttpResponseMessage responseMessage = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the EndPoint." + result);
            }
            return currency;
        }
    }
}
