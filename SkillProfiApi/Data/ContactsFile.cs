using Newtonsoft.Json;
using SkillProfi;
using SkillProfi.Contacts;
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

        public static async Task<SocialNetwork?> GetSocialNetwork(Guid id) 
        {
            Contacts contacts = await GetContactsAsync() ?? throw new NullReferenceException();
            List<SocialNetwork> socNets = contacts.SocialNetworks ?? throw new NullReferenceException();
            return socNets.SingleOrDefault(s => s.Id == id);   
        }

        public async static Task<bool> IsExcistSocialNetworkById(Guid id) 
        {
            Contacts? contacts = await GetContactsAsync();

            if (contacts == null || contacts.SocialNetworks == null) return false;

            return contacts.SocialNetworks.Exists(s => s.Id == id);
        }

        public async static Task EditMainContacts(ContactsTransfer transfer) 
        {
            Contacts? contacts = await GetContactsAsync();

            contacts = new()
            {
                Adress = transfer.Adress,
                PhoneNumber = transfer.PhoneNumber,
                Email = transfer.Email,
                LinkToMapContructor = transfer.LinkToMapContructor,
                SocialNetworks = contacts.SocialNetworks
            };

            contacts.Save();

        }

        public async static Task EditSocialNetwork(SocialNetwork sc)
        {
            Contacts contacts = await GetContactsAsync();

            int a = contacts.SocialNetworks.IndexOf(contacts.SocialNetworks.First(s => s.Id == sc.Id));

            contacts.SocialNetworks[a] = sc;

            contacts.Save();
        }

        public async static Task AddSocialNetwork(SocialNetwork socialNetwork, byte[] image)
        {
            Contacts contacts = await GetContactsAsync();

            contacts.SocialNetworks.Add(socialNetwork);
            await PictureDirectory.SavePictureAsync(socialNetwork, image);

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
