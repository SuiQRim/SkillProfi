using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SkillProfi;
using Newtonsoft.Json;
using SkillProfiWPF.Models;
using SkillProfiWPF.Views;
using System.Windows.Input;
using SkillProfiWPF.ViewModels.Prefab;

namespace SkillProfiWPF.ViewModels
{
    internal class ConsultationsViewModel : Prefab.ViewModel
    {
        public ConsultationsViewModel()
        {

            SetDateDiapasoneToday = new LamdaCommand(OnSetDateDiapasoneToday, CanAnyWay);
            SetDateDiapasoneYesterday = new LamdaCommand(OnSetDateDiapasoneYesterday, CanAnyWay);
            SetDateDiapasoneWeek = new LamdaCommand(OnSetDateDiapasoneWeek, CanAnyWay);
            SetDateDiapasoneMonth = new LamdaCommand(OnSetDateDiapasoneMonth, CanAnyWay);

            Consultations = new ObservableCollection<Consultation>(GetConsultations() ?? new());
            FirstDate = DateTime.Now;
            LastDate = DateTime.Now;
        }

        private DateTime _firstDate;
        public DateTime FirstDate 
        {
            get => _firstDate;
            set
            {
                Set(ref _firstDate, value);
                FilteredConsultations = new(ConsultationsFilter.FilterByDate(Consultations, FirstDate, LastDate));
            }
        }

        private DateTime _lastDate;
        public DateTime LastDate
        {
            get => _lastDate;
            set
            {
                Set(ref _lastDate, value);
                FilteredConsultations = new (ConsultationsFilter.FilterByDate(Consultations, FirstDate, LastDate));
            }
        }

        private bool CanAnyWay(object p) => true;
        public ICommand SetDateDiapasoneToday { get; }
        private void OnSetDateDiapasoneToday(object p)
        {
            LastDate = DateTime.Now;
            FirstDate = DateTime.Now;
        }
        public ICommand SetDateDiapasoneYesterday { get; }
        private void OnSetDateDiapasoneYesterday(object p)
        {
            LastDate = DateTime.Now.AddDays(-1);
            FirstDate = DateTime.Now;
        }
        public ICommand SetDateDiapasoneWeek { get; }
        private void OnSetDateDiapasoneWeek(object p)
        {
            LastDate = DateTime.Now.AddDays(-7);
            FirstDate = DateTime.Now;
        }
        public ICommand SetDateDiapasoneMonth { get; }
        private void OnSetDateDiapasoneMonth(object p)
        {
            LastDate = DateTime.Now.AddMonths(-1);
            FirstDate = DateTime.Now;
        }



        private ObservableCollection<Consultation> _consultations;
        public ObservableCollection<Consultation> Consultations
        {
            get => _consultations;
            set => Set(ref _consultations, value);
        }

        private ObservableCollection<Consultation> _filteredConsultations;
        public ObservableCollection<Consultation> FilteredConsultations
        {
            get => _filteredConsultations;
            set => Set(ref _filteredConsultations, value);
        }

        public static List<Consultation>? GetConsultations(string uri = "https://localhost:7120/api/Consultations")
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            string text;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new(stream))
            {
                text = reader.ReadToEnd();
            }
            
            return JsonConvert.DeserializeObject<List<Consultation>>(text);
        }
    }
}