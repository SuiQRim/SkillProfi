using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkillProfi
{
    public class SocialNetwork : IPicture
    {
        public Guid Id { get; set; }

        public string? Link { get; set; }

        public string PictureName { get; set; }
    }
}
