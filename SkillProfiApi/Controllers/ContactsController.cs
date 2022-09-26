using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfiApi.Data;
using SkillProfi;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        // GET: api/Contacts
        [HttpGet]
        public async Task<ActionResult<Contacts>> GetContacts()
        {
            Contacts? contacts = await ContactsFile.GetContactsAsync();
            if (contacts == null)
            {
                return NotFound();
            }

            return contacts;
        }
    }
}
