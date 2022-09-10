using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfi
{
    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        public Guid PictureId { get; set; }
        public Picture Picture { get; set; }

        public DateTime Created { get; set; }


    }
}
