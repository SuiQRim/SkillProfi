using SkillProfi;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

namespace SkillProfiApi.Data
{
	internal static class PictureDirectory
	{
		private const string REPOSITORY = "Pictures";

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

        public static async Task GetPictureAsync(this IPicture picture) {

			string path = Path.Combine(REPOSITORY, picture.PictureName);
            picture.PictureBytePresentation = await File.ReadAllBytesAsync(path);
		}

		private static void SetOriginalName(this IPicture picture) 
		{
			picture.PictureName = Guid.NewGuid().ToString();

			if (File.Exists(Path.Combine(REPOSITORY, picture.PictureName)))
			{
				picture.SetOriginalName();
			}	
		}

		public static async Task SavePictureAsync(this IPicture picture)
		{
			if (picture.PictureBytePresentation == null)
			{
				throw new ArgumentNullException(nameof(picture.PictureBytePresentation), "Byte presentation of picture is null");
			}

			picture.SetOriginalName();

			string path = Path.Combine(REPOSITORY, picture.PictureName);

			await File.WriteAllBytesAsync(path, picture.PictureBytePresentation);
		}

		public static void RemovePicture(this IPicture picture)
		{
            string path = Path.Combine(REPOSITORY, picture.PictureName);
			File.Delete(path);
        }

		public static void CreateFolder() 
		{
			if (Directory.Exists("Pictures")) return;

			Directory.CreateDirectory(REPOSITORY);
		}

	}
}
