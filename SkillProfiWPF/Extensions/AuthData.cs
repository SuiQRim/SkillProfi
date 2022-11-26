using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiWPF.Extensions
{
    internal static class AuthData
    {
        public static string? AccessToken { get; set; }

        public static string? Login { get; set; }

        public static bool IsLogin { get; set; }

    }
}
