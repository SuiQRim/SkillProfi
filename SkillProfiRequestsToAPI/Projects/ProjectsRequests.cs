using Newtonsoft.Json;
using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Projects
{
    public class ProjectsRequests
    {
        public static List<Project> GetProjects()
        {
            var url = "https://localhost:7120/api/Projects";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string ProjectsJson = reader.ReadToEnd();

            var Projects = JsonConvert.DeserializeObject<List<Project>>(ProjectsJson);

            return Projects;
        }

        public static async Task<List<Project>> GetProjectsAsync()
        {
            using var client = new HttpClient();
            string ProjectsJson = await client.GetStringAsync("https://localhost:7120/api/Projects");

            var Projects = JsonConvert.DeserializeObject<List<Project>>(ProjectsJson);

            return Projects;
        }

        public static Project GetProject(string id)
        {
            var url = $"https://localhost:7120/api/Projects/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string ProjectJson = reader.ReadToEnd();

            var Project = JsonConvert.DeserializeObject<Project>(ProjectJson);

            return Project;
        }

        public static async Task<Project> GetProjectAsync(string id)
        {
            using var client = new HttpClient();
            string ProjectJson = await client.GetStringAsync($"https://localhost:7120/api/Projects/{id}");

            var Project = JsonConvert.DeserializeObject<Project>(ProjectJson);

            return Project;
        }

        public static string AddProject(Project Project)
        {
            var url = "https://localhost:7120/api/Projects";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;

            var json = JsonConvert.SerializeObject(Project);
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

        public static async Task<string> AddProjectAsync(Project Project)
        {

            var json = JsonConvert.SerializeObject(Project);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:7120/api/Projects";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string EditProject(string id, Project Project)
        {
            var url = $"https://localhost:7120/api/Projects/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(Project);
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

        public static async Task<string> EditProjectAsync(string id, Project Project)
        {

            var json = JsonConvert.SerializeObject(Project);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:7120/api/Projects/{id}";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static async Task<string> DeleteProjectAsync(string id)
        {
            var url = $"https://localhost:7120/api/Projects/{id}";
            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
