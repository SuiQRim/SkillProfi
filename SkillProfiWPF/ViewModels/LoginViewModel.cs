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


        protected bool _isObjectSelect = false;
        public bool IsObjectSelect
        {
            get => _isObjectSelect;
            set => Set(ref _isObjectSelect, value);

        }
        private bool CanAnyWay(object p) => true;
        public ICommand JoinWithLogin { get; }
        private void OnJoinWithLogin(object window)
        {
            if (window != null)
            {
                IsLogined = true;
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
