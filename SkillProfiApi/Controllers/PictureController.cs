using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillProfiApi.Data;
using SkillProfiApi.Models;

namespace SkillProfiApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PictureController : ControllerBase
	{
		const int Success = 0;
		const int ImageNotFound = 1;

		[HttpGet]
		public async Task<IActionResult> GetPictureAsync(Guid Id) {

			byte[]? picture = await PictureDirectory.GetPictureAsync(Id);

			if (picture == null) return NotFound(new RequestResponce(ImageNotFound, "The Image Not Found"));

			return File(picture, "image/png");
		}
	}
}
