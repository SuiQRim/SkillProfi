using System.ComponentModel.DataAnnotations;

namespace SkillProfi
{
    public class Account
    {
        [Required]
        [MaxLength(24, ErrorMessage = "Login name is too long - max length 24")]
        [MinLength(4, ErrorMessage = "Login name is too short - min length 4")]
        public string Login { get; set; }

        [Required]
		[MaxLength(24, ErrorMessage = "Password name is too long - max length 24")]
		[MinLength(5, ErrorMessage = "Password name is too short - min length 5")]
        [DataType(DataType.Password)]
		public string Password { get; set; }
    }
}
