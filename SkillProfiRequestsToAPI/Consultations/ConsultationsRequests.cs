using System.Net;
using System.Text;
using SkillProfi;
using Newtonsoft.Json;

namespace SkillProfiRequestsToAPI.Consultations
{
    public static class ConsultationsRequests
    {
        public static List<Consultation> GetConsultations()
        {
            var url = "https://localhost:7120/api/Consultations";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string consultationsJson = reader.ReadToEnd();

            var consultations = JsonConvert.DeserializeObject<List<Consultation>>(consultationsJson);

            return consultations;
        }

        public static async Task<List<Consultation>> GetConsultationsAsync() 
        {
            using var client = new HttpClient();
            string consultationsJson = await client.GetStringAsync("https://localhost:7120/api/Consultations");

            var consultations = JsonConvert.DeserializeObject<List<Consultation>>(consultationsJson);

            return consultations;
        }

        public static Consultation GetConsultation(string id)
        {
            var url = $"https://localhost:7120/api/Consultations/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string consultationJson = reader.ReadToEnd();

            var consultation = JsonConvert.DeserializeObject<Consultation>(consultationJson);

            return consultation;
        }

        public static async Task<Consultation> GetConsultationAsync(string id)
        {
            using var client = new HttpClient();
            string consultationJson = await client.GetStringAsync($"https://localhost:7120/api/Consultations/{id}");

            var consultation = JsonConvert.DeserializeObject<Consultation>(consultationJson);

            return consultation;
        }

        public static string AddConsultation(Consultation consultation)
        {
            var url = "https://localhost:7120/api/Consultations";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;

            var json = JsonConvert.SerializeObject(consultation);
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

        public static async Task<string> AddConsultationAsync(Consultation consultation)
        {

            var json = JsonConvert.SerializeObject(consultation);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:7120/api/Consultations";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string EditConsultation(string id, Consultation consultation)
        {
            var url = $"https://localhost:7120/api/Consultations/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(consultation);
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

        public static async Task<string> EditConsultationAsync(string id, Consultation consultation)
        {

            var json = JsonConvert.SerializeObject(consultation);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:7120/api/Consultations/{id}";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string DeleteConsultation(string id)
        {
            var url = $"https://localhost:7120/api/Consultations/{id}";

            var request = WebRequest.Create(url);
            request.Method = "DELETE";

            using var reqStream = request.GetRequestStream();

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public static async Task<string> DeleteConsultationAsync(string id) 
        {
            var url = $"https://localhost:7120/api/Consultations/{id}";
            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }

    }
}
