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


namespace SkillProfiWPF.ViewModels
{
    internal class MainWindowVeiwModel : ViewModel
    {
        public MainWindowVeiwModel()
        {
            OpenConsultationUC = new LamdaCommand(OnOpenConsultationUC, CanOpenConsultationUC);
            OpenProjectsUC = new LamdaCommand(OnOpenProjectsUC, CanOpenProjectsUC);
            OpenServicesUC = new LamdaCommand(OnOpenServicesUC, CanOpenServicesUC);
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

    }
}
