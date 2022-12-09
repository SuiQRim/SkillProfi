using System.ComponentModel.DataAnnotations;

namespace SkillProfi.Consult
{
    public class Consultation
    {
		[Required]
        public Guid Id { get; set; }

		[Required]
		[MaxLength(24)]
		public string? Name { get; set; }

		[Required]
        [MaxLength(254)]
		public string? EMail { get; set; }

		[Required]
		[MaxLength(1024)]
		public string? Description { get; set; }

		[Required]
		public ConsultationStatus Status { get; set; }

		[Required]
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
