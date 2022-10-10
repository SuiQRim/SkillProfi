using Newtonsoft.Json;
using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Services
{
    public class ServicesRequests
    {
        public static List<Service> GetServices()
        {
            var url = "https://localhost:7120/api/Services";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string ServicesJson = reader.ReadToEnd();

            var Services = JsonConvert.DeserializeObject<List<Service>>(ServicesJson);

            return Services;
        }

        public static async Task<List<Service>> GetServicesAsync()
        {
            using var client = new HttpClient();
            string ServicesJson = await client.GetStringAsync("https://localhost:7120/api/Services");

            var Services = JsonConvert.DeserializeObject<List<Service>>(ServicesJson);

            return Services;
        }

        public static Service GetService(string id)
        {
            var url = $"https://localhost:7120/api/Services/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string ServiceJson = reader.ReadToEnd();

            var Service = JsonConvert.DeserializeObject<Service>(ServiceJson);

            return Service;
        }

        public static async Task<Service> GetServiceAsync(string id)
        {
            using var client = new HttpClient();
            string ServiceJson = await client.GetStringAsync($"https://localhost:7120/api/Services/{id}");

            var Service = JsonConvert.DeserializeObject<Service>(ServiceJson);

            return Service;
        }

        public static string AddService(Service Service)
        {
            var url = "https://localhost:7120/api/Services";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;

            var json = JsonConvert.SerializeObject(Service);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public static async Task<string> AddServiceAsync(Service Service)
        {

            var json = JsonConvert.SerializeObject(Service);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:7120/api/Services";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string EditService(string id, Service Service)
        {
            var url = $"https://localhost:7120/api/Services/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(Service);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;

        }

        public static async Task<string> EditServiceAsync(string id, Service Service)
        {

            var json = JsonConvert.SerializeObject(Service);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:7120/api/Services/{id}";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }
        public static string DeleteService(string id)
        {
            var url = $"https://localhost:7120/api/Services/{id}";

            var request = WebRequest.Create(url);
            request.Method = "DELETE";

            using var reqStream = request.GetRequestStream();

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public static async Task<string> DeleteServiceAsync(string id)
        {
            var url = $"https://localhost:7120/api/Services/{id}";
            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
