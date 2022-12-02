using Newtonsoft.Json;
using SkillProfi;
using SkillProfiApi.Data.Picture;

namespace SkillProfiApi.Data
{
    internal static class ContactsFile
    {
        private const string REPOSITORY = "DataFiles";

        private const string FILE_FULL_NAME = "Contacts.json";

        private static string CreatePath() => Path.Combine(REPOSITORY, FILE_FULL_NAME);
        

        public static async Task<Contacts?> GetContactsAsync()
        {
            Contacts? contacts;
            string path = CreatePath();
            using (StreamReader r = new (path))
            {
                string json = await r.ReadToEndAsync();
                contacts = JsonConvert.DeserializeObject<Contacts>(json);
            }

            return contacts;
        }

        public async static Task<bool> IsExcistSocialNetworkById(Guid id) 
        {
            Contacts? contacts = await GetContactsAsync();

            if (contacts == null || contacts.SocialNetworks == null) return false;

            return contacts.SocialNetworks.Exists(s => s.Id == id);
        }

        public async static Task EditMainContacts(Contacts modifyContacts) 
        {
            Contacts? contacts = await GetContactsAsync();

            contacts.Adress = modifyContacts.Adress;
            contacts.PhoneNumber = modifyContacts.PhoneNumber;
            contacts.Email = modifyContacts.Email;
            contacts.LinkToMapContructor = modifyContacts.LinkToMapContructor;

            contacts.Save();

        }


        public async static Task EditSocialNetwork(Guid id, ObjectWithPicture<SocialNetwork> socialNetwork) 
        {
            Contacts contacts = await GetContactsAsync();

            int a = contacts.SocialNetworks.IndexOf(contacts.SocialNetworks.First(s => s.Id == id));

            contacts.SocialNetworks[a] = socialNetwork.Object;

            contacts.Save();
            
        }

        public async static Task AddSocialNetwork(ObjectWithPicture<SocialNetwork> socialNetwork)
        {
            Contacts contacts = await GetContactsAsync();

            socialNetwork.Object.Id = Guid.NewGuid();

            contacts.SocialNetworks.Add(socialNetwork.Object);
            await PictureDirectory.SavePictureAsync(socialNetwork);

            contacts.Save();

        }

        public async static Task<SocialNetwork> DeleteSocialNetworkAsync(Guid id)
        {
            Contacts contacts = await GetContactsAsync();

            SocialNetwork socialNetwork = contacts.SocialNetworks.First(s => s.Id == id);
            contacts.SocialNetworks.Remove(socialNetwork);
         
            contacts.Save();

            return socialNetwork;
        }

        private static void Save(this Contacts contacts)
        {
            string contactsJson = JsonConvert.SerializeObject(contacts);
            File.WriteAllText(CreatePath(), contactsJson);
        }
    }
}
