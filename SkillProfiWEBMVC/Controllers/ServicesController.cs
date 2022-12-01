using Microsoft.AspNetCore.Mvc;
using SkillProfiRequestsToAPI;
using SkillProfiRequestsToAPI.Services;

namespace SkillProfiWEBMVC.Controllers
{
	public class ServicesController : Controller
	{
        private readonly SkillProfiWebClient _spClient = new();

        public async Task<IActionResult> Services()
		{
			return View(await _spClient.Services.GetListAsync());
		}
	}
}
