using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeClient.Services.Implementation
{
    public class MeasureService : IMeasureService
    {
        private readonly string url = "https://localhost:7243/api/Measure";
        private readonly HttpClient client = new HttpClient();
        public MeasureUnit CreateMeasure(MeasureUnit model)
        {
            string json = JsonConvert.SerializeObject(model);
            HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var detail = JsonConvert.DeserializeObject<MeasureUnit>(result);
                if (detail != null) model = detail;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the End Point." + result);
            }
            return model;
        }

        public bool DeleteMeasure(int id)
        {
            HttpResponseMessage responseMessage = client.DeleteAsync(url + "/" + id).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the API EndPoint" + result);
            }
            return true;
        }

        public List<MeasureUnit> GetAllMeasure()
        {
            List<MeasureUnit> measureUnits = new List<MeasureUnit>();
            HttpResponseMessage responseMessage = client.GetAsync(url).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<MeasureUnit>>(result);
                if (data != null) measureUnits = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the End Point." + result);
            }
            return measureUnits;
        }

        public MeasureUnit GetMeasureById(int id)
        {
            MeasureUnit measureUnit = new MeasureUnit();
            HttpResponseMessage responseMessage = client.GetAsync(url + "/" + id).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<MeasureUnit>(result);
                if (data != null) measureUnit = data;
            }
            else
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error at the End Point." + result);
            }
            return measureUnit;
        }

        public MeasureUnit UpdateMeasure(MeasureUnit model)
        {
            string json = JsonConvert.SerializeObject(model);
            HttpResponseMessage responseMessage = client.PutAsync(url + "/" + model.MeasureUnitId, new StringContent(json, Encoding.UTF8, "application/json")).Result;
            if (!responseMessage.IsSuccessStatusCode)
            {
                string result = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error Occured at the EndPoint." + result);
            }
            return model;
        }
    }
}
