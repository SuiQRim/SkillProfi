using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWEBMVC.Models;
using SkillProfiWPF;

namespace SkillProfiWEBMVC.Controllers
{
	public class ContactsController : Controller
	{
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public IActionResult Contacts()
		{
			Contacts contacts = _spClient.Contacts.Get();

			ContactsModel cmodel = new()
			{
				Adress = contacts.Adress,
				Email = contacts.Email,
				LinkToMapContructor = contacts.LinkToMapContructor,
				PhoneNumber = contacts.PhoneNumber,

				SocialNetworks = contacts.SocialNetworks.Select(sc => new GeneralModel<SocialNetwork>()
				{
					Object = sc,
					ImageLink = _spClient.Pictures.GetURL(sc.PictureName)
				})
				.ToList()
			};
			
			return View(cmodel);
		}
	}
}
