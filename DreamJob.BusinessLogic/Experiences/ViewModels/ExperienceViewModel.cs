using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Experiences.ViewModels
{
    public class ExperienceViewModel
    {
        public string ExperienceName { get; set; } = null!;

        public DateTime StartYear { get; set; }

        public DateTime EndYear { get; set; }

        public string ExperienceDescription { get; set; } = null!;
    }
}
