using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfi
{
    public class Picture
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public byte[] Image { get; set; }

    }
}
