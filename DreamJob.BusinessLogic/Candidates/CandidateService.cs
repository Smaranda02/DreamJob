using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Common.Enums;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.BusinessLogic.Skills;
using System.Web.Mvc;

namespace DreamJob.BusinessLogic.Candidates
{
    public class CandidateService
    {
        private readonly DreamJobContext _context;
        private readonly UserService _userService;
        private readonly SkillsService _skillsService;

        public CandidateService(DreamJobContext context, UserService userService, SkillsService skillsService)
        {
            _context = context;
            _userService = userService;
            _skillsService = skillsService;
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

       
    }
}
