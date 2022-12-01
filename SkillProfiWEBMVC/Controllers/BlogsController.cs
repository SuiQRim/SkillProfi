using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWEBMVC.Models;
using System.Transactions;
using SkillProfiRequestsToAPI.Pictures;
using System.Reflection;

namespace SkillProfiWEBMVC.Controllers
{
    public class BlogsController : Controller
    {
        private readonly SkillProfiWebClient _spClient = new();

        public async Task<IActionResult> BlogsAsync()
        {
            List<Blog> reqblogs = new(await _spClient.Blogs.GetListAsync());
            List<GeneralModel<Blog>> blogs = 
                reqblogs.Select(b => new GeneralModel<Blog>()
                { 
                    Object = b,
                    ImageLink = _spClient.Pictures.GetURL(b.PictureName)
                })
                .ToList();

            return View(blogs);
        }

        public async Task<IActionResult> BlogAsync(string id)
        {
            Blog reqblog = await _spClient.Blogs.GetByIdAsync(id.ToString());
            GeneralModel<Blog> blog = new()
            {
                Object = reqblog,
                ImageLink = _spClient.Pictures.GetURL(reqblog.PictureName)
            };

            return View(blog);
        }
    }
}
