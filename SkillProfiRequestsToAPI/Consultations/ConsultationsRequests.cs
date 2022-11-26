﻿using System.Net;
using System.Text;
using SkillProfi;
using Newtonsoft.Json;

namespace SkillProfiRequestsToAPI.Consultations
{
    public static class ConsultationsRequests
    {
        private const string _mainUrl = "https://localhost:7120/api/Consultations";

        public static List<Consultation> GetConsultations(string successToken) =>
            Request.Get<List<Consultation>>(_mainUrl, successToken);

        public static async Task<List<Consultation>> GetConsultationsAsync(string successToken) => 
            await Request.GetAsync<List<Consultation>>(_mainUrl, successToken);



        public static Consultation GetConsultation(string id, string accessToken) => 
            Request.Get<Consultation>($"{_mainUrl}/{id}", accessToken);

        public static async Task<Consultation> GetConsultationAsync(string id, string accessToken) =>
            await Request.GetAsync<Consultation>($"{_mainUrl}/{id}", accessToken);



        public static string AddConsultation(Consultation Consultation) => 
            Request.Add(Consultation, _mainUrl);

        public static async Task<string> AddConsultationAsync(Consultation Consultation) => 
            await Request.AddAsync(Consultation, _mainUrl);



        public static string EditConsultation(string id, Consultation Consultation, string accessToken) 
            => Request.Edit(Consultation,_mainUrl, id, accessToken);

        public static async Task<string> EditConsultationAsync(string id, Consultation Consultation, string accessToken) 
            => await Request.EditAsync(Consultation,_mainUrl, id, accessToken);



        public static string DeleteConsultation(string id, string accessToken) => 
            Request.Delete(id, _mainUrl, accessToken);

        public static async Task<string> DeleteConsultationAsync(string id, string accessToken) => 
            await Request.DeleteAsync(id, _mainUrl, accessToken);

    }
}
