using DreamJob.DataAccess.EntityFramework;
using DreamJob.Common.Enums;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.BusinessLogic.Skills;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DreamJob.BusinessLogic.Studies;
using DreamJob.BusinessLogic.Experiences;
using DreamJob.BusinessLogic.JobOffers.ViewModels;

namespace DreamJob.BusinessLogic.Candidates
{
    public class CandidateService
    {
        private readonly DreamJobContext _context;
        private readonly UserService _userService;
        private readonly SkillsService _skillsService;
        private CurrentUserViewModel currentUser;
        private readonly IMapper _mapper;
        private readonly StudyService _studyService;
        private readonly ExperienceService _experienceService;

        public CandidateService(DreamJobContext context, UserService userService, SkillsService skillsService, 
            IMapper mapper, StudyService studyService, ExperienceService experienceService)
        {
            _context = context;
            _userService = userService;
            _skillsService = skillsService;
            currentUser = _userService.GetCurrentUser();
            _mapper = mapper;
            _studyService = studyService;
            _experienceService = experienceService;
        }

        public RegisterViewModel CreateRegisterVM()
        {
            var skills = _skillsService.GetDefaultSkills();
            var model = new RegisterViewModel
            {
                Skills = skills,
            };
            return model;
        }


        public void Register(RegisterViewModel model)
        {
            // var hashedPassword = HashPassword(model.Password);
            model.Role = (int)Roles.Candidate;
            var username = model.FirstName.ToLower() + model.Surname.ToLower();

            var userVM = new UserViewModel
            {
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                Username = username,
            };

            var newUser = _userService.CreateUser(userVM);

            var newCandidate = new Candidate
            {
                FirstName = model.FirstName,
                Surname = model.Surname,
                CandidateDescription = model.CandidateDescription,
                Linkedin = model.Linkedin,
                User = newUser,
            };

            var candidateSkills = _skillsService.CreateCandidateSkill(model.SelectedSkillIds, newCandidate);

            _context.Users.Add(newUser);
            _context.Candidates.Add(newCandidate);
            _context.CandidateSkills.AddRange(candidateSkills);
            _context.SaveChanges();

        }


        public UpdateCandidateViewModel GetUpdateCandidateVM()
        {
            var candidate =  _context.Candidates
                                .Include(c => c.User)
                                .Where(c => c.UserId == currentUser.Id)
                                .FirstOrDefault();

            var studies = _studyService.GetCandidateStudies(candidate.Id);
            var experiences = _experienceService.GetCandidateExperiences(candidate.Id);
            var candidateToUpdate = _mapper.Map<Candidate, UpdateCandidateViewModel>(candidate);
            candidateToUpdate.Studies = studies;
            candidateToUpdate.Experiences= experiences;

            return candidateToUpdate;
        }

        public void Update(UpdateCandidateViewModel model)
        {
            var candidate = _mapper.Map<UpdateCandidateViewModel, Candidate>(model);
            var newStudies = _studyService.CreateNewStudies(model.Studies, candidate.Id);
            var oldStudies = _studyService.GetCurrentStudies(candidate.Id);
            var newExperiences = _experienceService.CreateExperinces(model.Experiences, candidate.Id);
            var oldExperiences = _experienceService.GetCurrentExperiences(candidate.Id);

            var user = new User
            {
                Id = currentUser.Id,
                Email = model.Email,
                UserPassword = model.Password,
                RoleId = currentUser.Role,
                Username = currentUser.Username
            };

            candidate.User = user;
            candidate.UserId = currentUser.Id;

            _context.Users.Update(user);
            _context.Studies.RemoveRange(oldStudies);
            _context.Experiences.RemoveRange(oldExperiences);
            _context.SaveChanges();

            _context.Studies.AddRange(newStudies);
            _context.Experiences.AddRange(newExperiences);
            _context.SaveChanges();


            var exp = _context.Experiences.Where(e => e.CandidateId == candidate.Id).ToList();
            var stu = _context.Studies.Where(e => e.CandidateId == candidate.Id).ToList();

            candidate.Experiences.Clear();
            candidate.Studies.Clear();


            foreach (var ex in exp)
                candidate.Experiences.Add(ex);

            foreach(var s in stu)
                candidate.Studies.Add(s);   

            _context.Candidates.Update(candidate);
            _context.SaveChanges();
        }


        public List<CandidateViewModel> GetCandidates()
        {
            var candidatesList = new List<CandidateViewModel>();
            var currentUserId = _userService.GetCurrentUser().Id;

            var employerId = _context.Employers
                                     .Where(e => e.UserId == currentUserId)
                                     .Select( e => e.Id)
                                     .FirstOrDefault();

            //i want all the candidates that have interacted with at least one job offer created by me, the employer 

            var result = _context.Interactions
                                        .Include(i => i.JobOffer)
                                        .Where(i => i.JobOffer.Employer.Id == employerId)
                                        .Select(i => new { i.Candidate, i.JobOffer})
                                        .ToList()
                                        .Select(a => (a.Candidate, a.JobOffer))
                                        .ToList();

            var candidates = result.Select(r => r.Candidate).ToList();
            var jobOffers = result.Select(r => r.JobOffer).ToList();

            for(int i = 0; i < candidates.Count; i++)
            {
                var candidateViewModel = _mapper.Map<Candidate, CandidateViewModel>(candidates[i]);
                var jobOfferViewModel = _mapper.Map<JobOffer, JobOfferViewModel>(jobOffers[i]);
                candidateViewModel.JobOffer = jobOfferViewModel;
                candidatesList.Add(candidateViewModel);
            }

            return candidatesList;
        }
       
    }
}
