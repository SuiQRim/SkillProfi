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
            OpenConsultationPage = new LamdaCommand(OnOpenConsultationPage, CanAnyWay);
            OpenProjectsUserControl = new LamdaCommand(OnOpenProjectsUserControl, CanAnyWay);
        }

        private UserControl _page;
        public UserControl Page
        {
            get => _page;
            set => Set(ref _page, value);
        } 

        private bool CanAnyWay(object p) => true;


        public ICommand OpenConsultationPage { get; }
        private void OnOpenConsultationPage(object p)
        {
            Page = new ConsultationsUserControl();
        }

        public ICommand OpenProjectsUserControl { get; }
        private void OnOpenProjectsUserControl(object p)
        {
            Page = new ProjectsUserControl();
        }
    }
}
