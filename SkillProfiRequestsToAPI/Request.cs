using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace SkillProfiRequestsToAPI
{
    public static class Request
    {
        public static T Get<T>(string url, string accessToken = null)
        {
            var request = WebRequest.Create(url);

            request.Method = WebRequestMethods.Http.Get;
            if (accessToken != null)
                request.Headers.Add("Authorization", "Bearer " + accessToken);

            using var webResponse = request.GetResponse();

            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);

            string json = reader.ReadToEnd();

            T obj = JsonConvert.DeserializeObject<T>(json);

            return obj;
        }

        public async static Task<T> GetAsync<T>(string url, string accessToken = null)
        {
            using var client = new HttpClient();
            if (accessToken != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            string ServicesJson = await client.GetStringAsync(url);

            var obg = JsonConvert.DeserializeObject<T>(ServicesJson);

            return obg;
        }

        public static string Add<T>(T obj, string url, string? accessToken = null)
        {
            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;

            var json = JsonConvert.SerializeObject(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            if (accessToken != null)
                request.Headers.Add("Authorization", "Bearer " + accessToken);

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public async static Task<string> AddAsync<T>(T obj, string url, string? accessToken = null)
        {
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            if (accessToken != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }


        public static string Edit<T>(T obg, string url, string? id = null, string? accessToken = null)
        {
            string uri = (id == null ? url : $"{url}/{id}");

            var request = WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(obg);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);

            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            if (accessToken != null)
                request.Headers.Add("Authorization", "Bearer " + accessToken);

            using var reqStream = request.GetRequestStream();
            reqStream.Write(byteArray, 0, byteArray.Length);

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;

        }

        public async static Task<string> EditAsync<T>(T obj, string url, string? id = null, string? accessToken = null)
        {
            string uri = (id == null ? url : $"{url}/{id}");

            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            if (accessToken != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.PutAsync(uri, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }


        public static string Delete(string id, string url, string? accessToken = null)
        {
            var uri = $"{url}/{id}";

            var request = WebRequest.Create(uri);
            request.Method = "DELETE";
            if (accessToken != null)
                request.Headers.Add("Authorization", "Bearer " + accessToken);

            using var reqStream = request.GetRequestStream();

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public async static Task<string> DeleteAsync(string id, string url, string? accessToken = null)
        {
            var uri = $"{url}/{id}";

            using var client = new HttpClient();
            if (accessToken != null)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            var response = await client.DeleteAsync(uri);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
