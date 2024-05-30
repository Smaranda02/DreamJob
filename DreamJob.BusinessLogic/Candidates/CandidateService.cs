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

namespace DreamJob.BusinessLogic.Candidates
{
    public class CandidateService
    {
        private readonly DreamJobContext _context;
        private readonly UserService _userService;
        private readonly SkillsService _skillsService;
        private CurrentUserViewModel currentUser;
        private readonly IMapper _mapper;

        public CandidateService(DreamJobContext context, UserService userService, SkillsService skillsService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _skillsService = skillsService;
            currentUser = _userService.GetCurrentUser();
            _mapper = mapper;
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
            var user =  _context.Candidates
                                .Include(c => c.User)
                                .Where(c => c.UserId == currentUser.Id)
                                .FirstOrDefault();

            var userToUpdate = _mapper.Map<Candidate, UpdateCandidateViewModel>(user);

            return userToUpdate;
        }

        public void Update(UpdateCandidateViewModel model)
        {
            var candidate = _mapper.Map<UpdateCandidateViewModel, Candidate>(model);

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
            _context.Candidates.Update(candidate);
            _context.SaveChanges();
        }

       
    }
}
