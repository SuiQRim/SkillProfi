﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi.Service;
using SkillProfiRequestsToAPI;
using SkillProfiRequestsToAPI.Services;
using SkillProfiWEBMVC.Models;

namespace SkillProfiWEBMVC.Controllers
{
	public class ServicesController : Controller
	{
		private readonly SkillProfiWebClient _spClient = new();

		public async Task<IActionResult> Services()
		{
			return View(await _spClient.Services.GetListAsync());
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditAsync(string? id)
		{
			if (id == null)
                return View(new ModelCustom<ServiceTransfer>() { Blog = new ()});
            

			Service service = await _spClient.Services.GetByIdAsync(id);

			ModelCustom<ServiceTransfer> mc = new()
			{
				Id = service.Id.ToString(),
				Blog = new()
				{
					Description = service.Description,
					Title = service.Title,
				}
			};
			return View(mc);
		}


        [Authorize]
		[HttpPost]
		public async Task<IActionResult> EditAsync(ModelCustom<ServiceTransfer> model, string? id)
		{
			if (!ModelState.IsValid)
				return View(model);

			ServiceTransfer st = model.Blog;
			if (id != null)
			{
				await _spClient.Services.EditAsync(id, st);

			}
			else
			{
                await _spClient.Services.AddAsync(st);
            }
			
			return Redirect("~/Services/Services");
		}
		
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> DeleteAsync(string id)
		{
            await _spClient.Services.DeleteByIdAsync(id);
			return Redirect("~/Services/Services");

        }

	}
}
