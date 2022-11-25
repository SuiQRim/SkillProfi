using SkillProfiWPF.ViewModels.Prefab;
using SkillProfiWPF.ViewModels.Prefabs;
using SkillProfiWPF.Views.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SkillProfiWPF.ViewModels
{
    internal class ContactsViewModel : WithLoginViewModel
    {
        public ContactsViewModel(Func<bool> getLoginStatus) : base(getLoginStatus)
        {
            SocialNetworksUC = new SocialNetworksUserControl(getLoginStatus);
            MainContactsDataUC = new MainContactsDataUserControl(getLoginStatus);
        }

        private UserControl _socialNetworksUC;
        public UserControl SocialNetworksUC
        {
            get => _socialNetworksUC;
            set => Set(ref _socialNetworksUC, value);
        }

        private UserControl _mainContactsDataUC;
        public UserControl MainContactsDataUC
        {
            get => _mainContactsDataUC;
            set => Set(ref _mainContactsDataUC, value);
        }
    }
}
