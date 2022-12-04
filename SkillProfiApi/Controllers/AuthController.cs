using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SkillProfi;
using SkillProfiApi.Data;
using SkillProfiApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        public async Task<ActionResult<AuthParameters>> Login(Account account)
        {
            UserAccount? userAcc = new() {Login = account.Login, Password = account.Password };
			userAcc = await _context.Accounts.SingleOrDefaultAsync(a => a.Login == userAcc.Login);

            if (userAcc == null) return Ok(new RequestResponce(1, "Login or password is wrong"));
            if (userAcc.Password != account.Password)
                return Ok(new RequestResponce(1, "Login or password is wrong"));

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, account.Login) };

            JwtSecurityToken jwt = new (
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return Ok(new RequestResponce(0, "Login is access",

                new AuthParameters { 
                    Login = account.Login,
                    IsLogin = true,
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt) 
                }

            ));
        }

    }
}
