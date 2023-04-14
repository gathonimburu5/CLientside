using EmployeeClient.Models.Domain;
using EmployeeClient.Services.Interface;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace EmployeeClient.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private string url = "https://localhost:7243/api/employee";
        private HttpClient client = new HttpClient();

        public Employee CreateEmployee(Employee employee)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string json = JsonConvert.SerializeObject(employee);
            try
            {
                HttpResponseMessage response = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<Employee>(result);
                    if (data != null)
                        employee = data;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    throw new Exception("An Error Occured at the API, Error info.." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured at the API, Error info.." + ex.Message);
            }
            finally { }
            return employee;
        }

        public void DeleteEmployee(Guid id)
        {
            url = url + "/" + id;
            Employee employee = new Employee();
            try{
                HttpResponseMessage responseMessage = client.DeleteAsync(url).Result;
                if(! responseMessage.IsSuccessStatusCode){
                    string result = responseMessage.Content.ReadAsStringAsync().Result;
                    throw new Exception("An Error Occured at the Api End Point.." + result);
                }
            }catch(Exception ex){
                throw new Exception("An Error Occured at the End Point API.." + ex.Message);
            }
            return;
        }

        public List<Employee> GetAll()
        {
            List<Employee> employee = new List<Employee>();
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                HttpResponseMessage ressponse = client.GetAsync(url).Result;
                if (ressponse.IsSuccessStatusCode)
                {
                    string result = ressponse.Content.ReadAsStringAsync().Result;
                    var dataCol = JsonConvert.DeserializeObject<List<Employee>>(result);
                    if (dataCol != null)
                        employee = dataCol;
                }
                else
                {
                    string result = ressponse.Content.ReadAsStringAsync().Result;
                    throw new Exception("Error Occured at the API EndPoint, Error info.." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured at the API End Point, Error Info.." + ex.Message);
            }
            finally { }
            return employee;
        }

        public Employee GetEmployeeById(Guid id)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Employee employee = new Employee();
            string empUrl = url + "/" + id;
            try
            {
                HttpResponseMessage respose = client.GetAsync(empUrl).Result;
                if (respose.IsSuccessStatusCode)
                {
                    string result = respose.Content.ReadAsStringAsync().Result;
                    var items = JsonConvert.DeserializeObject<Employee>(result);
                    if (items != null)
                        employee = items;
                }
                else
                {
                    string result = respose.Content.ReadAsStringAsync().Result;
                    throw new Exception("An Error Occured.." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured.." + ex.Message);
            }
            finally { }
            return employee;

        }

        public Employee UpdateEmployee(Employee employee)
        {
            if (url.Trim().Substring(0, 5).ToLower() == "https")
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Guid id = employee.Id;
            url = url + "/" + id;
            string json = JsonConvert.SerializeObject(employee);
            try
            {
                HttpResponseMessage responce = client.PutAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
                if (!responce.IsSuccessStatusCode)
                {
                    string result = responce.Content.ReadAsStringAsync().Result;
                    throw new Exception("An Error Occured While Updating.." + result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An Error Occured " + ex.Message);
            }
            finally { }
            return employee;
        }
    }
}
