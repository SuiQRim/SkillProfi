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
            OpenConsultationPage = new LamdaCommand(OnOpenConsultationPage, CanIfConsultationPageNotSelected);
            OpenProjectsUserControl = new LamdaCommand(OnOpenProjectsUserControl, CanIfProjectsUserControlNotSelected);
        }

        private UserControl _page;
        public UserControl Page
        {
            get => _page;
            set => Set(ref _page, value);
        } 

        private bool CanAnyWay(object p) => true;


        private bool CanIfConsultationPageNotSelected(object p) => !(Page != null && Page.GetType() == typeof(ConsultationsUserControl));
        public ICommand OpenConsultationPage { get; }
        private void OnOpenConsultationPage(object p)
        {
            Page = new ConsultationsUserControl();
        }

        private bool CanIfProjectsUserControlNotSelected(object p) => !(Page != null && Page.GetType() == typeof(ProjectsUserControl));
        public ICommand OpenProjectsUserControl { get; }
        private void OnOpenProjectsUserControl(object p)
        {
            Page = new ProjectsUserControl();
        }
    }
}
