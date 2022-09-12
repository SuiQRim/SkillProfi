using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfi
{
    public class Blog : IPost, IPicture
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string PictureName { get; set; }

        [NotMapped]
        public byte[]? PictureBytePresentation { get; set; }

        public DateTime Created { get; set; }
    }
}
