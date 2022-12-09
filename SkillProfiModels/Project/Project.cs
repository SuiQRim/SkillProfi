using System.ComponentModel.DataAnnotations;

namespace SkillProfi.Project
{
    public class Project : IPicture
    {
		[Required]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(48)]
		public string Title { get; set; }

		[Required]
		[MaxLength(4096)]
		public string Description { get; set; }

		[Required]
		public string PictureName { get; set; }

		[Required]
		public DateTime Created { get; set; }
    }
}
