using SkillProfi;

namespace SkillProfiRequestsToAPI.Contacts
{
    public class SocialNetworksRequests : RequestController
    {
        public SocialNetworksRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts/SocialNetworks") { }

        public List<SocialNetwork> GetList() =>
            Request.Get<List<SocialNetwork>>(Url);

        public async Task<List<SocialNetwork>> GetListAsync() =>
            await Request.GetAsync<List<SocialNetwork>>(Url);



        public string Add(SocialNetwork SocialNetwork, string accessToken) =>
            Request.Add(SocialNetwork, Url, accessToken);

        public async Task<string> AddAsync(SocialNetwork SocialNetwork, string accessToken) =>
            await Request.AddAsync(SocialNetwork, Url, accessToken);



        public string Edit(string id, SocialNetwork SocialNetwork, string accessToken) =>
            Request.Edit(SocialNetwork, Url, id, accessToken);

        public async Task<string> EditAsync(string id, SocialNetwork SocialNetwork, string accessToken) =>
            await Request.EditAsync(SocialNetwork, Url, id, accessToken);


        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);
    }
}
