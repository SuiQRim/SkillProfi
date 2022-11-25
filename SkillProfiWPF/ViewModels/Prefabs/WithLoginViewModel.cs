using SkillProfiWPF.ViewModels.Prefab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiWPF.ViewModels.Prefabs
{
    internal class WithLoginViewModel : ViewModel
    {
        private Func<bool> _getLoginStatus;

        public bool IsLogin { get => _getLoginStatus(); }
        public WithLoginViewModel(Func<bool> getLoginStatus)
        {
            _getLoginStatus = getLoginStatus;
        }
    }
}
