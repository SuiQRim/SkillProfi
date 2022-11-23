using Microsoft.AspNetCore.Mvc;
using SkillProfiRequestsToAPI.Services;

namespace SkillProfiWEBMVC.Controllers
{
	public class ServicesController : Controller
	{
		public async Task<IActionResult> Services()
		{
			return View(await ServicesRequests.GetServicesAsync());
		}
	}
}
