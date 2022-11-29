using SkillProfi;

namespace SkillProfiRequestsToAPI.Projects
{
    public class ProjectsRequests : RequestController
    {
        public ProjectsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Projects") {}

        public List<Project> GetList() => Request.Get<List<Project>>(Url);

        public async Task<List<Project>> GetListAsync() => await Request.GetAsync<List<Project>>(Url);



        public Project GetById(string id) => Request.Get<Project>(Url + $"/{id}");

        public async Task<Project> GetByIdAsync(string id) => await Request.GetAsync<Project>(Url + $"/{id}");



        public string Add(Project project, string accessToken) =>
            Request.Add(project, Url, accessToken);

        public async Task<string> AddAsync(Project project, string accessToken) =>
            await Request.AddAsync(project, Url, accessToken);



        public string Edit(string id, Project project, string accessToken) =>
             Request.Edit(project, Url, id, accessToken);
        

        public async Task<string> EditAsync(string id, Project project, string accessToken) =>
            await Request.EditAsync(project, Url, id, accessToken);



        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);

    }
}
