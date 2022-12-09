using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfi.Project;
using SkillProfiApi.Data;
using SkillProfiApi.Data.Picture;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly SkillProfiDbContext _context;

        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(SkillProfiDbContext context, ILogger<ProjectsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            if (_context.Projects == null)
                return Problem("Entity set 'SkillProfiDbContext.Projects'  is null.");

            List<Project> projects = await _context.Projects.ToListAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            if (_context.Projects == null)
                return Problem("Entity set 'SkillProfiDbContext.Projects'  is null.");

            Project? project = await _context.Projects.FindAsync(id);
            
            if (project == null)         
                return NotFound();
            
            return Ok(project);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProject(Guid id, [FromBody] ObjectWithPicture<ProjectTransfer> project)
        {
            if (_context.Projects == null)
                return Problem("Entity set 'SkillProfiDbContext.Projects'  is null.");

            ProjectTransfer projBase = project.Object;
            Project? proj = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);

            if (proj == null)
                return BadRequest();

            Project updateProj = new()
            {
                Id = proj.Id,
                Title = projBase.Title,
                Description = projBase.Description,
                PictureName = proj.PictureName,
                Created = proj.Created,
            };

			_context.Entry(proj).CurrentValues.SetValues(updateProj);

            try { await PictureDirectory.SavePictureAsync(proj, project.Picture); }
            catch (PictureNullException e)
            {
                _logger.LogWarning(exception: e, $"{nameof(project)}'s picture is not Excist and not pass from request");
                return BadRequest(e.Message);
            }

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {                
                if (!ProjectExists(id))
                    return NotFound();

                else
                    throw;
            }
          
            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProject(ObjectWithPicture<ProjectTransfer> project)
        {
            if (_context.Projects == null)
                return Problem("Entity set 'SkillProfiDbContext.Projects'  is null.");

            ProjectTransfer projBase = project.Object;

			Project proj = new()
            {
                Id = Guid.NewGuid(),
                Title = projBase.Title,
                Description = projBase.Description,
                Created = DateTime.UtcNow,
            };

            _context.Projects.Add(proj);

			try { await PictureDirectory.SavePictureAsync(proj, project.Picture); }
			catch (PictureNullException e)
			{
				_logger.LogWarning(exception: e, $"{nameof(proj)} saved without image");
                return BadRequest(e.Message);
			}

			await _context.SaveChangesAsync(); 

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            if (_context.Projects == null)
                return Problem("Entity set 'SkillProfiDbContext.Projects'  is null.");

            Project? project = await _context.Projects.FindAsync(id);

            if (project == null)
                return NotFound();
         
            _context.Projects.Remove(project);

			try { project.RemovePicture(); }
			catch (PictureNotFound e)
			{
				_logger.LogWarning(exception: e, "The image cannot be deleted because it's not found");
                return BadRequest(e.Message);
            }

			await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(Guid id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
