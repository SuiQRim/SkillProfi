using SkillProfiWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkillProfiWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для ProjectsUserControl.xaml
    /// </summary>
    public partial class ProjectsUserControl : UserControl
    {
        public ProjectsUserControl(Func<bool> getLoginStatus)
        {
            InitializeComponent();
            DataContext = new ProjectsViewModel(getLoginStatus);
        }
    }
}
