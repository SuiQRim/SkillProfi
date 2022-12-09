using System.ComponentModel.DataAnnotations;

namespace SkillProfi.Service
{
	public class ServiceTransfer
	{
		[Required]
		[MaxLength(48)]
		public string Title { get; set; }

		[Required]
		[MaxLength(4096)]
		public string Description { get; set; }
	}
}
