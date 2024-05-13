using DreamJob.BusinessLogic.JobOffer.ViewModels;
using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities;
using DreamJob.Entities.Entities;

namespace DreamJob.BusinessLogic.JobOffers
{
    public class JobOfferService
    {

        private readonly DreamJobContext _context;
        public JobOfferService( DreamJobContext context)
        {
            _context = context;
        }

        public CreateJobOfferViewModel CreateJobOfferVM()
        {
            var model = new CreateJobOfferViewModel();
            return model;
        }

        public void CreateJobOffer(CreateJobOfferViewModel model)
        {
            var jobOffer = new Entities.Entities.JobOffer
            {
                Salary = model.Salary,
                JobDescription = model.JobDescription,
                //TO DO 
                //EmployerId = CurrentUser.Id
            };

            _context.JobOffers.Add(jobOffer);
            _context.SaveChanges();
        }
    }
}
