using SkillProfi.Contacts;

namespace SkillProfiRequestsToAPI.Contacts
{
    public class SocialNetworksRequests : RequestController
    {
        public SocialNetworksRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Contacts/SocialNetworks") { }

        public List<SocialNetwork> GetList() =>
            Request.Get<List<SocialNetwork>>(Url);

        public async Task<List<SocialNetwork>> GetListAsync() =>
            await Request.GetAsync<List<SocialNetwork>>(Url);



        public string Add(SocialNetworkTransfer socialNetwork, Stream stream, string accessToken)
        {
			var obj = BuildObjectWithImage(socialNetwork, stream);
			return Request.Add(obj, Url, accessToken);
        }


        public async Task<string> AddAsync(SocialNetworkTransfer socialNetwork, Stream stream, string accessToken)
        {
			var obj = BuildObjectWithImage(socialNetwork, stream);
			return await Request.AddAsync(obj, Url, accessToken);
        }




        public string Edit(string id, SocialNetworkTransfer socialNetwork, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(socialNetwork, stream);
			return Request.Edit(obj, Url, id, accessToken);
        }


        public async Task<string> EditAsync(string id, SocialNetworkTransfer socialNetwork, Stream? stream, string accessToken)
        {
			var obj = BuildObjectWithImage(socialNetwork, stream);
			return await Request.EditAsync(obj, Url, id, accessToken); 
        }



        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);
    }
}
