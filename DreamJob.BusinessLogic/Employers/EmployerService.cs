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
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DreamJob.BusinessLogic.CareerFields;
using Microsoft.EntityFrameworkCore;

namespace DreamJob.BusinessLogic.Employers
{
    public class EmployerService
    {
        private readonly DreamJobContext _context;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly CareerFieldsService _careerFieldService;
        private CurrentUserViewModel currentUser;
        
        

        public EmployerService(DreamJobContext context, UserService userService, IMapper mapper, CareerFieldsService careerFieldService) {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _careerFieldService = careerFieldService;
            currentUser = _userService.GetCurrentUser();
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

        // create a getEmployerById method
        public Entities.Entities.Employer GetEmployerById(int id) {
            var employer = _context.Employers.FirstOrDefault(x => x.Id == id);
            if (employer == null) {
                throw new Exception("Employer not found");
            }
            return employer;
        }

        public UpdateEmployerViewModel GetUpdateEmployerVM() {

            var employer = _context.Employers
                .Include(e => e.User)
                .Where (e => e.UserId == currentUser.Id)
                .FirstOrDefault();

            var careerFields = _careerFieldService.GetEmployerCareerFields(employer.Id);
            var employerToUpdate = _mapper.Map<Employer, UpdateEmployerViewModel>(employer);
            employerToUpdate.CareerFields = careerFields;

            return employerToUpdate;
        }

        public void Update(UpdateEmployerViewModel model) {
            var employer = _mapper.Map<UpdateEmployerViewModel, Employer>(model);
            var newCareerFields = _careerFieldService.CreateNewCareerFields(model.CareerFields, employer.Id);
            var oldCareerFields = _careerFieldService.GetCurrentCareerFields(employer.Id);

            var user = new User {
                Id = currentUser.Id,
                Email = "email",
                UserPassword = model.Password,
                RoleId = currentUser.Role,
                Username = currentUser.Username
            };

            employer.User = user;
            employer.UserId = currentUser.Id;
            _context.Users.Update(user);
            

            _context.CareerFields.RemoveRange(oldCareerFields);
            _context.CareerFields.AddRange(newCareerFields);
            _context.SaveChanges();

            var careerFields = _context.EmployersCareerFields.Where(s => s.Id == employer.Id).ToList();

            employer.EmployersCareerFields.Clear();

            foreach (var careerField in careerFields) {
                employer.EmployersCareerFields.Add(careerField);
            }

            _context.Employers.Update(employer);
            _context.SaveChanges();

        }


    }
}
