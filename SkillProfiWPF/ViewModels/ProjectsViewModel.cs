using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SkillProfiWPF.ViewModels.Prefab;
using SkillProfi;
using System.Windows.Input;
using System.IO;
using System.Windows.Forms;
using SkillProfiWPF.Extensions;
using SkillProfiRequestsToAPI;

namespace SkillProfiWPF.ViewModels
{
    internal class ProjectsViewModel : EditorViewModel
    {
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public ProjectsViewModel()
        {
            Projects = new (GetProjectsWithImage());
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);
        }

        private List<Project> GetProjectsWithImage() => 
            Task.Run(async () => await _spClient.Projects.GetListAsync()).Result.LoadImage(_spClient);
      
        
        #region Commands

        #region Main Commands

        protected override bool CanAdd(object p) => base.CanAdd(p);
        protected override void OnAdd(object p)
        {
            base.OnAdd(p);

            Title = "Title";
            Description = "Description";
        }


        protected override bool CanDelete(object p) => base.CanDelete(p);
        protected override void OnDelete(object p)
        {
            _spClient.Projects.DeleteById(SelectedProject!.Id.ToString(), AccessToken);
            Projects = new(GetProjectsWithImage());
            IsObjectSelect = false;

        }

        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {  
            base.OnReturn(p);
            if (!IsAddObject)
            {
                Projects = new(GetProjectsWithImage());
                SelectedProject = Projects.First(p => p.Id == _lastSelectedProjectId);
            }

        }


        protected override bool CanSave(object p)
        {
            if (PictureBytePresentation == Array.Empty<byte>() || PictureBytePresentation == null || Title == string.Empty || Description == string.Empty)
            {
                return false;
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
                _spClient.Projects.Add(newProject, AccessToken);
                Projects = new(GetProjectsWithImage());

            }
            else
            {
                SelectedProject.Title = Title;
                SelectedProject.Description = Description;
                SelectedProject.PictureBytePresentation = PictureBytePresentation;

                _spClient.Projects.Edit(SelectedProject.Id.ToString(), SelectedProject, AccessToken);
                Projects = new(GetProjectsWithImage());
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
