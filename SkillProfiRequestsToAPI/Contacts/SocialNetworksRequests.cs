using Newtonsoft.Json;
using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Contacts
{
    public static class SocialNetworksRequests
    {
        public static List<SocialNetwork> GetSocialNetworks()
        {
            var url = "https://localhost:7120/api/Contacts/SocialNetworks";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string SocialNetworksJson = reader.ReadToEnd();

            var socialNetworks = JsonConvert.DeserializeObject<List<SocialNetwork>>(SocialNetworksJson);

            return socialNetworks;
        }

        public static string AddSocialNetwork(SocialNetwork SocialNetwork)
        {
            var url = "https://localhost:7120/api/Contacts/SocialNetworks";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;

            var json = JsonConvert.SerializeObject(SocialNetwork);
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

        public static async Task<string> AddSocialNetworkAsync(SocialNetwork SocialNetwork)
        {

            var json = JsonConvert.SerializeObject(SocialNetwork);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:7120/api/Contacts/SocialNetworks";

            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string EditSocialNetwork(string id, SocialNetwork SocialNetwork)
        {
            var url = $"https://localhost:7120/api/Contacts/SocialNetworks?id={id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(SocialNetwork);
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

        public static async Task<string> EditSocialNetworkAsync(string id, SocialNetwork SocialNetwork)
        {

            var json = JsonConvert.SerializeObject(SocialNetwork);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:7120/api/Contacts/SocialNetworks?id={id}";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string DeleteSocialNetwork(string id)
        {
            var url = $"https://localhost:7120/api/Contacts/SocialNetworks?id={id}";

            var request = WebRequest.Create(url);
            request.Method = "DELETE";

            using var reqStream = request.GetRequestStream();

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public static async Task<string> DeleteSocialNetworkAsync(string id)
        {
            var url = $"https://localhost:7120/api/Contacts/SocialNetworks?id={id}";
            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }
    }
}
