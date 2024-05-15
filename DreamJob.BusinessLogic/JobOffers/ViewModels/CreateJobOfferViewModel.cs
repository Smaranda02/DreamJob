using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.JobOffer.ViewModels
{
    public class CreateJobOfferViewModel
    {
        public int EmployerId { get; set; }

        public decimal Salary { get; set; }

        public string JobDescription { get; set; } = null!;

        public string Location { get; set; }
        public string JobTitle { get; set; }
        public string JobIndustry { get; set; }
    }
}
