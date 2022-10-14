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
    public static class ContactsRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Contacts";

        public static SkillProfi.Contacts GetContacts() => Request.Get<SkillProfi.Contacts>("https://localhost:7120/api/Contacts");
        

        public static string EditContacts(SkillProfi.Contacts contact)
        {
            var url = _mainUrl;

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(contact);
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

        public static async Task<string> EditContactsAsync(SkillProfi.Contacts contact)
        {

            var json = JsonConvert.SerializeObject(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = _mainUrl;
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

    }
}
