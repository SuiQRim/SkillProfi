using Newtonsoft.Json.Linq;
using System;
using System.IO;


namespace SkillProfiWPF
{
    internal static class AppState
    {
        public static string ReadServerUrl 
        {
            get
            {
                string text = File.ReadAllText("appsettings.json");

                JObject configJs = JObject.Parse(text);

                string serverUrl = configJs["serverUrl"].Value<string>() ?? throw new Exception("Не получилось прочитать Url сервера");

                return serverUrl;
            }
        }
    }
}
