using SkillProfi.Consult;

namespace SkillProfiRequestsToAPI.Consultations
{
    public class ConsultationsRequests : RequestController
    {
        public ConsultationsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Consultations"){}

        public List<Consultation> GetList(string successToken) =>
            Request.Get<List<Consultation>>(Url, successToken);

        public async Task<List<Consultation>> GetListAsync(string successToken) => 
            await Request.GetAsync<List<Consultation>>(Url, successToken);



        public Consultation GetById(string id, string accessToken) => 
            Request.Get<Consultation>($"{Url}/{id}", accessToken);

        public async Task<Consultation> GetByIdAsync(string id, string accessToken) =>
            await Request.GetAsync<Consultation>($"{Url}/{id}", accessToken);



        public string Add(ConsultationTransfer Consultation) => 
            Request.Add(Consultation, Url);

        public async Task<string> AddAsync(ConsultationTransfer Consultation) => 
            await Request.AddAsync(Consultation, Url);



        public string Edit(string id, Consultation Consultation, string accessToken) =>
            Request.Edit(Consultation,Url, id, accessToken);

        public async Task<string> EditAsync(string id, Consultation Consultation, string accessToken) =>
            await Request.EditAsync(Consultation,Url, id, accessToken);



        public string DeleteById(string id, string accessToken) => 
            Request.Delete(id, Url, accessToken);

        public async Task<string> DeleteByIdAsync(string id, string accessToken) => 
            await Request.DeleteAsync(id, Url, accessToken);

    }
}
