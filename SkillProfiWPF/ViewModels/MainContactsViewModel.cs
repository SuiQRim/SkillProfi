using SkillProfi;
using System;
using SkillProfiRequestsToAPI.Contacts;
using SkillProfiWPF.ViewModels.Prefab;
using System.Windows.Input;
using System.Windows;
using SkillProfiRequestsToAPI;

namespace SkillProfiWPF.ViewModels
{
    internal class MainContactsViewModel : EditorViewModel
    {
        private readonly SkillProfiWebClient _spClient = new();

        public MainContactsViewModel()
        {
            Contacts = _spClient.Contacts.Get();
            CopyLink = new LamdaCommand(OnCopyLink, CanCopyLink);
        }

        private Contacts _contacts;
        public Contacts Contacts
        {
            get => _contacts;
            set
            {
                IsObjectEdit = false;
                Set(ref _contacts, value);
            }
        }
        protected virtual bool CanCopyLink(object p) => true;
        public ICommand CopyLink { get; }
        protected virtual void OnCopyLink(object p)
        {
            Clipboard.SetText(Contacts.LinkToMapContructor);
        }

        protected override void OnReturn(object p)
        {
            base.OnReturn(p);
            Contacts = _spClient.Contacts.Get();
        }

        protected override bool CanSave(object p)
        {
            var c = Contacts;
            if (string.IsNullOrEmpty(c.Adress) || string.IsNullOrEmpty(c.PhoneNumber) || string.IsNullOrEmpty(c.Email) || string.IsNullOrEmpty(c.LinkToMapContructor))
            {
                return false;
            }
            return true;
        }
        protected override void OnSave(object p)
        {
            _spClient.Contacts.Edit(Contacts, AccessToken);
            Contacts = _spClient.Contacts.Get();
        }


        protected override bool CanAdd(object p)
        {
            throw new NotImplementedException();
        }
        protected override void OnDelete(object p)
        {
            throw new NotImplementedException();
        }
    }
}
