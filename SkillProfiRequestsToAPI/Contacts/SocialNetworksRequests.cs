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
    public static class SocialNetworksRequests
    {

        private const string _mainUrl = "https://localhost:7120/api/Contacts/SocialNetworks";

        public static List<SocialNetwork> GetSocialNetworks() => 
            Request.Get<List<SocialNetwork>>(_mainUrl);

        public static async Task<List<SocialNetwork>> GetSocialNetworksAsync() => 
            await Request.GetAsync<List<SocialNetwork>>(_mainUrl);



        public static string AddSocialNetwork(SocialNetwork SocialNetwork, string accessToken) => 
            Request.Add(SocialNetwork, _mainUrl, accessToken);

        public static async Task<string> AddSocialNetworkAsync(SocialNetwork SocialNetwork, string accessToken) =>
            await Request.AddAsync(SocialNetwork, _mainUrl, accessToken);



        public static string EditSocialNetwork(string id, SocialNetwork SocialNetwork, string accessToken) => 
            Request.Edit(SocialNetwork, _mainUrl, id, accessToken);

        public static async Task<string> EditSocialNetworkAsync(string id, SocialNetwork SocialNetwork, string accessToken) => 
            await Request.EditAsync(SocialNetwork, _mainUrl, id, accessToken);


        public static string DeleteSocialNetwork(string id, string accessToken) => 
            Request.Delete(id, _mainUrl, accessToken);

        public static async Task<string> DeleteSocialNetworkAsync(string id, string accessToken) => 
            await Request.DeleteAsync(id, _mainUrl, accessToken);
    }
}
