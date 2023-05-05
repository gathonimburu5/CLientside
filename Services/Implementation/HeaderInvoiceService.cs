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
	public class HeaderInvoiceService : IHeaderInvoiceService
	{
		private readonly string url = "https://localhost:7243/api/HeaderInvoice";
		private readonly HttpClient client = new HttpClient();
		public PInvoiceHeader CreateHeader(PInvoiceHeader model)
		{
			model.PInvoiceDetails.RemoveAll(x => x.IsDeleted == true);
			string json = JsonConvert.SerializeObject(model);
			HttpResponseMessage responseMessage = client.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json")).Result;
			if(responseMessage.IsSuccessStatusCode)
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				var data = JsonConvert.DeserializeObject<PInvoiceHeader>(result);
				if(data != null) model = data;
			}else
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				throw new Exception("Error Occurred at the API EndPoints, Please Check!!!" + result);
			}
			return model;
		}

		public bool DeleteHeader(int id)
		{
			HttpResponseMessage responseMessage = client.DeleteAsync(url + "/" + id).Result;
			if(!responseMessage.IsSuccessStatusCode)
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				throw new Exception("Error Occurred at the API EndPoints, Please Check!!" + result);
			}
			return true;
		}

		public string GenerateInvoiceCode()
		{
			string code = "";
			var headerCode = GetAllHeaderList().Max(x=>x.InvoiceCode);
			if(headerCode == null)
			{
				code = "INV/0001/"+ DateTime.Now.Year;
			}
			else
			{
				int lastNumber = 1;
				int.TryParse(headerCode.Substring(4, 4), out lastNumber);
				code = $"INV/{(lastNumber + 1).ToString().PadLeft(4, '0')}/{DateTime.Now.Year}";
			}
			return code;
		}

		public List<PInvoiceHeader> GetAllHeaderList()
		{
			List<PInvoiceHeader> headers = new List<PInvoiceHeader>();
			HttpResponseMessage responseMessage = client.GetAsync(url).Result;
			if(responseMessage.IsSuccessStatusCode)
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				var data = JsonConvert.DeserializeObject<List<PInvoiceHeader>>(result);
				if(data != null) headers = data;
			}else
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				throw new Exception("Error at the API EndPoint, Please check your API!!" + result);
			}
			return headers;
		}

		public PInvoiceHeader GetHeaderById(int id)
		{
			PInvoiceHeader headers = new PInvoiceHeader();
			HttpResponseMessage responseMessage = client.GetAsync(url + "/" + id).Result;
			if(responseMessage.IsSuccessStatusCode)
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				var data = JsonConvert.DeserializeObject<PInvoiceHeader>(result);
				if(data != null) headers = data;
			}else
			{
				string result = responseMessage.Content.ReadAsStringAsync().Result;
				throw new Exception("Error at the API EndPoint, Check your API!!" + result);
			}
			return headers;
		}

		public PInvoiceHeader UpdateHeader(PInvoiceHeader model)
		{
			model.PInvoiceDetails.RemoveAll(x => x.IsDeleted == true);
			string json = JsonConvert.SerializeObject(model);
			HttpResponseMessage responseMessage = client.PutAsync(url + "/" + model.PInvoiceHeaderId, new StringContent(json, Encoding.UTF8, "application/json")).Result;
			if(! responseMessage.IsSuccessStatusCode)
			{
				string response = responseMessage.Content.ReadAsStringAsync().Result;
				throw new Exception("Error Occurred at the API EndPoint, Please Check your API!!" + response);
			}
			return model;
		}
	}
}