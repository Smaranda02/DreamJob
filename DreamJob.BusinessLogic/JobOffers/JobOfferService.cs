﻿using DreamJob.BusinessLogic.JobOffers.ViewModels;
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
using DreamJob.BusinessLogic.Users.Mappings;
using DreamJob.BusinessLogic.Users.ViewModels;

namespace DreamJob.BusinessLogic.JobOffers
{
    public class JobOfferService
    {

        private readonly DreamJobContext _context;
        private readonly SkillsService _skillsService;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private CurrentUserViewModel currentUser;
        public JobOfferService(DreamJobContext context, SkillsService skillsService, IMapper mapper, UserService userService)
        {
            _context = context;
            _skillsService = skillsService;
            _mapper = mapper;
            _userService = userService;
            currentUser = _userService.GetCurrentUser();

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

        public List<JobOfferViewModel> GetMyJobOffers(int pageIndex, int pageSize)
        {
            var jobOffers = _context.JobOffers
                            .Include(j => j.Employer)
                            .Where(j => j.EmployerId == _userService.GetCurrentEmployerId())
                            .Skip((pageIndex - 1) * pageSize).Take(pageSize)
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
                            .Include(j => j.Employer)
                            .Include(j => j.JobSkills)
                            .ThenInclude(j => j.Skill)
                            .ToList();
           
           
            var jobOfferList = new List<JobOfferViewModel>();

            foreach (var jo in jobOffers)
            {
                var jobOfferViewModel = _mapper.Map<JobOffer, JobOfferViewModel>(jo);
                var skills = new List<string>();

                foreach(var skill in jo.JobSkills)
                {
                    skills.Add(skill.Skill.SkillName);
                }

                jobOfferViewModel.JobSkills = skills;
                jobOfferList.Add(jobOfferViewModel);
            }
            return jobOfferList;
        }
        

        public int GetJobOffersCount()
        {
            return _context.JobOffers.Count();
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

            var skillsToDelete = _context.JobSkills.Where(j => j.JobOfferId == id);
            var interactionsToDelete = _context.Interactions.Where(i => i.JobOfferId == id);

            _context.JobSkills.RemoveRange(skillsToDelete);
            _context.Interactions.RemoveRange(interactionsToDelete);
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

        public UpdateJobOfferViewModel GetUpdateJobOfferVM() {
            var jobOffer = _context.JobOffers
                .Include(j => j.Employer)
                .FirstOrDefault(j => j.EmployerId == currentUser.Id);

            var skills = _skillsService.GetJobOfferSkills(jobOffer.Id);
            var jobOfferToUpdate = _mapper.Map<JobOffer, UpdateJobOfferViewModel>(jobOffer);
            jobOfferToUpdate.JobSkills = skills;

            return jobOfferToUpdate;
        }

        /*public Update(UpdateJobOfferViewModel model) {
            var jobOffer = _mapper.Map<UpdateJobOfferViewModel, JobOffer>(model);
            var newSkills = _skillsService.CreateJobSkill(model.SkillIds, jobOffer);
            var oldSkills = _skillsService.GetJobOfferSkills(jobOffer.Id);

            var employer = new Employer {
                Id = currentUser.Id,
                EmployerName = model.EmployerName,
                EmployerDescription = model.EmployerDescription,
            };
        }*/

    }
}
