using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using SkillProfi;
using SkillProfiRequestsToAPI.Contacts;
using SkillProfiRequestsToAPI.Services;
using SkillProfiWPF.ViewModels.Prefab;

namespace SkillProfiWPF.ViewModels
{
    internal class SocialNetworksViewModel : EditorViewModel
    {
        public SocialNetworksViewModel() : base()
        {
            SocialNetworks = new(SocialNetworksRequests.GetSocialNetworks());
            SelectImage = new LamdaCommand(OnSelectImage, CanSelectImage);
        }

        protected override bool CanAdd(object p) => base.CanAdd(p);
        protected override void OnAdd(object p)
        {
            base.OnAdd(p);

            Link = "Title";
            PictureBytePresentation = Array.Empty<byte>();
        }


        protected override bool CanDelete(object p) => base.CanDelete(p);
        protected override void OnDelete(object p)
        {
            SocialNetworksRequests.DeleteSocialNetwork(SelectedSocialNetwork!.Id.ToString());
            SocialNetworks = new(SocialNetworksRequests.GetSocialNetworks());
            IsObjectSelect = false;

        }

        protected override bool CanReturn(object p) => base.CanReturn(p);
        protected override void OnReturn(object p)
        {
            if (!IsAddObject)
            {
                SocialNetworks = new(SocialNetworksRequests.GetSocialNetworks());
                SelectedSocialNetwork = SocialNetworks.First(p => p.Id == _lastSelectedSocialNetworkId);
            }

            base.OnReturn(p);
        }


        protected override bool CanSave(object p)
        {
            if (PictureBytePresentation == Array.Empty<byte>() || PictureBytePresentation == null || Link == string.Empty)
            {
                return false;
            }
            return true;
        }

        protected override void OnSave(object p)
        {
            if (IsAddObject)
            {
                SocialNetwork newSocialNetwork = new()
                {
                    Link = Link,
                    PictureName = "SomePictureName",
                    PictureBytePresentation = PictureBytePresentation,
                };
                SocialNetworksRequests.AddSocialNetwork(newSocialNetwork);
                SocialNetworks = new(SocialNetworksRequests.GetSocialNetworks());

            }
            else
            {
                SelectedSocialNetwork.Link = Link;
                SelectedSocialNetwork.PictureBytePresentation = PictureBytePresentation;

                SocialNetworksRequests.EditSocialNetwork(SelectedSocialNetwork.Id.ToString(), SelectedSocialNetwork);
                SocialNetworks = new(SocialNetworksRequests.GetSocialNetworks());
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
                PictureBytePresentation = File.ReadAllBytes(openFileDialog.FileName);

        }

        private string _link = "";
        public string Link
        {
            get => _link;
            set => Set(ref _link, value);

        }


        private byte[]? _pictureBytePresentation;
        public byte[]? PictureBytePresentation
        {
            get => _pictureBytePresentation;
            set => Set(ref _pictureBytePresentation, value);
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
                    PictureBytePresentation = value.PictureBytePresentation;
                    IsObjectSelect = true;
                    _lastSelectedSocialNetworkId = value.Id;
                }
                else IsObjectSelect = false;

                IsAddObject = false;
                IsObjectEdit = false;

                Set(ref _selectedSocialNetwork, value);
            }
        }
    }
}
