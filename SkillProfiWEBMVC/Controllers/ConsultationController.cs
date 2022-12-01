using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWEBMVC.Models;

namespace SkillProfiWEBMVC.Controllers
{
	public class ConsultationController : Controller
	{
		private readonly SkillProfiWebClient _spClient = new();

		public IActionResult Write()
		{
			return View(new Consult());
		}

		[HttpPost]
		public async Task<ActionResult> Write(Consult consult)
		{
			if (ModelState.IsValid)
			{ //checking model state

				//update student to db
				await _spClient.Consultations.AddAsync(new() 
				{ 
					EMail = consult.EMail, 
					Name = consult.Name, 
					Description = consult.Description
				});

				return RedirectPermanent("~/");
			}

			return View(consult);
		}
	}
}
