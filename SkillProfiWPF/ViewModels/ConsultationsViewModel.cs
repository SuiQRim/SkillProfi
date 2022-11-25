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
using SkillProfiWPF.Views;
using System.Windows.Input;
using SkillProfiWPF.ViewModels.Prefab;
using SkillProfiRequestsToAPI.Consultations;
using SkillProfiWPF.Extensions;
using SkillProfiWPF.ViewModels.Prefabs;

namespace SkillProfiWPF.ViewModels
{
    internal class ConsultationsViewModel : WithLoginViewModel
    {
        public ConsultationsViewModel(Func<bool> getLoginStatus) : base(getLoginStatus)
        {
            SetDateDiapasone =  new LamdaCommand(OnSetDateDiapasone, CanAnyWay);
            EditConsultationStatus = new LamdaCommand(OnEditConsultationStatus, CanAnyWay);
            SaveConsultationStatus = new LamdaCommand(OnSaveConsultationStatus, CanAnyWay);

            IsOpenEditStatusMenuElement = false;

            UpdateConsultations();
            FirstDate = DateTime.Now;
            LastDate = DateTime.Now;

        }


        public void UpdateConsultations()
        {
            Consultations = new 
                (ConsultationsRequests.GetConsultations() ?? new());

            FilteredConsultations = new 
                (ConsultationsFilter.FilterByDate(Consultations, LastDate, FirstDate));
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
            ConsultationsRequests.EditConsultation(SelectedConsultation.Id.ToString(), SelectedConsultation);
            UpdateConsultations();

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
                FilteredConsultations = new
                    (ConsultationsFilter.FilterByDate(Consultations, LastDate, FirstDate));
            }
        }

        private DateTime _lastDate;
        public DateTime LastDate
        {
            get => _lastDate;
            set
            {
                Set(ref _lastDate, value);
                FilteredConsultations = new 
                    (ConsultationsFilter.FilterByDate(Consultations, LastDate, FirstDate));
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
            set 
            {
                value = new ( value.OrderByDescending(c => c.Created));
                Set(ref _filteredConsultations, value);
                    
            }
        }
    }
}