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
        private const string _mainUrl = "https://localhost:7120/api/Projects";

        public static List<Project> GetProjects() => Request.Get<List<Project>>(_mainUrl);

        public static async Task<List<Project>> GetProjectsAsync() => await Request.GetAsync<List<Project>>(_mainUrl);



        public static Project GetProject(string id) => Request.Get<Project>(_mainUrl + $"/{id}");

        public static async Task<Project> GetProjectAsync(string id) => await Request.GetAsync<Project>(_mainUrl + $"/{id}");



        public static string AddProject(Project project, string accessToken) => 
            Request.Add(project, "https://localhost:7120/api/Projects", accessToken);

        public static async Task<string> AddProjectAsync(Project project, string accessToken) => 
            await Request.AddAsync(project, _mainUrl, accessToken);



        public static string EditProject(string id, Project project, string accessToken)
        {
            return Request.Edit(project, _mainUrl, id, accessToken);
        }

        public static async Task<string> EditProjectAsync(string id, Project project, string accessToken) =>
            await Request.EditAsync(project, _mainUrl, id, accessToken);



        public static string DeleteProject(string id, string accessToken) => 
            Request.Delete(id, _mainUrl, accessToken);

        public static async Task<string> DeleteProjectAsync(string id, string accessToken) => 
            await Request.DeleteAsync(id, _mainUrl, accessToken);

    }
}
