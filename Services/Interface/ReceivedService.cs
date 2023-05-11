using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Implementation;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Interface
{
    public class ReceivedService : IReceivedService
    {
        private readonly string baseUrl = "https://localhost:7243/api/GoodReceive";
        private readonly HttpClient client = new HttpClient();
        public GoodReceivedHeader CreateGoodReceived(GoodReceivedHeader header)
        {
            string json = JsonConvert.SerializeObject(header);
            HttpResponseMessage responseMessage = client.PostAsync(baseUrl, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string goods = responseMessage.Content.ReadAsStringAsync().Result;
                var details = JsonConvert.DeserializeObject<GoodReceivedHeader>(goods);
                if (details != null) header = details;
            }
            else
            {
                string goods = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint" + goods);
            }
            return header;
        }

        public List<GoodReceivedHeader> GetAllGoodreceived()
        {
            List<GoodReceivedHeader> goods = new List<GoodReceivedHeader>();
            HttpResponseMessage responseMessage = client.GetAsync(baseUrl).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string received = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<List<GoodReceivedHeader>>(received);
                if (result != null) goods = result;
            }
            else
            {
                string received = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occurred at the EndPoint" + received);
            }
            return goods;
        }

        public GoodReceivedHeader GetGoodreceivedById(int id)
        {
            GoodReceivedHeader header = new GoodReceivedHeader();
            HttpResponseMessage responseMessage = client.GetAsync(baseUrl + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string goods = responseMessage.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<GoodReceivedHeader>(goods);
                if (result != null) header = result;
            }
            else
            {
                string goods = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occurred at the API EndPoint" + goods);
            }
            return header;
        }
    }
}
