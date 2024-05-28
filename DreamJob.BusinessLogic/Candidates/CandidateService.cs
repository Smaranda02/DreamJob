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

namespace DreamJob.BusinessLogic.Candidates
{
    public class CandidateService
    {
        private readonly DreamJobContext _context;
        private readonly UserService _userService;

        public CandidateService(DreamJobContext context, UserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public RegisterViewModel CreateRegisterVM()
        {
            var model = new RegisterViewModel();
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
                User = newUser
            };

            _context.Users.Add(newUser);
            _context.Candidates.Add(newCandidate);
            _context.SaveChanges();

        }
    }
}
