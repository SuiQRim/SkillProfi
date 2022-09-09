using System.ComponentModel.DataAnnotations;

namespace SkillProfi
{
    public class Project
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
