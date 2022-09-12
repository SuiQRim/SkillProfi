using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillProfi
{
    public interface IPicture
    {
        string PictureName { get; set; }

        byte[]? PictureBytePresentation { get; set; }
    }
}
