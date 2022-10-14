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

        public static List<SocialNetwork> GetSocialNetworks() => Request.Get<List<SocialNetwork>>(_mainUrl);

        public static async Task<List<SocialNetwork>> GetSocialNetworksAsync() => await Request.GetAsync<List<SocialNetwork>>(_mainUrl);



        public static string AddSocialNetwork(SocialNetwork SocialNetwork) => Request.Add(SocialNetwork, _mainUrl);

        public static async Task<string> AddSocialNetworkAsync(SocialNetwork SocialNetwork) => await Request.AddAsync(SocialNetwork, _mainUrl);



        public static string EditSocialNetwork(string id, SocialNetwork SocialNetwork) => Request.Edit(id, SocialNetwork , _mainUrl);

        public static async Task<string> EditSocialNetworkAsync(string id, SocialNetwork SocialNetwork) => await Request.EditAsync(id, SocialNetwork, _mainUrl);


        public static string DeleteSocialNetwork(string id) => Request.Delete(id, _mainUrl);

        public static async Task<string> DeleteSocialNetworkAsync(string id) => await Request.DeleteAsync(id, _mainUrl);
    }
}
