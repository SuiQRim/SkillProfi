using SkillProfi;

namespace SkillProfiApi.Data
{
	internal static class PictureDirectory
	{
		private const string REPOSITORY = "Pictures";

		public static async Task<byte[]?> GetPictureAsync(Guid guid)
		{
			string path = Path.Combine(REPOSITORY, guid.ToString());
			if (!File.Exists(path)) return null;

			return await File.ReadAllBytesAsync(path);
		}

		private static string SetOriginalName(this IPicture pic) 
		{
			pic.PictureName = Guid.NewGuid().ToString();

			if (File.Exists(Path.Combine(REPOSITORY, pic.PictureName)))
			{
				pic.SetOriginalName();
			}
			return pic.PictureName;
		}

		public static async Task SavePictureAsync<T>(this ObjectWithImage<T> objImg)
			where T : IPicture
		{
			if (objImg == null && objImg?.Picture == null)
			{
				throw new ArgumentNullException(nameof(objImg), "Byte presentation of picture is null");
			}

			objImg.Object.SetOriginalName();

			string path = Path.Combine(REPOSITORY, objImg.Object.PictureName);

			await File.WriteAllBytesAsync(path, objImg.Picture);
		}

		public static void RemovePicture(this IPicture picture)
		{
            string path = Path.Combine(REPOSITORY, picture.PictureName);
			File.Delete(path);
        }

		public static void Configurate() 
		{
			if (Directory.Exists("Pictures")) return;

			Directory.CreateDirectory(REPOSITORY);
		}

	}
}
