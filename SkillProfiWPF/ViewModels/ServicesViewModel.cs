using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiRequestsToAPI.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SkillProfiWPF.ViewModels
{
    internal class ServicesViewModel : EditorViewModel
    {
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public ServicesViewModel()
        {
            Services = new (_spClient.Services.GetList());
        }

        protected override bool CanAdd(object p) => true;
        protected override void OnAdd(object p)
        {
            base.OnAdd(p);
            IsObjectSelect = true;
            Title = "Title";
            Description = "Description";
        }


        protected override bool CanDelete(object p)
        {
            return base.CanDelete(p);
        }
        protected override void OnDelete(object p)
        {
            _spClient.Services.DeleteById(SelectedService!.Id.ToString(), AccessToken);
            Services = new(_spClient.Services.GetList());
            IsObjectSelect = false;
        }


        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {
            base.OnReturn(p);

            if (!IsAddObject)
            {
                Services = new(_spClient.Services.GetList());
                SelectedService = Services.First(p => p.Id == _lastSelectedProjectId);
            }
        }


        protected override bool CanSave(object p)
        {
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Description))
            {
                return false;
            }
            return true;
        }


        protected override void OnSave(object p)
        {
            if (IsAddObject)
            {
                Service newProject = new()
                {
                    Title = Title,
                    Description = Description,
                };
                _spClient.Services.Add(newProject, AccessToken);
                Services = new(_spClient.Services.GetList());

            }
            else
            {
                SelectedService.Title = Title;
                SelectedService.Description = Description;

                _spClient.Services.Edit(SelectedService.Id.ToString(), SelectedService, AccessToken);
                Services = new(_spClient.Services.GetList());
                SelectedService = Services.First(p => p.Id == _lastSelectedProjectId);
            }

            IsObjectEdit = false;
        }

        private string _title = "";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);

        }
        private string _description = "";
        public string Description
        {
            get => _description;
            set => Set(ref _description, value);

        }


        private Guid _lastSelectedProjectId;

        private Service _selectedServices;
        public Service SelectedService
        {
            get => _selectedServices;
            set
            {
                if (value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    IsObjectSelect = true;
                    _lastSelectedProjectId = value.Id;
                }
                else IsObjectSelect = false;

                IsAddObject = false;
                IsObjectEdit = false;

                Set(ref _selectedServices, value);
            }
            
        }

        private ObservableCollection<Service> _services;
        public ObservableCollection<Service> Services
        {
            get => _services;
            set => Set(ref _services, value);
        }

    }
}
