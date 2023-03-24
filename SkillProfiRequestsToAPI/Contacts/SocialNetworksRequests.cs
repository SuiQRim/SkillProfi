using SkillProfi.Contacts;

namespace SkillProfiRequestsToAPI.Contacts
{
    public class SocialNetworksRequests : RequestController
    {
        public SocialNetworksRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts/SocialNetworks") { }

        public async Task<List<SocialNetwork>> GetListAsync() =>
            await Request.GetAsync<List<SocialNetwork>>(Url);

        public async Task<string> AddAsync(SocialNetworkTransfer socialNetwork, Stream stream)
        {
			var obj = BuildObjectWithImage(socialNetwork, stream);
			return await Request.AddAsync(obj, Url);
        }

        public async Task<string> EditAsync(string id, SocialNetworkTransfer socialNetwork, Stream? stream)
        {
			var obj = BuildObjectWithImage(socialNetwork, stream);
			return await Request.EditAsync(obj, Url, id); 
        }

        public async Task<string> DeleteByIdAsync(string id) =>
            await Request.DeleteAsync(id, Url);
    }
}
