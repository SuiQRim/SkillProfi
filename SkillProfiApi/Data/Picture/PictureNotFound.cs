namespace SkillProfiApi.Data.Picture
{
    public class PictureNotFound : FileNotFoundException
    {
        public PictureNotFound(string pictureName) : base("Picture is not found", pictureName)
        {

        }

    }
}
