using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfiApi.Data;
using SkillProfiApi.Models;
using System.Linq;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SkillProfiDbContext _context;
        public AuthController(SkillProfiDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> GetConsultations(Account account)
        {
            Account? acc = await _context.Accounts.SingleOrDefaultAsync(a => a.Name == account.Name);
            if (acc == null) return Ok(new RequestResponce(1, "Login or password is wrong"));
            if (acc.Password != account.Password)
                return Ok(new RequestResponce(1, "Login or password is wrong"));
               
            return Ok(new RequestResponce(0, "Login is access"));
        }

    }
}
