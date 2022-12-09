using Microsoft.AspNetCore.Mvc;
using SkillProfi.Contacts;
using SkillProfiRequestsToAPI;
using SkillProfiWEBMVC.Models;

namespace SkillProfiWEBMVC.Controllers
{
    public class ContactsController : Controller
	{
        private readonly SkillProfiWebClient _spClient = new();

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
