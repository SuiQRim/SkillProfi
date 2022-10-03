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

            SetDateDiapasone =  new LamdaCommand(OnSetDateDiapasone, CanAnyWay);
            EditConsultationStatus = new LamdaCommand(OnEditConsultationStatus, CanAnyWay);
            SaveConsultationStatus = new LamdaCommand(OnSaveConsultationStatus, CanAnyWay);

            IsOpenEditStatusMenuElement = false;

            Consultations = new ObservableCollection<Consultation>(GetConsultations() ?? new());
            FirstDate = DateTime.Now;
            LastDate = DateTime.Now;
        }

        private ConsultationStatus _selectedStatus;
        public ConsultationStatus SelectedStatus
        {
            get => _selectedStatus;
            set => Set(ref _selectedStatus, value);
        }

        private Consultation _selectedConsultation;
        public Consultation SelectedConsultation
        {
            get => _selectedConsultation;
            set => Set(ref _selectedConsultation, value);
        }

        public ICommand SaveConsultationStatus { get; }
        private void OnSaveConsultationStatus(object p)
        {
            IsOpenEditStatusMenuElement = false;
        }

        public ICommand EditConsultationStatus { get; }
        private void OnEditConsultationStatus(object p)
        {
            IsOpenEditStatusMenuElement = true;
        }

        private bool _isOpenEditStatusMenuElement;
        public bool IsOpenEditStatusMenuElement
        {
            get => _isOpenEditStatusMenuElement;
            set => Set(ref _isOpenEditStatusMenuElement, value);
        }

        public List<ConsultationStatus> ConsultationStatuses 
        {
            get => Consultation.Statuses;
        }


        private DateTime _firstDate;
        public DateTime FirstDate 
        {
            get => _firstDate;
            set
            {
                Set(ref _firstDate, value);
                FilteredConsultations = new(ConsultationsFilter.FilterByDate(Consultations, LastDate, FirstDate));
            }
        }

        private DateTime _lastDate;
        public DateTime LastDate
        {
            get => _lastDate;
            set
            {
                Set(ref _lastDate, value);
                FilteredConsultations = new (ConsultationsFilter.FilterByDate(Consultations, LastDate, FirstDate));
            }
        }

        private bool CanAnyWay(object p) => true;

        public ICommand SetDateDiapasone { get; }
        private void OnSetDateDiapasone(object p)
        {
            LastDate = DateTime.Now;
            FirstDate = DateTime.Now.AddDays(-Convert.ToDouble(p));
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