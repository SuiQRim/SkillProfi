using SkillProfi.Blog;

namespace SkillProfiRequestsToAPI.Blogs
{
    public class BlogsRequests : RequestController
    {
        public BlogsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Blogs") {}

        public List<Blog> GetList() => Request.Get<List<Blog>>(Url);

        public async Task<List<Blog>> GetListAsync() => await Request.GetAsync<List<Blog>>(Url);



        public Blog GetById(string id) => Request.Get<Blog>($"{Url}/{id}");

        public async Task<Blog> GetByIdAsync(string id) => await Request.GetAsync<Blog>($"{Url}/{id}");



        public string Add(BlogTransfer blogTransfer, Stream stream, string accessToken)
        {
			var obj = BuildObjectWithImage(blogTransfer, stream);
			return Request.Add(obj, Url, accessToken);
        }

        public async Task<string> AddAsync(BlogTransfer blogTransfer, Stream stream, string accessToken)
        {
			var obj = BuildObjectWithImage(blogTransfer, stream);
			return await Request.AddAsync(obj, Url, accessToken);
        }



        public string Edit(string id, BlogTransfer blogTransfer, Stream? stream, string accessToken)
		{
			var obj = BuildObjectWithImage(blogTransfer, stream);
            return Request.Edit(obj, Url, id, accessToken);
        }
           

        public async Task<string> EditAsync(string id, BlogTransfer blogTransfer, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(blogTransfer, stream);
			return await Request.EditAsync(obj, Url, id, accessToken);
        }


        public string DeleteById(string id, string accessToken) => 
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);
    }
}
