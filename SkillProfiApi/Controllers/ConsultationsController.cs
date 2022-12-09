using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfi.Consult;
using SkillProfiApi.Data;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly SkillProfiDbContext _context;

        public ConsultationsController(SkillProfiDbContext context)
        {
            _context = context;
        }

        // GET: api/Consultations
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Consultation>>> GetConsultations()
        {
            if (_context.Consultations == null)
                return Problem("Entity set 'SkillProfiDbContext.Consultations'  is null.");

            if (_context.Consultations == null)
            {
                return NotFound();
            }
            return Ok(await _context.Consultations.ToListAsync());
        }

        // GET: api/Consultations/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Consultation>> GetConsultation(Guid id)
        {
            if (_context.Consultations == null)
                return Problem("Entity set 'SkillProfiDbContext.Consultations'  is null.");

            if (_context.Consultations == null)
            {
                return NotFound();
            }
            var consultation = await _context.Consultations.FindAsync(id);

            if (consultation == null)
            {
                return NotFound();
            }

            return Ok(consultation);
        }

        // PUT: api/Consultations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutConsultation(Guid id, Consultation consultation)
        {
            if (_context.Consultations == null)
                return Problem("Entity set 'SkillProfiDbContext.Consultations'  is null.");

            if (id != consultation.Id)
                return BadRequest();

            _context.Entry(consultation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultationExists(id))
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

        // POST: api/Consultations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostConsultation(ConsultationTransfer consultation)
        {
            if (_context.Consultations == null)
                return Problem("Entity set 'SkillProfiDbContext.Consultations'  is null.");

            Consultation cons = new() 
            {
				Id = Guid.NewGuid(),
                Name = consultation.Name,
                Description= consultation.Description,
                EMail= consultation.EMail,
			    Status = ConsultationStatus.Received,
			    Created = DateTime.Now
		    };

            await _context.Consultations.AddAsync(cons);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Consultations/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteConsultation(Guid id)
        {
            if (_context.Consultations == null)
                return Problem("Entity set 'SkillProfiDbContext.Consultations'  is null.");

            if (_context.Consultations == null)
            {
                return NotFound();
            }
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation == null)
            {
                return NotFound();
            }

            _context.Consultations.Remove(consultation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultationExists(Guid id)
        {
            return (_context.Consultations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
