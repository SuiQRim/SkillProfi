using Newtonsoft.Json;
using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Blogs
{
    public class BlogsRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Blogs";

        public static List<Blog> GetBlogs() => Request.Get<List<Blog>>(_mainUrl);

        public static async Task<List<Blog>> GetBlogsAsync() => await Request.GetAsync<List<Blog>>(_mainUrl);



        public static Blog GetBlog(string id) => Request.Get<Blog>($"{_mainUrl}/{id}");

        public static async Task<Blog> GetBlogAsync(string id) => await Request.GetAsync<Blog>($"{_mainUrl}/{id}");



        public static string AddBlog(Blog Blog) => Request.Add(Blog, _mainUrl);

        public static async Task<string> AddBlogAsync(Blog Blog) => await Request.AddAsync(Blog, _mainUrl);



        public static string EditBlog(string id, Blog Blog) => Request.Edit(id, Blog, _mainUrl);

        public static async Task<string> EditBlogAsync(string id, Blog Blog) => await Request.EditAsync(id, Blog, _mainUrl);



        public static string DeleteBlog(string id) => Request.Delete(id, _mainUrl);

        public static async Task<string> DeleteBlogAsync(string id) => await Request.DeleteAsync(id, _mainUrl);
    }
}
