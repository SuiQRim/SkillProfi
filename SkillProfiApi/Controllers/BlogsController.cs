using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfi.Blog;
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
                return Problem("Entity set 'SkillProfiDbContext.Blogs'  is null.");

            List<Blog> blogs = await _context.Blogs.ToListAsync();

            return Ok(blogs);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(Guid id)
        {
            if (_context.Blogs == null)
                return Problem("Entity set 'SkillProfiDbContext.Blogs'  is null.");

            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();
            
            return Ok(blog);
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBlog(Guid id, ObjectWithPicture<BlogTransfer> blogPic)
        {
            if (_context.Blogs == null)
                return Problem("Entity set 'SkillProfiDbContext.Blogs'  is null.");

            BlogTransfer blogTranfer = blogPic.Object;     
            Blog? blog = await _context.Blogs.SingleOrDefaultAsync(blog1=> blog1.Id == id);
            
            if (blog == null || blogTranfer == null)
                return BadRequest();

            Blog updateBlog = new()
            {
                Id = blog.Id,
                Description = blogTranfer.Description,
                Title = blogTranfer.Title,
                PictureName = blog.PictureName,
                Created = blog.Created
            };
            
            _context.Entry(blog).CurrentValues.SetValues(updateBlog);

			try { await PictureDirectory.SavePictureAsync(blog, blogPic.Picture); }
			catch (PictureNullException e)
			{
				_logger.LogWarning(exception: e, $"{nameof(blog)} saved without image");
                return BadRequest(e.Message);
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
        public async Task<IActionResult> PostBlog(ObjectWithPicture<BlogTransfer> blogPic)
        { 
            if (_context.Blogs == null)
                return Problem("Entity set 'SkillProfiDbContext.Blogs'  is null.");
            
            BlogTransfer blogBase = blogPic.Object;
            
            if (blogBase == null)
                return BadRequest();

            Blog blog = new()
            {
                Id = Guid.NewGuid(),
                Description = blogBase.Description,
                Title = blogBase.Title,
                Created = DateTime.UtcNow,
            };

            _context.Blogs.Add(blog);

			try { await PictureDirectory.SavePictureAsync(blog, blogPic.Picture); }
			catch (PictureNullException e)
			{
				_logger.LogWarning(exception: e, $"{nameof(blog)} saved without image");
				return BadRequest(e.Message);
			}

			await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            if (_context.Blogs == null)
                return Problem("Entity set 'SkillProfiDbContext.Blogs'  is null.");

            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
                return NotFound();
            
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
