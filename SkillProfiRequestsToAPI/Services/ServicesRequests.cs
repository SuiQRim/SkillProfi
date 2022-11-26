using Newtonsoft.Json;
using SkillProfi;
using System.Net;
using System.Text;

namespace SkillProfiRequestsToAPI.Services
{
    public class ServicesRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Services";

        public static List<Service> GetServices() => 
            Request.Get<List<Service>>(_mainUrl);

        public static async Task<List<Service>> GetServicesAsync() => 
            await Request.GetAsync<List<Service>>(_mainUrl);



        public static Service GetService(string id) =>
            Request.Get<Service>($"{_mainUrl}/{id}");
        
        public static async Task<Service> GetServiceAsync(string id) => 
            await Request.GetAsync<Service>($"{_mainUrl}/{id}");



        public static string AddService(Service Service, string accessToken) => 
            Request.Add(Service, _mainUrl, accessToken); 

        public static async Task<string> AddServiceAsync(Service Service, string accessToken) => 
            await Request.AddAsync(Service, _mainUrl, accessToken);
        


        public static string EditService(string id, Service Service, string accessToken) =>
            Request.Edit(Service, _mainUrl, id, accessToken);

        public static async Task<string> EditServiceAsync(string id, Service Service, string accessToken) => 
            await Request.EditAsync(Service, _mainUrl, id, accessToken);

       

        public static string DeleteService(string id, string accessToken) => 
            Request.Delete(id, _mainUrl, accessToken);
        
        public static async Task<string> DeleteServiceAsync(string id, string accessToken) => 
            await Request.DeleteAsync(id, _mainUrl, accessToken);

    }
}
