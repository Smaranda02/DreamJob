using DreamJob.BusinessLogic.Experiences.ViewModels;
using DreamJob.BusinessLogic.JobOffers.ViewModels;
using DreamJob.BusinessLogic.Studies.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Candidates.ViewModels
{
    public class CandidateViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string CandidateDescription { get; set; } = null!;

        public string Linkedin { get; set; } = null!;

        public JobOfferViewModel JobOffer { get; set; }

        public List<StudyViewModel>? Studies { get; set; } = new List<StudyViewModel>();
        public List<ExperienceViewModel>? Experiences { get; set; } = new List<ExperienceViewModel>();
    }
}
