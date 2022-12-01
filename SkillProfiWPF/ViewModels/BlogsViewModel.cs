﻿using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWPF.ViewModels.Prefab;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace SkillProfiWPF.ViewModels
{
    internal class BlogsViewModel : EditorViewModel
    {
        private readonly SkillProfiWebClient _spClient = new();

        public BlogsViewModel()
        {
            Blogs = new(GetBlogsWithImage());
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);
        }

        private List<Blog> GetBlogsWithImage() => 
            Task.Run(async () => await _spClient.Blogs.GetListAsync()).Result;

        #region Commands

        #region Main Commands

        protected override bool CanAdd(object p) => base.CanAdd(p);
        protected override void OnAdd(object p)
        {
            base.OnAdd(p);

            Title = "Title";
            Description = "Description";
            PictureName = null;
        }


        protected override bool CanDelete(object p) => base.CanDelete(p);
        protected override void OnDelete(object p)
        {
            _spClient.Blogs.DeleteById(SelectedBlog!.Id.ToString(), AccessToken);
            Blogs = new(GetBlogsWithImage());
            IsObjectSelect = false;

        }

        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {
            base.OnReturn(p);
            if (!IsAddObject)
            {
                Blogs = new(GetBlogsWithImage());
                SelectedBlog = Blogs.First(p => p.Id == _lastSelectedBlogId);
            }

        }


        protected override bool CanSave(object p)
        {

            if (!File.Exists(PictureName) || Title == string.Empty || Description == string.Empty)
            {
                return false;
            }
            return true;

        }

        protected override void OnSave(object p)
        {
			FileStream fstream = File.OpenRead(PictureName);

			if (IsAddObject)
            {
                Blog newBlog = new()
                {
                    Title = Title,
                    PictureName = "SomePictureName",
                    Description = Description,
                };
                _spClient.Blogs.Add(newBlog, fstream, AccessToken);
                Blogs = new(GetBlogsWithImage());

            }
            else
            {
                SelectedBlog.Title = Title;
                SelectedBlog.Description = Description;

                _spClient.Blogs.Edit(SelectedBlog.Id.ToString(), SelectedBlog, fstream, AccessToken);
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
                PictureName = openFileDialog.FileName;

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

		private string _pictureName = "";
		public string PictureName
		{
			get => _pictureName;
			set => Set(ref _pictureName, value);

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
                    _lastSelectedBlogId = value.Id;
                    PictureName = value.PictureName;
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
