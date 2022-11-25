using SkillProfi;
using SkillProfiRequestsToAPI.Blogs;
using SkillProfiRequestsToAPI.Projects;
using SkillProfiWPF.Extensions;
using SkillProfiWPF.ViewModels.Prefab;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace SkillProfiWPF.ViewModels
{
    internal class BlogsViewModel : EditorViewModel
    {
        public BlogsViewModel() : base()
        {
            Blogs = new(GetBlogsWithImage());
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);
        }

        private static List<Blog> GetBlogsWithImage() => 
            Task.Run(async () => await BlogsRequests.GetBlogsAsync()).Result.LoadImage();

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
            BlogsRequests.DeleteBlog(SelectedBlog!.Id.ToString());
            Blogs = new(GetBlogsWithImage());
            IsObjectSelect = false;

        }

        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {
            if (!IsAddObject)
            {
                Blogs = new(GetBlogsWithImage());
                SelectedBlog = Blogs.First(p => p.Id == _lastSelectedBlogId);
            }

            base.OnReturn(p);
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
                Blog newBlog = new()
                {
                    Title = Title,
                    PictureName = "SomePictureName",
                    Description = Description,
                    PictureBytePresentation = PictureBytePresentation,
                };
                BlogsRequests.AddBlog(newBlog);
                Blogs = new(GetBlogsWithImage());

            }
            else
            {
                SelectedBlog.Title = Title;
                SelectedBlog.Description = Description;
                SelectedBlog.PictureBytePresentation = PictureBytePresentation;

                BlogsRequests.EditBlog(SelectedBlog.Id.ToString(), SelectedBlog);
                Blogs = new(GetBlogsWithImage());
                SelectedBlog = Blogs.First(p => p.Id == _lastSelectedBlogId);
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

        private Guid _lastSelectedBlogId;


        private Blog? _selectedBlog;
        public Blog? SelectedBlog
        {
            get => _selectedBlog;
            set
            {

                if (value != null)
                {
                    Title = value.Title;
                    Description = value.Description;
                    PictureBytePresentation = value.PictureBytePresentation;
                    _lastSelectedBlogId = value.Id;
                    IsObjectSelect = true;
                }
                else IsObjectSelect = false;

                IsAddObject = false;
                IsObjectEdit = false;
                Set(ref _selectedBlog, value);
            }
        }

        private ObservableCollection<Blog> _Blogs;
        public ObservableCollection<Blog> Blogs
        {
            get => _Blogs;
            set => Set(ref _Blogs, value);
        }
    }
}
