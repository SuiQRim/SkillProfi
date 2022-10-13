using SkillProfiWPF.ViewModels.Prefab;
using SkillProfiWPF.Views.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SkillProfiWPF.ViewModels
{
    internal class ContactsViewModel : ViewModel
    {
        public ContactsViewModel()
        {
        }

        private UserControl _socialNetworksUC = new SocialNetworksUserControl();
        public UserControl SocialNetworksUC
        {
            get => _socialNetworksUC;
            set => Set(ref _socialNetworksUC, value);
        }

        private UserControl _mainContactsDataUC = new MainContactsDataUserControl();
        public UserControl MainContactsDataUC
        {
            get => _mainContactsDataUC;
            set => Set(ref _mainContactsDataUC, value);
        }
    }
}
