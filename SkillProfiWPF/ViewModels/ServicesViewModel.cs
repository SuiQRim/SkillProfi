using SkillProfi;
using SkillProfiRequestsToAPI.Services;
using SkillProfiWPF.ViewModels.Prefab;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiWPF.ViewModels
{
    internal class ServicesViewModel : EditorViewModel
    {
        public ServicesViewModel(Func<bool> getLoginStatus) : base(getLoginStatus)
        {
            Services = new (ServicesRequests.GetServices());
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
            ServicesRequests.DeleteService(SelectedService!.Id.ToString());
            Services = new(ServicesRequests.GetServices());
            IsObjectSelect = false;
        }


        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {
            if (!IsAddObject)
            {
                Services = new(ServicesRequests.GetServices());
                SelectedService = Services.First(p => p.Id == _lastSelectedProjectId);
            }

            base.OnReturn(p);
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
                ServicesRequests.AddService(newProject);
                Services = new(ServicesRequests.GetServices());

            }
            else
            {
                SelectedService.Title = Title;
                SelectedService.Description = Description;

                ServicesRequests.EditService(SelectedService.Id.ToString(), SelectedService);
                Services = new(ServicesRequests.GetServices());
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
