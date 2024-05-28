using DreamJob.DataAccess.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Entities.Entities;
using DreamJob.BusinessLogic.Employers.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.Common.Enums;
using DreamJob.BusinessLogic.Users.ViewModels;

namespace DreamJob.BusinessLogic.Employers
{
    public class EmployerService
    {
        private readonly DreamJobContext _context;
        private readonly UserService _userService;

        public EmployerService(DreamJobContext context, UserService userService) 
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
            model.Role = (int)Roles.Employer;
            var username = model.EmployerName.ToLower();

            var userVM = new UserViewModel
            {
                Email = model.Email,
                Password = model.Password,
                Role = model.Role,
                Username = username
            };

            var newUser = _userService.CreateUser(userVM);


            var employer = new Employer
            {
                EmployerName = model.EmployerName,
                OfficeLocation = model.OfficeLocation,
                EmployerLinkedin = model.EmployerLinkedin,
                EmployerDescription = model.EmployerDescription,
                User = newUser,
            };


            _context.Users.Add(newUser);
            _context.Employers.Add(employer);
            _context.SaveChanges();
        }
    }
}
