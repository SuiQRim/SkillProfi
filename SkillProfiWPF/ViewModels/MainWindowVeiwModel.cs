using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using SkillProfiWPF.ViewModels.Prefab;
using SkillProfiWPF.Views;
using SkillProfiWPF.Views.Contacts;

namespace SkillProfiWPF.ViewModels
{
    internal class MainWindowVeiwModel : ViewModel
    {
        public MainWindowVeiwModel()
        {
            OpenConsultationUC = new LamdaCommand(OnOpenConsultationUC, CanOpenConsultationUC);
            OpenProjectsUC = new LamdaCommand(OnOpenProjectsUC, CanOpenProjectsUC);
            OpenServicesUC = new LamdaCommand(OnOpenServicesUC, CanOpenServicesUC);
            OpenBlogsUC = new LamdaCommand(OnOpenBlogsUC, CanOpenBlogsUC);
            OpenContactsUC = new LamdaCommand(OnOpenContactsUC, CanOpenContactsUC);
        }

        private UserControl _page;
        public UserControl Page
        {
            get => _page;
            set => Set(ref _page, value);
        }
  
        private bool CanOpenConsultationUC(object p) => !(Page != null && Page is ConsultationsUserControl);
        public ICommand OpenConsultationUC { get; }
        private void OnOpenConsultationUC(object p)
        {
            Page = new ConsultationsUserControl();
        }

        private bool CanOpenProjectsUC(object p) => !(Page != null && Page is ProjectsUserControl);
        public ICommand OpenProjectsUC{ get; }
        private void OnOpenProjectsUC(object p)
        {
            Page = new ProjectsUserControl();
        }

        private bool CanOpenServicesUC(object p) => !(Page != null && Page is ServicesUserControl);
        public ICommand OpenServicesUC { get; }
        private void OnOpenServicesUC(object p)
        {
            Page = new ServicesUserControl();
        }

        private bool CanOpenBlogsUC(object p) => !(Page != null && Page is BlogsUserControl);
        public ICommand OpenBlogsUC { get; }
        private void OnOpenBlogsUC(object p)
        {
            Page = new BlogsUserControl();
        }

        private bool CanOpenContactsUC(object p) => !(Page != null && Page is ContactsUserControl);
        public ICommand OpenContactsUC { get; }
        private void OnOpenContactsUC(object p)
        {
            Page = new ContactsUserControl();
        }
    }
}
