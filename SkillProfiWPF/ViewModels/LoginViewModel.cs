﻿using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWPF.ViewModels.Prefab;
using System.Windows;
using System.Windows.Input;

namespace SkillProfiWPF.ViewModels
{
    internal class LoginViewModel : ViewModel
    {
        private readonly SkillProfiWebClient _spClient = new(AppState.ReadServerUrl);

        public LoginViewModel()
        {
            JoinWithLogin = new LamdaCommand(OnJoinWithLogin, CanAnyWay);
            JoinAsGuest = new LamdaCommand(OnJoinAsGuest, CanAnyWay);
        }

        public AuthParameters AuthParams { get; private set; }


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

                AuthParams = _spClient.Accounts.Login(new Account() { Name = Name, Password = Password });

                if (!AuthParams.IsLogin)
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
                AuthParams = new (){IsLogin = false};
                (window as Window).DialogResult = true;
                (window as Window).Close();
            }
        }
    }
}
