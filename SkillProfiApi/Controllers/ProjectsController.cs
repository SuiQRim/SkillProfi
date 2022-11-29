using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfiApi.Data;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly SkillProfiDbContext _context;

        public ProjectsController(SkillProfiDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            List<Project> projects = await _context.Projects.ToListAsync();

            return projects;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(Guid id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            Project? project = await _context.Projects.FindAsync(id);
            ;
            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProject(Guid id, [FromBody] ObjectWithImage<Project> project)
        {
            if (id != project.Object.Id)
            {
                return BadRequest();
            }

            _context.Entry(project.Object).State = EntityState.Modified;
			await PictureDirectory.SavePictureAsync(project);

			try
            {
                await _context.SaveChangesAsync();

			}
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Project>> PostProject(ObjectWithImage<Project> project)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'SkillProfiDbContext.Projects'  is null.");
            }
            _context.Projects.Add(project.Object);
            await PictureDirectory.SavePictureAsync(project);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetProject", new { id = project.Object.Id }, project.Object);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            if (_context.Projects == null)
            {
                return NotFound();
            }
            Project? project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            project.RemovePicture();
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProjectExists(Guid id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
