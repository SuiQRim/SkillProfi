namespace SkillProfiRequestsToAPI.Contacts
{
    public class ContactsRequests : RequestController
    {
        public ContactsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts") {}

        public SkillProfi.Contacts.Contacts Get() => 
            Request.Get<SkillProfi.Contacts.Contacts>(Url);
        

        public string Edit(SkillProfi.Contacts.Contacts contact, string accessToken) =>
            Request.Edit(contact, Url, accessToken : accessToken);


        public async Task<string> EditAsync(SkillProfi.Contacts.Contacts contact, string accessToken) => 
            await Request.EditAsync(contact, Url, accessToken: accessToken);

    }
}
