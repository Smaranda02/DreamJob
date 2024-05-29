using DreamJob.BusinessLogic.JobOffer.ViewModels;
using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities;
using DreamJob.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DreamJob.BusinessLogic.JobOffers
{
    public class JobOfferService
    {

        private readonly DreamJobContext _context;
        public JobOfferService(DreamJobContext context)
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
                EmployerId = 1,
            };

            _context.JobOffers.Add(jobOffer);
            _context.SaveChanges();
        }

        public List<Entities.Entities.JobOffer> GetJobOffers()
        {
            var jobOffers = _context.JobOffers.Include(j => j.Employer).ToList();
            return jobOffers;
        }

        public Entities.Entities.JobOffer GetJobOffer(int id) {
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null) {
              //  throw new Exception("Job offer not found");
            }
            return jobOffer;
        }

        public void DeleteJobOffer(int id) {
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null) {
                throw new Exception("Job offer not found");
            }
            _context.JobOffers.Remove(jobOffer);
            _context.SaveChanges();
        }
        
        public void UpdateJobOffer(int id, CreateJobOfferViewModel model) {
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null) {
                throw new Exception("Job offer not found");
            }
            jobOffer.Salary = model.Salary;
            jobOffer.JobDescription = model.JobDescription;
            jobOffer.EmployerId = model.EmployerId;
            //jobOffer.Location = model.Location;
            //jobOffer.JobTitle = model.JobTitle;
            //jobOffer.JobIndustry = model.JobIndustry;
            _context.SaveChanges();
        }


    }
}
