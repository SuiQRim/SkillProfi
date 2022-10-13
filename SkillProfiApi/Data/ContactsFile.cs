using Newtonsoft.Json;
using SkillProfi;

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

        public static async Task<Contacts?> GetContactsWithImagesAsync()
        {
            Contacts? contacts = await GetContactsAsync();

            foreach (var sc in contacts.SocialNetworks) sc.GetPictureAsync();

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


        public async static Task EditSocialNetwork(Guid id, SocialNetwork socialNetwork) 
        {
            Contacts contacts = await GetContactsAsync();

            int a = contacts.SocialNetworks.IndexOf(contacts.SocialNetworks.First(s => s.Id == id));

            contacts.SocialNetworks[a] = socialNetwork;

            contacts.Save();
            
        }

        public async static Task AddSocialNetwork(SocialNetwork socialNetwork)
        {
            Contacts contacts = await GetContactsAsync();

            socialNetwork.Id = Guid.NewGuid();

            contacts.SocialNetworks.Add(socialNetwork);
            await socialNetwork.SavePictureAsync();

            contacts.Save();

        }

        public async static Task DeleteSocialNetwork(Guid id)
        {
            Contacts contacts = await GetContactsAsync();

            SocialNetwork socialNetwork = contacts.SocialNetworks.First(s => s.Id == id);
            contacts.SocialNetworks.Remove(socialNetwork);
            socialNetwork.RemovePicture();
         
            contacts.Save();

        }

        private static void Save(this Contacts contacts)
        {
            foreach (var sc in contacts.SocialNetworks)
            {
                if (sc.PictureBytePresentation != null) sc.PictureBytePresentation = null;
            }
            string contactsJson = JsonConvert.SerializeObject(contacts);


            File.WriteAllText(CreatePath(), contactsJson);

        }
    }
}
