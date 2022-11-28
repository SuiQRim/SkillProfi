using SkillProfi;
using SkillProfiRequestsToAPI.Images;
using System.Collections.Generic;

namespace SkillProfiTelegramBot
{
    internal static class ImageExtensions
    {
        public static List<T> LoadImage<T>(this List<T> pic) where T : IPicture
        {
            foreach (var p in pic)
            {
                p.PictureBytePresentation = ImagesRequests.GetImage(p.PictureName.ToString());
            }
            return pic;
        }
    }
}
