using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillProfiRequestsToAPI.Contacts;
using System.Threading.Tasks;
using SkillProfiWPF.ViewModels.Prefab;
using System.Windows.Input;
using System.Windows;

namespace SkillProfiWPF.ViewModels
{
    internal class MainContactsViewModel : EditorViewModel
    {
        public MainContactsViewModel(Func<bool> getLoginStatus) : base(getLoginStatus)
        {
            Contacts = ContactsRequests.GetContacts();
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
            Contacts = ContactsRequests.GetContacts();
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
            ContactsRequests.EditContacts(Contacts);
            Contacts = ContactsRequests.GetContacts();
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
