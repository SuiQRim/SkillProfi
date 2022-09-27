using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfi
{
    public class SocialNetwork : IPicture
    {
        public string? Link { get; set; }
        public string PictureName { get; set; }

        [NotMapped]
        public byte[]? PictureBytePresentation { get; set; }
    }
}
