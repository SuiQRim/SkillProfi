using SkillProfiRequestsToAPI.Accounts;
using SkillProfiRequestsToAPI.Blogs;
using SkillProfiRequestsToAPI.Consultations;
using SkillProfiRequestsToAPI.Contacts;
using SkillProfiRequestsToAPI.Images;
using SkillProfiRequestsToAPI.Projects;
using SkillProfiRequestsToAPI.Services;

namespace SkillProfiRequestsToAPI
{
    public class SkillProfiWebClient
    {
        public SkillProfiWebClient(string baseUrl) 
        {
            BaseURL = baseUrl;
            Accounts = new (GetBaseUrl);
            Blogs = new(GetBaseUrl);
            Contacts = new(GetBaseUrl);
            Consultations = new(GetBaseUrl);
            SocialNetworks = new(GetBaseUrl);
            Pictures = new(GetBaseUrl);
            Projects = new(GetBaseUrl);
            Services = new(GetBaseUrl);
        }

        public string BaseURL { get; set; }
        private string GetBaseUrl() => BaseURL;


        public readonly AccountsRequests Accounts;

        public readonly BlogsRequests Blogs;

        public readonly ConsultationsRequests Consultations;

        public readonly ContactsRequests Contacts;

        public readonly SocialNetworksRequests SocialNetworks;

        public readonly PicturesRequests Pictures;

        public readonly ProjectsRequests Projects;

        public readonly ServicesRequests Services;

    }
}
