namespace SkillProfiApi.Data.Picture
{
    public class PictureNullException : ArgumentNullException
    {
        public PictureNullException(string nameOf) : base(nameOf, "Byte presentation of picture is null")
        {

        }
    }
}
