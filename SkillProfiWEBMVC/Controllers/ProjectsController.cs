using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI.Projects;

namespace SkillProfiWEBMVC.Controllers
{
	public class ProjectsController : Controller
	{
		public async Task<IActionResult> ProjectsAsync()
		{
			List<Project> projects = new( await ProjectsRequests.GetProjectsAsync());
			return View(projects);
        }
        public async Task<IActionResult> ProjectAsync(string id)
        {
            Project project = await ProjectsRequests.GetProjectAsync(id);
            return View(project);
        }

    }
}
