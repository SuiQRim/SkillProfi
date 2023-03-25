using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Blog;
using SkillProfi.Contacts;
using SkillProfiRequestsToAPI;
using SkillProfiWEBMVC.Models;

namespace SkillProfiWEBMVC.Controllers
{
	public class ContactsController : Controller
	{
		private readonly SkillProfiWebClient _spClient = new();

		[HttpGet]
		public async Task<IActionResult> ContactsAsync()
		{
			Contacts contacts = await _spClient.Contacts.GetAsync();


			ContactsModel cmodel = new()
			{
				Adress = contacts.Adress,
				Email = contacts.Email,
				LinkToMapContructor = contacts.LinkToMapContructor,
				PhoneNumber = contacts.PhoneNumber,

				SocialNetworks = contacts.SocialNetworks.Select(sc => new ModelCustom<SocialNetwork>()
				{
					Target = sc,
					ImageLink = _spClient.Pictures.GetURL(sc.PictureName)
				})
				.ToList()
			};

			return View(cmodel);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> EditInformationAsync()
		{
			Contacts con = await _spClient.Contacts.GetAsync();

			ContactsTransfer ct = new()
			{
				Adress = con.Adress,
				Email = con.Email,
				PhoneNumber = con.PhoneNumber,
				LinkToMapContructor = con.LinkToMapContructor
			};

			return View(ct);
		}


		[HttpPost]
		[Authorize]
		public async Task<IActionResult> EditInformationAsync(ContactsTransfer model)
		{
			if (!ModelState.IsValid)
				return View(model);

			await _spClient.Contacts.EditAsync(model);

			return RedirectToAction("Contacts");
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> SocialNetworksAsync()
		{
			List<SocialNetwork> socialNetworks = await _spClient.SocialNetworks.GetListAsync();

			List<ModelCustom<SocialNetwork>> blogs =
				socialNetworks.Select(b => new ModelCustom<SocialNetwork>()
				{
					Id = b.Id.ToString(),
					Target = b,
					ImageLink = _spClient.Pictures.GetURL(b.PictureName)
				})
				.ToList();

			return View(blogs);

		}


		[HttpGet]
		[Authorize]
		public async Task<IActionResult> EditSocialNetworkAsync(string? id)
		{
			if (id == null)
				return View(new ModelCustom<SocialNetworkTransfer>() { Target = new() });

			List<SocialNetwork> socList = await _spClient.SocialNetworks.GetListAsync();
			SocialNetwork? soc = socList.FirstOrDefault(s => s.Id.ToString() == id);

			if (soc == null)
				return NotFound($"Такая социальная сеть не существует. Id = {id}");

			ModelCustom<SocialNetworkTransfer> customSocialNetwork = new()
			{
				Id = id,
				Target = new()
				{
					Link = soc.Link,
				},
				ImageLink = _spClient.Pictures.GetURL(soc.PictureName)
			};

			return View(customSocialNetwork);
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> EditSocialNetworkAsync(ModelCustom<SocialNetworkTransfer> model, string? id, IFormFile? imageFile)
		 {
			if (!ModelState.IsValid)
				return View(model);

			if (id == null)
			{
				if (imageFile == null)
				{
					ModelState.AddModelError("ImageStatus", "Image is Required");
					return View(model);
				}
				await _spClient.SocialNetworks.AddAsync(model.Target, imageFile.OpenReadStream());
			}
			else
			{
				Stream? stream = null;
				if (imageFile != null)
					stream = imageFile.OpenReadStream();

				await _spClient.SocialNetworks.EditAsync(id, model.Target, stream);
			}

			return RedirectToAction("SocialNetworks");
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> DeleteSocialNetworkAsync(string id)
		{
			await _spClient.SocialNetworks.DeleteByIdAsync(id);
			return RedirectToAction("SocialNetworks");
		}
	}
}
