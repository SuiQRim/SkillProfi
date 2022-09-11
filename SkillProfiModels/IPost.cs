using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfi
{
    public interface IPost
    {
        [Required]
        string Title { get; set; }
        [Required]
        string Description { get; set; }

        DateTime Created { get; set; } 
    }
}
