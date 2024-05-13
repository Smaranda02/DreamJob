using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.BusinessLogic.Candidate.ViewModels;
using DreamJob.Common.Enums;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.Users;


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

            var newUser = _userService.CreateUser(model);

            var newCandidate = new Entities.Entities.Candidate
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
