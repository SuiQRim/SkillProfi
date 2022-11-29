using SkillProfi;
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



        public string Add(Project project, Stream stream, string accessToken)
        {
			using (stream)
			{
				byte[] buffer = new byte[stream.Length];
				stream.Read(buffer, 0, buffer.Length);
				var obj = new ObjectWithImage<Project>()
				{
					Object = project,
					Picture = buffer
				};
				return Request.Add(obj, Url, accessToken);
			}
		}
            

        public async Task<string> AddAsync(Project project, Stream stream, string accessToken)
        {
			using (stream)
			{
				byte[] buffer = new byte[stream.Length];
				await stream.ReadAsync(buffer, 0, buffer.Length);
				var obj = new ObjectWithImage<Project>()
				{
					Object = project,
					Picture = buffer
				};
				return await Request.AddAsync(obj, Url, accessToken);
			}
		}
           



        public string Edit(string id, Project project, Stream stream, string accessToken)
        {
			using (stream)
			{
				byte[] buffer = new byte[stream.Length];
				stream.Read(buffer, 0, buffer.Length);
				var obj = new ObjectWithImage<Project>()
				{
					Object = project,
					Picture = buffer
				};
				return Request.Edit(obj, Url, id, accessToken);
			}
		}
             
        

        public async Task<string> EditAsync(string id, Project project, Stream stream, string accessToken)
        {
			using (stream)
			{
				byte[] buffer = new byte[stream.Length];
				await stream.ReadAsync(buffer, 0, buffer.Length);
				var obj = new ObjectWithImage<Project>()
				{
					Object = project,
					Picture = buffer
				};
				return await Request.EditAsync(obj, Url, id, accessToken);
			}
		}


        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);

    }
}
