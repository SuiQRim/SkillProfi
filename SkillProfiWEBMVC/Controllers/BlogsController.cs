using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI.Blogs;

namespace SkillProfiWEBMVC.Controllers
{
    public class BlogsController : Controller
    {
        public async Task<IActionResult> BlogsAsync()
        {
            List<Blog> blogs = new(await BlogsRequests.GetBlogsAsync());

            return View(blogs);
        }
        public async Task<IActionResult> BlogAsync(string id)
        {

            var blog = await BlogsRequests.GetBlogAsync(id.ToString());

            return View(blog);
        }
    }
}
