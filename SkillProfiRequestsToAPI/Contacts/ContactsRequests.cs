using SkillProfi.Contacts;

namespace SkillProfiRequestsToAPI.Contacts
{
    public class ContactsRequests : RequestController
    {
        public ContactsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts") {}

        public SkillProfi.Contacts.Contacts Get() => 
            Request.Get<SkillProfi.Contacts.Contacts>(Url);
        

        public string Edit(ContactsTransfer contact, string accessToken) =>
            Request.Edit(contact, Url, accessToken : accessToken);


        public async Task<string> EditAsync(ContactsTransfer contact, string accessToken) => 
            await Request.EditAsync(contact, Url, accessToken: accessToken);

    }
}
