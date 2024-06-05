using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.JobOffers.ViewModels
{
    public class JobOfferViewModel
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }

        public string Description { get; set; } = null!;

        public string Employer { get; set; } = null!;
        public string OfficeLocation { get; set; } = null!;
        public string EmployerLinkedin { get; set; } = null!;

    }
}
