using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillProfi;
using SkillProfi.Contacts;
using SkillProfiApi.Data;
using SkillProfiApi.Data.Picture;

namespace SkillProfiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
		private readonly ILogger<ProjectsController> _logger;

        public ContactsController(ILogger<ProjectsController> logger)
        {
            _logger = logger;
        }

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
        public async Task<ActionResult<IEnumerable<SocialNetwork>>> GetSocialNetworks()
        {
            Contacts? contacts = await ContactsFile.GetContactsAsync();

            if (contacts == null || contacts.SocialNetworks == null) return NotFound();

            return contacts.SocialNetworks;
        }

        // PUT: api/Contacts/SocialNetworks
        [HttpPut("SocialNetworks/{id}")]
        [Authorize]
        public async Task<IActionResult> PutSocialNetworks(Guid id, ObjectWithPicture<SocialNetworkTransfer> socialNetwork)
        {
            SocialNetworkTransfer socNetTransfer = socialNetwork.Object;
            SocialNetwork? socNet = await ContactsFile.GetSocialNetwork(id);

            if (socNet == null) 
                return NotFound();

            socNet = new()
            {
                Id = socNet.Id,
                Link = socNetTransfer.Link,
                PictureName = socNet.PictureName
            };

            try { await PictureDirectory.SavePictureAsync(socNet, socialNetwork.Picture); }
			catch (PictureNullException e)
			{
				_logger.LogWarning(exception: e, $"{nameof(socialNetwork)} saved without image");
                return BadRequest(e.Message);
			}
			await ContactsFile.EditSocialNetwork(socNet);

			return NoContent();
        }

        // POST: api/Contacts/SocialNetworks
        [HttpPost("SocialNetworks")]
        [Authorize]
        public async Task<IActionResult> PostSocialNetworks(ObjectWithPicture<SocialNetworkTransfer> socialNetwork)
        {
            SocialNetwork socNet = new()
            {
                Id = Guid.NewGuid(),
                Link = socialNetwork.Object.Link
            };

            await ContactsFile.AddSocialNetwork(socNet, socialNetwork.Picture);

            return NoContent();
        }

        // DELETE: api/Contacts/SocialNetworks
        [HttpDelete("SocialNetworks/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSocialNetworks(Guid id)
        {
            if (!await ContactsFile.IsExcistSocialNetworkById(id)) return NotFound(id);

            var socialNetwork = await ContactsFile.DeleteSocialNetworkAsync(id);

			try { socialNetwork.RemovePicture(); }
			catch (PictureNotFound e)
			{
				_logger.LogWarning(exception: e, "The image cannot be deleted because it's not found");
			}

			return NoContent();
        } 
    }
}
