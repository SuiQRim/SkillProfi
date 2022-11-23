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



        public static string AddProject(Project project) => Request.Add(project, "https://localhost:7120/api/Projects");

        public static async Task<string> AddProjectAsync(Project project) => await Request.AddAsync(project, _mainUrl);



        public static string EditProject(string id, Project project) => Request.Edit(id, project, _mainUrl);

        public static async Task<string> EditProjectAsync(string id, Project project) => await Request.EditAsync(id, project, _mainUrl);



        public static string DeleteProject(string id) => Request.Delete(id, _mainUrl);

        public static async Task<string> DeleteProjectAsync(string id) => await Request.DeleteAsync(id, _mainUrl);

    }
}
