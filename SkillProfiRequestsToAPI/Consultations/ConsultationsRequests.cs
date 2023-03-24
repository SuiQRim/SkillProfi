using SkillProfi.Consult;

namespace SkillProfiRequestsToAPI.Consultations
{
    public class ConsultationsRequests : RequestController
    {
        public ConsultationsRequests(Func<string> getBaseUrl) : base(getBaseUrl, "Consultations"){}

        public async Task<List<Consultation>> GetListAsync() => 
            await Request.GetAsync<List<Consultation>>(Url);

        public async Task<Consultation> GetByIdAsync(string id) =>
            await Request.GetAsync<Consultation>($"{Url}/{id}");

        public async Task<string> AddAsync(ConsultationTransfer Consultation) => 
            await Request.AddAsync(Consultation, Url);

        public async Task<string> EditAsync(string id, Consultation Consultation) =>
            await Request.EditAsync(Consultation,Url, id);

        public async Task<string> DeleteByIdAsync(string id) => 
            await Request.DeleteAsync(id, Url);

    }
}
