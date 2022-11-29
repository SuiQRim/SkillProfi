using Microsoft.AspNetCore.Mvc;
using SkillProfiRequestsToAPI;
using SkillProfiRequestsToAPI.Services;
using SkillProfiWPF;

namespace SkillProfiWEBMVC.Controllers
{
	public class ServicesController : Controller
	{
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public async Task<IActionResult> Services()
		{
			return View(await _spClient.Services.GetListAsync());
		}
	}
}
