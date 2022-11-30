using System.ComponentModel.DataAnnotations;

namespace SkillProfiWEBMVC.Models
{
	public class Consult
	{
		[Required(ErrorMessage = "Имя обязательное поле")]
		[MaxLength(24, ErrorMessage = "Имя слишком длинное")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Почта объязательное поле")]
		[EmailAddress(ErrorMessage = "Почта введена некорректно")]
		public string EMail { get; set; }

		[Required(ErrorMessage = "Описание обязательное поле")]
		[DataType(DataType.MultilineText)]
		[MinLength(6, ErrorMessage = "Если вы не изложите проблему подробно мы не сможем вам помочь")]
		[MaxLength(1024, ErrorMessage = "Макс. символов 1024")]
		public string Description { get; set; }
	}
}
