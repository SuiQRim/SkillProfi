using SkillProfiRequestsToAPI.Accounts;
using SkillProfiWPF.ViewModels.Prefab;
using SkillProfiWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SkillProfiWPF.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        public LoginViewModel()
        {
            JoinWithLogin = new LamdaCommand(OnJoinWithLogin, CanAnyWay);
            JoinAsGuest = new LamdaCommand(OnJoinAsGuest, CanAnyWay);
        }

        public bool IsLogined { get; private set; }


        private string _name = "";
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);

        }


        private string _password = "";
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);

        }


        private string _error = "";
        public string Error
        {
            get => _error;
            set
            {
                Set(ref _error, value);
                OnPropertyChanged("ErrorExcist");
            }
        }

        public bool ErrorExcist
        {
            get => !(Error == string.Empty);
            
        }

        private bool CanAnyWay(object p) => true;
        public ICommand JoinWithLogin { get; }
        private void OnJoinWithLogin(object window)
        {
            if (window != null)
            {
                if (Name == string.Empty || Password == string.Empty)
                {
                    Error = "Fields Cannot be Empty";
                    return;
                }

                IsLogined = AccountsRequests.Login(new SkillProfi.Account() { Name= Name, Password= Password });

                if (!IsLogined)
                {
                    Error = "Name or Password is Wrong!";
                    return;
                }

                (window as Window).DialogResult = true;
                (window as Window).Close();
            }
        }

        public ICommand JoinAsGuest { get; }
        private void OnJoinAsGuest(object window)
        {
            if (window != null)
            {
                IsLogined = false;
                (window as Window).DialogResult = true;
                (window as Window).Close();
            }
        }
    }
}
