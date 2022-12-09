using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfiApi.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkillProfiApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VisualComponentsController : ControllerBase
	{

		// GET api/<VisualComponentsController>
		[HttpGet("Face")]
		public async Task<ActionResult<Face>> GetFace()
		{
			return Ok(await FaceFile.GetAsync());
		}

		// DELETE api/<VisualComponentsController>
		[HttpPut("Face")]
		[Authorize]
		public IActionResult PutFace(Face face)
		{
			FaceFile.Save(face);
			return NoContent();
		}
	}
}
