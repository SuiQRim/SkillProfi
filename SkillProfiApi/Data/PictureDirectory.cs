using SkillProfi;

namespace SkillProfiApi.Data
{
	internal static class PictureDirectory
	{
		private const string REPOSITORY = "Pictures";

		private static string PathToPicture(string pictureName) =>
			Path.Combine(REPOSITORY, pictureName);

		public static async Task<byte[]?> GetPictureAsync(Guid guid)
		{
			string path = PathToPicture(guid.ToString());
			if (!File.Exists(path)) return null;

			return await File.ReadAllBytesAsync(path);
		}

		private static string SetOriginalName(this IPicture pic) 
		{
			pic.PictureName = Guid.NewGuid().ToString();

			if (File.Exists(PathToPicture(pic.PictureName)))
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

			string path = PathToPicture(objImg.Object.PictureName);

			await File.WriteAllBytesAsync(path, objImg.Picture);
		}

		public static void RemovePicture(this IPicture picture)
		{
            string path = PathToPicture(picture.PictureName);

			if (File.Exists(path))
				File.Delete(path);
        }
	}
}
