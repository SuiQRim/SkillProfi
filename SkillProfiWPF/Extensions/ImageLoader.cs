using SkillProfi;
using SkillProfiRequestsToAPI;
using System.Collections.Generic;

namespace SkillProfiWPF.Extensions
{
    internal static class ImageExtensions
    {
        public static List<T> LoadImage<T>(this List<T> pic, SkillProfiWebClient wc) where T : IPicture
        {
            pic.ForEach(p => p.PictureBytePresentation = wc.Pictures.GetImage(p.PictureName));
            return pic;
        }
    }
}
