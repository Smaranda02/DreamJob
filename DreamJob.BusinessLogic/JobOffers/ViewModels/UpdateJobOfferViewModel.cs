using DreamJob.BusinessLogic.Skills.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.JobOffers.ViewModels {
    public class UpdateJobOfferViewModel {
        public int Id { get; set; }

        public int EmployerId { get; set; }

        public decimal Salary { get; set; }

        public string JobDescription { get; set; } = null!;

        public List<SkillViewModel> JobSkills { get; set; }
    }
}
