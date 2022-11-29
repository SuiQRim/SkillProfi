using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWEBMVC.Models;
using SkillProfiWPF;

namespace SkillProfiWEBMVC.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public async Task<IActionResult> ProjectsAsync()
        {
            List<Project> reqProjects = new(await _spClient.Projects.GetListAsync());
            List<GeneralModel<Project>> projects =
                reqProjects.Select(b => new GeneralModel<Project>()
                {
                    Object = b,
                    ImageLink = _spClient.Pictures.GetURL(b.PictureName)
                })
                .ToList();
            return View(projects);
        }
        public async Task<IActionResult> ProjectAsync(string id)
        {
            Project reqProject = await _spClient.Projects.GetByIdAsync(id);
            GeneralModel<Project> project =
                new()
                {
                    Object = reqProject,
                    ImageLink = _spClient.Pictures.GetURL(reqProject.PictureName)
                };

            return View(project);
        }

    }
}
