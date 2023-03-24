using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWPF.Models;
using SkillProfiWPF.ViewModels.Prefab;
using System.Net;
using System.Net.Http;
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

        public AuthParameters AuthParams { get; private set; } = new();


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

                try
                {
                    Task.Run(async () =>
                        await UserContext.SPClient.Accounts.LoginAsycnc(new Account() { Login = Name, Password = Password })).Wait();
                }
                catch (System.AggregateException ex) when (ex.InnerException is HttpRequestException)
                {
                    var sc = (ex.InnerException as HttpRequestException).StatusCode;
                    Error = sc switch
                    {
                        HttpStatusCode.BadRequest => "Bad format of some field",
                        HttpStatusCode.NotFound => "Name or Password is Wrong!",
                        _ => "Сonnection error",
                    };
                    return;
                }

                AuthParams.IsLogin = true;
                AuthParams.Login = Name;
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
