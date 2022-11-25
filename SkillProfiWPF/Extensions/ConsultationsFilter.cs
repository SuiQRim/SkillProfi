using SkillProfi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiWPF.Extensions
{
    internal static class ConsultationsFilter
    {
        public static IList<Consultation> FilterByDate(IList<Consultation> consultations,
           DateTime firstDate, DateTime lastDate)
        {

            List<Consultation> filtered = new();

            filtered = consultations.Where(c => c.Created.Date >= lastDate.Date && c.Created.Date <= firstDate.Date).ToList();

            return filtered;
        }
    }
}
