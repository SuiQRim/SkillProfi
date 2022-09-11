using SkillProfi;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace SkillProfiApi.Data
{
	internal static class PictureDirectory
	{
		private const string REPOSITORY = "Pictures";
		private const string FORMAT = ".jpg";

        public static List<byte[]> GetImagesByte {

			get
			{
				List<byte[]> images = new ();
				foreach (var r in Directory.GetFiles(REPOSITORY))
				{
                    images.Add(File.ReadAllBytes(r));
				}
				return images;
			}

		}

		public static async void GetPictureByNameAcync(this IPicture picture) {

			string path = Path.Combine(REPOSITORY, picture.PictureName + FORMAT);
            picture.PictureBytePresentation = await File.ReadAllBytesAsync(path);
		}

		private static void SetOriginalName() { }

		public static async Task SavePictureAsync(this IPicture picture)
		{
            string path = Path.Combine(REPOSITORY, picture.PictureName + FORMAT);
			await File.WriteAllBytesAsync(path, picture.PictureBytePresentation);
		}

		public static void RemovePicture(this IPicture picture)
		{
            string path = Path.Combine(REPOSITORY, picture.PictureName + FORMAT);
			File.Delete(path);
        }

		public static void CreateFolder() 
		{
			if (Directory.Exists("Pictures")) return;

			Directory.CreateDirectory(REPOSITORY);
		}

		public static void ClearDirectory() { }

	}
}
