using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfiApi.Data;

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
            Contacts? contacts = await ContactsFile.GetContactsWithImagesAsync();

            if (contacts == null) return NotFound();

            return contacts;
        }

        // PUT: api/Contacts
        [HttpPut]
        public async Task<ActionResult<Contacts>> PutContacts(Contacts contacts)
        { 
            if (contacts == null) return NotFound();

            await ContactsFile.EditMainContacts(contacts);

            return Ok(await ContactsFile.GetContactsAsync());
        }

        // GET: api/Contacts/SocialNetworks
        [HttpGet("SocialNetworks")]
        public async Task<ActionResult<List<SocialNetwork>>> GetSocialNetworks()
        {
            Contacts? contacts = await ContactsFile.GetContactsWithImagesAsync();

            if (contacts == null || contacts.SocialNetworks == null) return NotFound();

            return contacts.SocialNetworks;
        }

        // PUT: api/Contacts/SocialNetworks
        [HttpPut("SocialNetworks/{id}")]
        public async Task<ActionResult> PutSocialNetworks(Guid id, SocialNetwork socialNetwork)
        {

            if (!await ContactsFile.IsExcistSocialNetworkById(id)) return NotFound(id);

            await ContactsFile.EditSocialNetwork(id, socialNetwork);

            return Ok();
        }


        // DELETE: api/Contacts/SocialNetworks
        [HttpDelete("SocialNetworks/{id}")]
        public async Task<ActionResult> DeleteSocialNetworks(Guid id)
        {
            if (!await ContactsFile.IsExcistSocialNetworkById(id)) return NotFound(id);

            await ContactsFile.DeleteSocialNetwork(id);

            return Ok();
        }
         
        // POST: api/Contacts/SocialNetworks
        [HttpPost("SocialNetworks")]
        public async Task<ActionResult> PostSocialNetworks(SocialNetwork socialNetwork)
        {
            await ContactsFile.AddSocialNetwork(socialNetwork);

            return Ok();
        }
    }
}
