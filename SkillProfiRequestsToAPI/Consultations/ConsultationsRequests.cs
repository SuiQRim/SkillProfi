using System.Net;
using System.Text;
using SkillProfi;
using Newtonsoft.Json;

namespace SkillProfiRequestsToAPI.Consultations
{
    public static class ConsultationsRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Consultations";

        public static List<Consultation> GetConsultations() => Request.Get<List<Consultation>>(_mainUrl);

        public static async Task<List<Consultation>> GetConsultationsAsync() => await Request.GetAsync<List<Consultation>>(_mainUrl);



        public static Consultation GetConsultation(string id) => Request.Get<Consultation>($"{_mainUrl}/{id}");

        public static async Task<Consultation> GetConsultationAsync(string id) => await Request.GetAsync<Consultation>($"{_mainUrl}/{id}");



        public static string AddConsultation(Consultation Consultation) => Request.Add(Consultation, _mainUrl);

        public static async Task<string> AddConsultationAsync(Consultation Consultation) => await Request.AddAsync(Consultation, _mainUrl);



        public static string EditConsultation(string id, Consultation Consultation) => Request.Edit(id, Consultation, _mainUrl);

        public static async Task<string> EditConsultationAsync(string id, Consultation Consultation) => await Request.EditAsync(id, Consultation, _mainUrl);



        public static string DeleteConsultation(string id) => Request.Delete(id, _mainUrl);

        public static async Task<string> DeleteConsultationAsync(string id) => await Request.DeleteAsync(id, _mainUrl);

    }
}
