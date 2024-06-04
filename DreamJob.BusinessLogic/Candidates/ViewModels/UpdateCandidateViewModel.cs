using DreamJob.BusinessLogic.Experiences.ViewModels;
using DreamJob.BusinessLogic.Studies.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Candidates.ViewModels
{
    public class UpdateCandidateViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string CandidateDescription { get; set; } = null!;
        public string Linkedin { get; set; } = null!;

        //public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        //public List<int> SelectedSkillIds { get; set; } = new List<int>();

        public List<StudyViewModel> Studies { get; set; } = new List<StudyViewModel>();
        public List<ExperienceViewModel> Experiences { get; set; } = new List<ExperienceViewModel>();



    }
}
