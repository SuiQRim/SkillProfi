using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillProfiWPF.ViewModels.Prefab;
using SkillProfi;
using SkillProfiRequestsToAPI.Projects;
using System.Windows.Input;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace SkillProfiWPF.ViewModels
{
    internal class ProjectsViewModel : ViewModel
    {
        public ProjectsViewModel()
        {
            Projects = new(ProjectsRequests.GetProjects());
            EditProject = new LamdaCommand(OnEditProject, CanEditProject);
            SaveProject = new LamdaCommand(OnSaveProject, CanSaveProject);
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);


            ReturnProject = new LamdaCommand(OnReturnProject, CanReturnProject);
        }


        private bool CanEditProject(object p) => !IsProjectEdit;
        public ICommand EditProject { get; }
        private void OnEditProject(object p)
        {
            IsProjectEdit = true;

        }


        private bool CanSelectImage(object p) => true;
        public ICommand SelectImage { get; }
        private void OnSelectImage(object p)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new ();
            
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Файлы рисунков (*.png, *.jpg)|*.png;*.jpg";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Read the contents of the file into a stream

                SelectedProject.PictureBytePresentation = File.ReadAllBytes(openFileDialog.FileName);
                OnPropertyChanged(nameof(SelectedProject));
                OnPropertyChanged(nameof(Projects));
            }


        }

        private bool CanReturnProject(object p) => true;
        public ICommand ReturnProject { get; }
        private void OnReturnProject(object p)
        {
            Projects = new(ProjectsRequests.GetProjects());
            SelectedProject = Projects.First(p => p.Id == _lastSelectedProjectId);
            IsProjectEdit = false;
        }


        private bool CanSaveProject(object p) => IsProjectEdit;
        public ICommand SaveProject { get; }
        private void OnSaveProject(object p)
        {
            SelectedProject.Title = Title;
            SelectedProject.Description = Description;
            ProjectsRequests.EditProject(SelectedProject.Id.ToString(), SelectedProject);
            Projects = new(ProjectsRequests.GetProjects());
            SelectedProject = Projects.First(p => p.Id == _lastSelectedProjectId);


            IsProjectEdit = false;
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

        private bool _isProjectEdit = false;
        public bool IsProjectEdit
        {
            get => _isProjectEdit;
            set =>  Set(ref _isProjectEdit, value);

        }

        private Guid _lastSelectedProjectId;

        private Project _selectedProject;
        public Project SelectedProject
        {
            get => _selectedProject;
            set {

                if (value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    _lastSelectedProjectId = value.Id;
                }
                IsProjectEdit = false;
                Set(ref _selectedProject, value);
            } 
        }

        private ObservableCollection<Project> _projects;
        public ObservableCollection<Project> Projects
        {
            get => _projects;
            set => Set(ref _projects, value);
        }

    }
}
