using SkillProfi.Service;

namespace SkillProfiRequestsToAPI.Services
{
    public class ServicesRequests : RequestController
    {
        public ServicesRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Services")
        {
        }

        public List<Service> GetList() =>
            Request.Get<List<Service>>(Url);

        public async Task<List<Service>> GetListAsync() =>
            await Request.GetAsync<List<Service>>(Url);



        public Service GetById(string id) =>
            Request.Get<Service>($"{Url}/{id}");

        public async Task<Service> GetByIdAsync(string id) =>
            await Request.GetAsync<Service>($"{Url}/{id}");



        public string Add(ServiceTransfer Service, string accessToken) =>
            Request.Add(Service, Url, accessToken);

        public async Task<string> AddAsync(ServiceTransfer Service, string accessToken) =>
            await Request.AddAsync(Service, Url, accessToken);



        public string Edit(string id, ServiceTransfer Service, string accessToken) =>
            Request.Edit(Service, Url, id, accessToken);

        public async Task<string> EditAsync(string id, ServiceTransfer Service, string accessToken) =>
            await Request.EditAsync(Service, Url, id, accessToken);



        public string DeleteById(string id, string accessToken) =>
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) =>
            await Request.DeleteAsync(id, Url, accessToken);

    }
}
