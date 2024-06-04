using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Studies.ViewModels
{
    public class StudyViewModel
    {
        public int? Id { get; set; }
        public string University { get; set; } = null!;

        public string Specialty { get; set; } = null!;

        public DateTime StartYear { get; set; }

        public DateTime EndYear { get; set; }

    }
}
