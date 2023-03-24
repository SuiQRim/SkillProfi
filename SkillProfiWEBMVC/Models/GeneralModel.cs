using SkillProfi;

namespace SkillProfiWEBMVC.Models
{
    public class GeneralModel<T>
    {
        public GeneralModel()
        {

        }
        public T Object { get; set; }

        public string? ImageLink { get; set; }

    }
}
