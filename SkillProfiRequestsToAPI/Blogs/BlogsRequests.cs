using SkillProfi;

namespace SkillProfiRequestsToAPI.Blogs
{
    public class BlogsRequests : RequestController
    {
        public BlogsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Blogs") {}

        public List<Blog> GetList() => Request.Get<List<Blog>>(Url);

        public async Task<List<Blog>> GetListAsync() => await Request.GetAsync<List<Blog>>(Url);



        public Blog GetById(string id) => Request.Get<Blog>($"{Url}/{id}");

        public async Task<Blog> GetByIdAsync(string id) => await Request.GetAsync<Blog>($"{Url}/{id}");



        public string Add(Blog Blog, string accessToken) => Request.Add(Blog, Url, accessToken);

        public async Task<string> AddAsync(Blog Blog, string accessToken) => 
            await Request.AddAsync(Blog, Url, accessToken);


        public string Edit(string id, Blog Blog, string accessToken) => 
            Request.Edit(Blog, Url, id, accessToken);

        public async Task<string> EditAsync(string id, Blog Blog, string accessToken) => 
            await Request.EditAsync( Blog, Url, id, accessToken);



        public string DeleteById(string id, string accessToken) => 
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);
    }
}
