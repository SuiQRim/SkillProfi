﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SkillProfi
{
    public class Consultation
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EMail { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Status { get; set; }

        public DateTime Created { get; set; }

        public static List<ConsultationStatus> Statuses
        {
            get
            {
                List<ConsultationStatus> a = new();
                foreach (ConsultationStatus i in Enum.GetValues(typeof(ConsultationStatus)))
                    a.Add(i);

                return a;
            }
        }
    }
}
