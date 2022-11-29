using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillProfi;
using SkillProfiApi.Data;
using SkillProfiApi.Models;

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

            if (contacts == null) return NotFound();

            return contacts;
        }

        // PUT: api/Contacts
        [HttpPut]
        [Authorize]
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
            Contacts? contacts = await ContactsFile.GetContactsAsync();

            if (contacts == null || contacts.SocialNetworks == null) return NotFound();

            return contacts.SocialNetworks;
        }

        // PUT: api/Contacts/SocialNetworks
        [HttpPut("SocialNetworks/{id}")]
        [Authorize]
        public async Task<ActionResult> PutSocialNetworks(Guid id, ObjectWithImage<SocialNetwork> socialNetwork)
        {

            if (!await ContactsFile.IsExcistSocialNetworkById(id)) return NotFound(id);

            await ContactsFile.EditSocialNetwork(id, socialNetwork);
			await PictureDirectory.SavePictureAsync(socialNetwork);

			return Ok();
        }


        // DELETE: api/Contacts/SocialNetworks
        [HttpDelete("SocialNetworks/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteSocialNetworks(Guid id)
        {
            if (!await ContactsFile.IsExcistSocialNetworkById(id)) return NotFound(id);

            await ContactsFile.DeleteSocialNetwork(id);

            return Ok();
        }
         
        // POST: api/Contacts/SocialNetworks
        [HttpPost("SocialNetworks")]
        [Authorize]
        public async Task<ActionResult> PostSocialNetworks(ObjectWithImage<SocialNetwork> socialNetwork)
        {
            await ContactsFile.AddSocialNetwork(socialNetwork);

            return Ok();
        }
    }
}
