using Newtonsoft.Json.Linq;

namespace SkillProfiTelegramBot
{
	internal class File
	{

		private const string SETTINGS_FILE_NAME = "appsettings.json";

		private const string TOKEN_FIELD = "token";

		public static string ReadToken()
		{
			if (!System.IO.File.Exists(SETTINGS_FILE_NAME))
			{
				throw new FileNotFoundException($"Для работы бота должен быть файл {SETTINGS_FILE_NAME}, с параметром '{TOKEN_FIELD}'");
			}
			string text = System.IO.File.ReadAllText(SETTINGS_FILE_NAME);

			JObject configJs = JObject.Parse(text);

			string serverUrl = configJs[TOKEN_FIELD].Value<string>() ?? throw new Exception("Не получилось прочитать токен бота");

			return serverUrl;
		}

	}
}
