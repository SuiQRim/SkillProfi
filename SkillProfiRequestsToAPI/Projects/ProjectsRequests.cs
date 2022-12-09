using SkillProfi.Project;
using System.IO;
using System.Text;

namespace SkillProfiRequestsToAPI.Projects
{
    public class ProjectsRequests : RequestController
    {
        public ProjectsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Projects") {}

        public List<Project> GetList() => Request.Get<List<Project>>(Url);

        public async Task<List<Project>> GetListAsync() => await Request.GetAsync<List<Project>>(Url);


        public Project GetById(string id) => Request.Get<Project>(Url + $"/{id}");
        public async Task<Project> GetByIdAsync(string id) => await Request.GetAsync<Project>(Url + $"/{id}");


        public string Add(ProjectTransfer project, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(project, stream);
			return Request.Add(obj, Url, accessToken);	
		}        

        public async Task<string> AddAsync(ProjectTransfer project, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(project, stream);
			return await Request.AddAsync(obj, Url, accessToken);
		}

        public string Edit(string id, ProjectTransfer project, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(project, stream);
			return Request.Edit(obj, Url, id, accessToken);
		}
             
        public async Task<string> EditAsync(string id, ProjectTransfer project, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(project, stream);
			return await Request.EditAsync(obj, Url, id, accessToken);
		}


        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);

    }
}
