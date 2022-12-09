using SkillProfi;

namespace SkillProfiApi.Data.Picture
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

        public static async Task SavePictureAsync(IPicture pic, byte[]? picture)
        {
            if (picture == null)
            {
                if (string.IsNullOrEmpty(pic.PictureName) || !pic.PictureExsist())
                    throw new PictureNullException(nameof(pic));

                else if (pic.PictureExsist())
                    return;                
            }

            if (!string.IsNullOrEmpty(pic.PictureName) && pic.PictureExsist())
                RemovePicture(pic);
            
            pic.SetOriginalName();
            string path = PathToPicture(pic.PictureName);
            await File.WriteAllBytesAsync(path, picture);
        }


		public static void RemovePicture(this IPicture picture)
        {
            string path = PathToPicture(picture.PictureName);

            if (!File.Exists(path)) 
                throw new PictureNotFound(picture.PictureName);

            File.Delete(path);
        }

        private static bool PictureExsist(this IPicture pic) 
        {
            return File.Exists(PathToPicture(pic.PictureName));
        }
    }
}
