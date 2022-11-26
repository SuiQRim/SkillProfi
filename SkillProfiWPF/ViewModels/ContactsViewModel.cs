using SkillProfiWPF.ViewModels.Prefab;
using SkillProfiWPF.Views.Contacts;
using System.Windows.Controls;

namespace SkillProfiWPF.ViewModels
{
    internal class ContactsViewModel : ViewModel
    {
        public ContactsViewModel()
        {
            SocialNetworksUC = new SocialNetworksUserControl();
            MainContactsDataUC = new MainContactsDataUserControl();
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
