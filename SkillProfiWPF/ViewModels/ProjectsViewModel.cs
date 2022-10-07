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
            AddProject = new LamdaCommand(OnAddProject, CanAddProject);
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);


            ReturnProject = new LamdaCommand(OnReturnProject, CanReturnProject);
        }


        private bool _isAddProject = false;
        public bool IsAddProject
        {
            get => _isAddProject;
            set => Set(ref _isAddProject, value);
        }

        private bool CanAddProject(object p)
        {
            if (IsAddProject || IsProjectEdit)
            {
                return false;
            }
            return true;
        } 
        public ICommand AddProject { get; }
        private void OnAddProject(object p)
        {
            Title = "Title";
            Description = "Description";
            PictureBytePresentation = Array.Empty<byte>();
            IsProjectEdit = true;
            IsAddProject = true; 
            IsProjectSelect = true;

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
            OpenFileDialog openFileDialog = new ();
            openFileDialog.Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                PictureBytePresentation = File.ReadAllBytes(openFileDialog.FileName);
            
        }

        private bool CanReturnProject(object p) => true;
        public ICommand ReturnProject { get; }
        private void OnReturnProject(object p)
        { 
            if (!IsAddProject)
            {
                Projects = new(ProjectsRequests.GetProjects());
                SelectedProject = Projects.First(p => p.Id == _lastSelectedProjectId);
            }
            IsAddProject = false;
            IsProjectEdit = false;
            IsProjectSelect = false;
        }


        private bool CanSaveProject(object p)
        {
            if (IsAddProject)
            {
                if (PictureBytePresentation == Array.Empty<byte>() || Title == string.Empty || Description == string.Empty)
                {
                    return false;
                }
                return true;
            }
            return true;
        }
        public ICommand SaveProject { get; }
        private void OnSaveProject(object p)
        {
            if (IsAddProject)
            {
                Project newProject = new()
                {
                    Title = Title,
                    PictureName = "SomePictureName",
                    Description = Description,
                    PictureBytePresentation = PictureBytePresentation,
                };
                ProjectsRequests.AddProject(newProject);
                Projects = new(ProjectsRequests.GetProjects());

            }
            else
            {
                SelectedProject.Title = Title;
                SelectedProject.Description = Description;
                SelectedProject.PictureBytePresentation = PictureBytePresentation;

                ProjectsRequests.EditProject(SelectedProject.Id.ToString(), SelectedProject);
                Projects = new(ProjectsRequests.GetProjects());
                SelectedProject = Projects.First(p => p.Id == _lastSelectedProjectId);
            }

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


        private byte[] _pictureBytePresentation;
        public byte[] PictureBytePresentation
        {
            get => _pictureBytePresentation;
            set => Set(ref _pictureBytePresentation, value);

        }


        private bool _isProjectEdit = false;
        public bool IsProjectEdit
        {
            get => _isProjectEdit;
            set =>  Set(ref _isProjectEdit, value);

        }

        private Guid _lastSelectedProjectId;


        private bool _isProjectSelect = false;
        public bool IsProjectSelect
        {
            get => _isProjectSelect;
            set => Set(ref _isProjectSelect, value);

        }


        private Project _selectedProject;
        public Project SelectedProject
        {
            get => _selectedProject;
            set {

                if (value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    PictureBytePresentation = value.PictureBytePresentation;
                    _lastSelectedProjectId = value.Id;
                    IsProjectSelect = true;
                }
                else IsProjectSelect = false;

                IsAddProject = false;
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
