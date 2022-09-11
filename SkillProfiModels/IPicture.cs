using System.ComponentModel.DataAnnotations;

namespace SkillProfi
{
    public interface IPicture
    {
        [Required]
        string PictureName { get; set; }
        byte[]? PictureBytePresentation { get; set; }
    }
}
