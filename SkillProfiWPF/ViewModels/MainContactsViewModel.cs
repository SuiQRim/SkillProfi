using System;
using SkillProfiRequestsToAPI.Contacts;
using SkillProfiWPF.ViewModels.Prefab;
using System.Windows.Input;
using System.Windows;
using SkillProfiRequestsToAPI;
using SkillProfi.Contacts;
using System.Threading.Tasks;
using System.Drawing;

namespace SkillProfiWPF.ViewModels
{
    internal class MainContactsViewModel : EditorViewModel
    {
        public MainContactsViewModel()
        {
            Contacts = Task.Run(async () =>
                await UserContext.SPClient.Contacts.GetAsync()).Result;
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
            Contacts = Task.Run(async () =>
               await UserContext.SPClient.Contacts.GetAsync()).Result;
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
            var c = Contacts;

			ContactsTransfer contTransfer = new()
            {
				Adress = c.Adress,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
				LinkToMapContructor = c.LinkToMapContructor
			};

			Task.Run(async () => await UserContext.SPClient.Contacts.EditAsync(contTransfer));
            Contacts = Task.Run(async () =>
                await UserContext.SPClient.Contacts.GetAsync()).Result;
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
