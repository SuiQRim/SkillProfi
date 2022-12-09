using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi.Service;
using SkillProfiApi.Data;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly SkillProfiDbContext _context;

        public ServicesController(SkillProfiDbContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            if (_context.Services == null)
				return Problem("Entity set 'SkillProfiDbContext.Services'  is null.");

			return Ok(await _context.Services.ToListAsync());
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(Guid id)
        {

            if (_context.Services == null)
				return Problem("Entity set 'SkillProfiDbContext.Services'  is null.");

			var service = await _context.Services.FindAsync(id);

            if (service == null)
                return NotFound();

            return Ok(service);
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutService(Guid id, ServiceTransfer serviceTransfer)
        {
            if (_context.Services == null)
                return Problem("Entity set 'SkillProfiDbContext.Services'  is null.");

            Service? service = await _context.Services.SingleOrDefaultAsync(s => s.Id == id);
            if (service == null)
                return NotFound();

            Service updateService = new()
            {
                Id = service.Id,
                Description = service.Description,
                Title = serviceTransfer.Title,
                Created = service.Created,
            };

            _context.Entry(service).CurrentValues.SetValues(updateService);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostService(ServiceTransfer serviceTrans)
        {
            if (_context.Services == null)
                return Problem("Entity set 'SkillProfiDbContext.Services'  is null.");

            Service service = new()
            {
                Id = Guid.NewGuid(),
                Title = serviceTrans.Title,
                Description = serviceTrans.Description,
                Created = DateTime.Now,
            };         

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            if (_context.Services == null)
                return Problem("Entity set 'SkillProfiDbContext.Services'  is null.");

            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(Guid id)
        {
            return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
