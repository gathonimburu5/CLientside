using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private string url = "https://localhost:7243/api/Invoice";
        private HttpClient client = new HttpClient();
        public Invoice CreateInvoice(Invoice invoice)
        {
            string json = JsonConvert.SerializeObject(invoice);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string content = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Invoice>(content);
                if (result != null) invoice = result;
            }
            else
            {
                string response = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint. " + response);
            }
            return invoice;
        }

        public bool DeleteInvoice(int id)
        {
            Invoice invoice = new Invoice();
            url = url + "/" + id;
            HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint" + result);
            }
            return true;
        }

        public List<Invoice> GetAllInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string results = responseMessage.Content.ReadAsStringAsync().Result;
                    var items = JsonConvert.DeserializeObject<List<Invoice>>(results);
                    if (items != null) invoices = items;
                }
                else
                {
                    string repo = responseMessage.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error at the API EndPoint" + repo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at the API EndPoint" + ex.Message);
            }
            return invoices;
        }

        public Invoice GetInvoiceById(int id)
        {
            Invoice invoice = new Invoice();
            url = url + "/" + id;
            try
            {
                HttpResponseMessage responseMessage = client.GetAsync(url).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    string result = responseMessage.Content.ReadAsStringAsync().Result;
                    var item = JsonConvert.DeserializeObject<Invoice>(result);
                    if (item != null) invoice = item;
                }
                else
                {
                    string result = responseMessage.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error at the API EndPoint" + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error at the API EndPoint" + ex.Message);
            }
            return invoice;
        }

        public string InvoiceCodes()
        {
            string code = "";
            var invoiceCodes = GetAllInvoices().Max(x => x.InvoiceCode);
            if (invoiceCodes == null)
            {
                code = "IN/0001/" + DateTime.Now.Year;
            }
            else
            {
                int lastDigit = 1;
                int.TryParse(invoiceCodes.Substring(3, 4), out lastDigit);
                code = $"IN/{(lastDigit + 1).ToString().PadLeft(4, '0')}/{DateTime.Now.Year}";
            }
            return code;
        }

        public Invoice UpdateInvoice(Invoice invoice)
        {
            int id = invoice.InvoiceId;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(invoice);
            HttpResponseMessage responseMessage = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint" + result);
            }
            return invoice;
        }
    }
}
