using SkillProfi.Contacts;

namespace SkillProfiRequestsToAPI.Contacts
{
    public class ContactsRequests : RequestController
    {
        public ContactsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts") {}

        public async Task<SkillProfi.Contacts.Contacts> GetAsync() => 
           await Request.GetAsync<SkillProfi.Contacts.Contacts>(Url);      

        public async Task<string> EditAsync(ContactsTransfer contact) => 
            await Request.EditAsync(contact, Url);

    }
}
