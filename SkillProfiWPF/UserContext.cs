using SkillProfiRequestsToAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiWPF
{
    internal static class UserContext
    {
        public static readonly SkillProfiWebClient SPClient = new ();
    }
}
