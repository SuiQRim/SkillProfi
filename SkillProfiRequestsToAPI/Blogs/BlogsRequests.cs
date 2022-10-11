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
        public static List<Blog> GetBlogs()
        {
            var url = "https://localhost:7120/api/Blogs";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string BlogsJson = reader.ReadToEnd();

            var Blogs = JsonConvert.DeserializeObject<List<Blog>>(BlogsJson);

            return Blogs;
        }

        public static async Task<List<Blog>> GetBlogsAsync()
        {
            using var client = new HttpClient();
            string BlogsJson = await client.GetStringAsync("https://localhost:7120/api/Blogs");

            var Blogs = JsonConvert.DeserializeObject<List<Blog>>(BlogsJson);

            return Blogs;
        }

        public static Blog GetBlog(string id)
        {
            var url = $"https://localhost:7120/api/Blogs/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;

            using var webResponse = request.GetResponse();
            using var webStream = webResponse.GetResponseStream();

            using var reader = new StreamReader(webStream);
            string BlogJson = reader.ReadToEnd();

            var Blog = JsonConvert.DeserializeObject<Blog>(BlogJson);

            return Blog;
        }

        public static async Task<Blog> GetBlogAsync(string id)
        {
            using var client = new HttpClient();
            string BlogJson = await client.GetStringAsync($"https://localhost:7120/api/Blogs/{id}");

            var Blog = JsonConvert.DeserializeObject<Blog>(BlogJson);

            return Blog;
        }

        public static string AddBlog(Blog Blog)
        {
            var url = "https://localhost:7120/api/Blogs";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;

            var json = JsonConvert.SerializeObject(Blog);
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

        public static async Task<string> AddBlogAsync(Blog Blog)
        {

            var json = JsonConvert.SerializeObject(Blog);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "https://localhost:7120/api/Blogs";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string EditBlog(string id, Blog Blog)
        {
            var url = $"https://localhost:7120/api/Blogs/{id}";

            var request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Put;

            var json = JsonConvert.SerializeObject(Blog);
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

        public static async Task<string> EditBlogAsync(string id, Blog Blog)
        {

            var json = JsonConvert.SerializeObject(Blog);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"https://localhost:7120/api/Blogs/{id}";
            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;

        }

        public static string DeleteBlog(string id)
        {
            var url = $"https://localhost:7120/api/Blogs/{id}";

            var request = WebRequest.Create(url);
            request.Method = "DELETE";

            using var reqStream = request.GetRequestStream();

            using var response = request.GetResponse();

            using var respStream = response.GetResponseStream();

            using var reader = new StreamReader(respStream);
            string data = reader.ReadToEnd();

            return data;
        }

        public static async Task<string> DeleteBlogAsync(string id)
        {
            var url = $"https://localhost:7120/api/Blogs/{id}";
            using var client = new HttpClient();

            var response = await client.DeleteAsync(url);

            string result = response.Content.ReadAsStringAsync().Result;

            return result;
        }
    }
}
