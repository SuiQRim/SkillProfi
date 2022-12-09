﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using SkillProfi.Contacts;
using SkillProfiRequestsToAPI;
using SkillProfiWPF.ViewModels.Prefab;

namespace SkillProfiWPF.ViewModels
{
    internal class SocialNetworksViewModel : EditorViewModel
    {
        private readonly SkillProfiWebClient _spClient = new();

        public SocialNetworksViewModel()
        {
            SocialNetworks = new(GetProjectsWithImage());
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);
        }

        private List<SocialNetwork> GetProjectsWithImage() =>
            Task.Run(async () => await _spClient.SocialNetworks.GetListAsync()).Result;

        protected override bool CanAdd(object p) => base.CanAdd(p);
        protected override void OnAdd(object p)
        {
            base.OnAdd(p);

            Link = "Link";
            PictureName = null;
        }


        protected override bool CanDelete(object p) => base.CanDelete(p);
        protected override void OnDelete(object p)
        {
            _spClient.SocialNetworks.DeleteById(SelectedSocialNetwork!.Id.ToString(), AccessToken);
            SocialNetworks = new(GetProjectsWithImage());
            SelectedSocialNetwork = null;
            IsObjectSelect = false;
            _lastSelectedSocialNetworkId = Guid.Empty;

		}

        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {
            base.OnReturn(p);

            if (!IsAddObject)
            {
                SocialNetworks = new(GetProjectsWithImage());
                if (_lastSelectedSocialNetworkId != Guid.Empty)
                {
                    SelectedSocialNetwork = SocialNetworks.First(p => p.Id == _lastSelectedSocialNetworkId);
                }

            }
        }


        protected override bool CanSave(object p)
        {
			if (IsAddObject && File.Exists(PictureName) && Link != string.Empty )
				return true;

			else if (!IsAddObject && Link != string.Empty )
				return true;

			return false;
		}

        protected override void OnSave(object p)
        {
			FileStream? fstream = File.Exists(PictureName) ? File.OpenRead(PictureName) : null;
            SocialNetworkTransfer newSocialNetwork = new()
            {
                Link = Link,
            };

            if (IsAddObject)
            {

                _spClient.SocialNetworks.Add(newSocialNetwork, fstream, AccessToken);
                SocialNetworks = new(GetProjectsWithImage());

            }
            else
            {
                _spClient.SocialNetworks.Edit(SelectedSocialNetwork.Id.ToString(), newSocialNetwork, fstream, AccessToken);
                SocialNetworks = new(GetProjectsWithImage());
                SelectedSocialNetwork = SocialNetworks.First(p => p.Id == _lastSelectedSocialNetworkId);
            }

            IsObjectEdit = false;
        }

        private bool CanSelectImage(object p) => true;
        public ICommand SelectImage { get; }
        private void OnSelectImage(object p)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
                PictureName = openFileDialog.FileName;

        }

        private string _link = "";
        public string Link
        {
            get => _link;
            set => Set(ref _link, value);

        }

		private string _pictureName = "";
		public string PictureName
		{
			get => _pictureName;
			set => Set(ref _pictureName, value);

		}

		private Guid _lastSelectedSocialNetworkId;

        private ObservableCollection<SocialNetwork> _socialNetworks;
        public ObservableCollection<SocialNetwork> SocialNetworks
        {
            get => _socialNetworks;
            set => Set(ref _socialNetworks, value);
        }

        private SocialNetwork? _selectedSocialNetwork;
        public SocialNetwork? SelectedSocialNetwork
        {
            get => _selectedSocialNetwork;
            set
            {
                if (value != null)
                {
                    Link = value.Link;
                    IsObjectSelect = true;
                    _lastSelectedSocialNetworkId = value.Id;
                    PictureName = value.PictureName;
                }
                else IsObjectSelect = false;

                IsAddObject = false;
                IsObjectEdit = false;

                Set(ref _selectedSocialNetwork, value);
            }
        }
    }
}
