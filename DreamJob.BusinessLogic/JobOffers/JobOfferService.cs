using DreamJob.BusinessLogic.JobOffers.ViewModels;
using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities;
using DreamJob.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using DreamJob.BusinessLogic.Skills;
using DreamJob.BusinessLogic.JobOffers.ViewModels;
using AutoMapper;
using DreamJob.BusinessLogic.Users;

namespace DreamJob.BusinessLogic.JobOffers
{
    public class JobOfferService
    {

        private readonly DreamJobContext _context;
        private readonly SkillsService _skillsService;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        public JobOfferService(DreamJobContext context, SkillsService skillsService, IMapper mapper, UserService userService)
        {
            _context = context;
            _skillsService = skillsService;
            _mapper = mapper;
            _userService = userService;
        }

        public CreateJobOfferViewModel CreateJobOfferVM()
        {

            var skills = _skillsService.GetDefaultSkills();
            var model = new CreateJobOfferViewModel()
            {
                SelectedSkills = skills
            };

            return model;
        }

        public void CreateJobOffer(CreateJobOfferViewModel model)
        {
            // find the employer that has the same name as username from the model
            var employer = _context.Employers.FirstOrDefault(x => x.EmployerName == model.Username);
            var jobOffer = new Entities.Entities.JobOffer
            {
                Salary = model.Salary,
                JobDescription = model.JobDescription,
                EmployerId = employer.Id

            };

            var jobSkills = _skillsService.CreateJobSkill(model.SkillIds, jobOffer);
            _context.JobOffers.Add(jobOffer);
            _context.JobSkills.AddRange(jobSkills);
            _context.SaveChanges();

        }

        public List<JobOfferViewModel> GetMyJobOffers()
        {
            var jobOffers = _context.JobOffers
                            .Include(j => j.Employer)
                            .Where(j => j.EmployerId == _userService.GetCurrentEmployerId())
                            .ToList();
            var jobOfferList = new List<JobOfferViewModel>();

            foreach(var jo in jobOffers)
            {
                var jobOfferViewModel = _mapper.Map<JobOffer, JobOfferViewModel>(jo);
                jobOfferList.Add(jobOfferViewModel);
            }
            return jobOfferList;
        }


        public List<JobOfferViewModel> GetAllJobOffers()
        {
            var jobOffers = _context.JobOffers
                            .Include(j => j.Employer).ToList();
            var jobOfferList = new List<JobOfferViewModel>();

            foreach (var jo in jobOffers)
            {
                var jobOfferViewModel = _mapper.Map<JobOffer, JobOfferViewModel>(jo);
                jobOfferList.Add(jobOfferViewModel);
            }
            return jobOfferList;
        }

        public JobOffer GetJobOffer(int id)
        {
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null)
            {
                //  throw new Exception("Job offer not found");
            }
            return jobOffer;
        }

        public void DeleteJobOffer(int id)
        {
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null)
            {
                throw new Exception("Job offer not found");
            }
            _context.JobOffers.Remove(jobOffer);
            _context.SaveChanges();
        }

        public void UpdateJobOffer(int id, CreateJobOfferViewModel model)
        {
            var jobOffer = _context.JobOffers.FirstOrDefault(x => x.Id == id);
            if (jobOffer == null)
            {
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
