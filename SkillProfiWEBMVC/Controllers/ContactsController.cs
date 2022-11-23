using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiRequestsToAPI.Contacts;

namespace SkillProfiWEBMVC.Controllers
{
	public class ContactsController : Controller
	{
		public IActionResult Contacts()
		{
			Contacts contacts = ContactsRequests.GetContacts();
			
			return View(contacts);
		}
	}
}
