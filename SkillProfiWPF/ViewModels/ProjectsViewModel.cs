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
using System.Threading;

namespace SkillProfiWPF.ViewModels
{
    internal class ProjectsViewModel : EditorViewModel
    {
        public ProjectsViewModel()
        {
            Projects = new(ProjectsRequests.GetProjects());
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);
        }

        #region Commands

        #region Main Commands

        protected override bool CanAdd(object p) => base.CanAdd(p);
        protected override void OnAdd(object p)
        {
            base.OnAdd(p);

            Title = "Title";
            Description = "Description";
            PictureBytePresentation = Array.Empty<byte>();
        }


        protected override bool CanDelete(object p) => base.CanDelete(p);
        protected override void OnDelete(object p)
        {
            ProjectsRequests.DeleteProject(SelectedProject!.Id.ToString());
            Projects = new(ProjectsRequests.GetProjects());
            IsObjectSelect = false;

        }

        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        { 
            if (!IsAddObject)
            {
                Projects = new(ProjectsRequests.GetProjects());
                SelectedProject = Projects.First(p => p.Id == _lastSelectedProjectId);
            }

            base.OnReturn(p);
        }


        protected override bool CanSave(object p)
        {
            if (IsAddObject)
            {
                if (PictureBytePresentation == Array.Empty<byte>() || Title == string.Empty || Description == string.Empty)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        protected override void OnSave(object p)
        {
            if (IsAddObject)
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

            IsObjectEdit = false;
        }

        #endregion

        #region Original Commands
        private bool CanSelectImage(object p) => true;
        public ICommand SelectImage { get; }
        private void OnSelectImage(object p)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                PictureBytePresentation = File.ReadAllBytes(openFileDialog.FileName);

        }

        #endregion

        #endregion

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


        private byte[]? _pictureBytePresentation;
        public byte[]? PictureBytePresentation
        {
            get => _pictureBytePresentation;
            set => Set(ref _pictureBytePresentation, value);

        }

        private Guid _lastSelectedProjectId;


        private Project? _selectedProject;
        public Project? SelectedProject
        {
            get => _selectedProject;
            set {

                if (value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    PictureBytePresentation = value.PictureBytePresentation;
                    _lastSelectedProjectId = value.Id;
                    IsObjectSelect = true;
                }
                else IsObjectSelect = false;

                IsAddObject = false;
                IsObjectEdit = false;
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
