using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DreamJob.BusinessLogic.JobOffer.ViewModels
{
    public class CreateJobOfferViewModel
    {
        public int EmployerId { get; set; }

        public decimal Salary { get; set; }

        public string JobDescription { get; set; } = null!;
        public List<SelectListItem> SelectedSkills { get; set; } = new List<SelectListItem>();

        public List<int> SkillIds { get; set; } = new List<int>();

        public string Username { get; set; } = null!;   
    }
}
