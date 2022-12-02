using Microsoft.AspNetCore.Mvc;
using SkillProfiApi.Data.Picture;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PictureController : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetPictureAsync(Guid Id) {

			byte[]? picture = await PictureDirectory.GetPictureAsync(Id);

			if (picture == null) return NotFound();

			return File(picture, "image/png");
		}
	}
}
