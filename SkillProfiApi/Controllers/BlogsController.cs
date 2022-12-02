using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfiApi.Data;
using SkillProfiApi.Data.Picture;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly SkillProfiDbContext _context;

		private readonly ILogger<ProjectsController> _logger;
		public BlogsController(SkillProfiDbContext context, ILogger<ProjectsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }

            List<Blog> blogs = await _context.Blogs.ToListAsync();

            return blogs;
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(Guid id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }
            return blog;
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBlog(Guid id, ObjectWithPicture<Blog> blog)
        {
            if (id != blog.Object.Id)
            {
                return BadRequest();
            }

            _context.Entry(blog.Object).State = EntityState.Modified;

			try { await PictureDirectory.SavePictureAsync(blog); }
			catch (PictureNullException e)
			{
				_logger.LogWarning(exception: e, $"{nameof(blog)} saved without image");
			}

			try { await _context.SaveChangesAsync(); }
			catch (DbUpdateConcurrencyException)
			{
				if (!BlogExists(id))
					return NotFound();

				else
					throw;
			}

			return NoContent();
        }

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Blog>> PostBlog(ObjectWithPicture<Blog> blog)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'SkillProfiDbContext.Blogs'  is null.");
            }
            _context.Blogs.Add(blog.Object);

			try { await PictureDirectory.SavePictureAsync(blog); }
			catch (PictureNullException e)
			{
				_logger.LogWarning(exception: e, $"{nameof(blog)} saved without image");
				return BadRequest(e.Message);
			}

			await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Object.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);

            try { blog.RemovePicture(); }
            catch (PictureNotFound e)
            {
                _logger.LogWarning(exception: e, "The image cannot be deleted because it is not found");
            }


            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(Guid id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
