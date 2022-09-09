using SkillProfi;
using System.Drawing;
using System.IO;

namespace SkillProfiApi.Data
{
	public class PictureDirectory
	{
		private const string REPOSITORY = "Pictures";
		private const string FORMAT = ".jpg";

		public PictureDirectory()
		{
			CreateFolder();
		}

        public List<byte[]> GetImagesByte {

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

		public async Task<byte[]> GetPictureByNameAcync(string Name) {

			string path = Path.Combine(REPOSITORY, Name + FORMAT);
            return await File.ReadAllBytesAsync(path);
		}

		private void SetOriginalName() { }

		public void SavePicture(byte[] imageByte) 
		{
		
		}

		public void RemovePicture(string name)
		{
		
		}

		public void CreateFolder() 
		{
			if (Directory.Exists("Pictures")) return;

			Directory.CreateDirectory(REPOSITORY);
		}

		public void ClearDirectory() { }

	}
}
