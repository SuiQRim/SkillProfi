using Newtonsoft.Json;
using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Contacts
{
    public static class ContactsRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Contacts";

        public static SkillProfi.Contacts GetContacts() => 
            Request.Get<SkillProfi.Contacts>("https://localhost:7120/api/Contacts");
        

        public static string EditContacts(SkillProfi.Contacts contact, string accessToken) =>
            Request.Edit(contact, _mainUrl, accessToken : accessToken);


        public static async Task<string> EditContactsAsync(SkillProfi.Contacts contact, string accessToken) => 
            await Request.EditAsync(contact, _mainUrl, accessToken: accessToken);


        

    }
}
