using Newtonsoft.Json;
using NuGet.Protocol.Plugins;
using SkillProfi;

namespace SkillProfiApi.Data
{
	public static class FaceFile
	{
		private const string REPOSITORY = "DataFiles";

		private const string FILE_FULL_NAME = "face.json";

		private static string CreatePath() => Path.Combine(REPOSITORY, FILE_FULL_NAME);

		public static async Task<Face> GetAsync() {

			Face? contacts;
			string path = CreatePath();
			using (StreamReader r = new(path))
			{
				string json = await r.ReadToEndAsync();
				contacts = JsonConvert.DeserializeObject<Face>(json);
			}
			return contacts;
		}

		public static void Save(Face face) {
			string contactsJson = JsonConvert.SerializeObject(face);
			File.WriteAllText(CreatePath(), contactsJson);
		}
	}
}
