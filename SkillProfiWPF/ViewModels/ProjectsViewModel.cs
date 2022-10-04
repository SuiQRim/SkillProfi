using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillProfiWPF.ViewModels.Prefab;
using SkillProfi;
using SkillProfiRequestsToAPI.Projects;

namespace SkillProfiWPF.ViewModels
{
    internal class ProjectsViewModel : ViewModel
    {
        public ProjectsViewModel()
        {
            Projects = new (ProjectsRequests.GetProjects());
        }

        private Project _selectedProject;

        public Project SelectedProject
        {
            get => _selectedProject;
            set => Set(ref _selectedProject, value);
        }

        private ObservableCollection<Project> _projects;

        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set => Set(ref _projects, value);
        }

    }
}
