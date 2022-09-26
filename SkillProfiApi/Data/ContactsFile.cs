using Newtonsoft.Json;
using SkillProfi;

namespace SkillProfiApi.Data
{
    public static class ContactsFile
    {
        private const string REPOSITORY = "DataFiles";

        private const string FILE_FULL_NAME = "Contacts.json";

        public static async Task<Contacts?> GetContactsAsync()
        {
            Contacts contacts;
            string path = Path.Combine(REPOSITORY, FILE_FULL_NAME);
            using (StreamReader r = new (path))
            {
                string json = await r.ReadToEndAsync();
                contacts = JsonConvert.DeserializeObject<Contacts>(json);
            }

            return contacts;
        }

    }
}
