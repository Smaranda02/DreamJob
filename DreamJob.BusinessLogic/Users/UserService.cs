using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DreamJob.Common.Enums;
using Microsoft.EntityFrameworkCore;
using DreamJob.DataAccess.EntityFramework;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DreamJob.BusinessLogic.Users
{
    public class UserService
    {
        private DreamJobContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserService(DreamJobContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

      

        //public string HashPassword(string password)
        //{
        //    byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes

        //    // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password!,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 100000,
        //        numBytesRequested: 256 / 8));

        //    return hashed;
        //}

        public CurrentUserViewModel Login(LoginViewModel model)
        {
            var user = _context.Users.
                                Where(u =>  u.Email == model.Email &&
                                u.UserPassword == model.Password)
                                .FirstOrDefault();
            if (user == null)
            {
                return new CurrentUserViewModel { IsAuthenticated = false };
            }

            var currentUser = _mapper.Map<User, CurrentUserViewModel>(user);
            return currentUser;
        }

        public LoginViewModel CreateLoginVM()
        {
            var model = new LoginViewModel();
            return model;
        }

        public User CreateUser(UserViewModel model)
        {
            var newUser = new User
            {
                Email = model.Email,
                UserPassword = model.Password,
                RoleId = model.Role,
                Username = model.Username
            };

            return newUser;
        }


        public CurrentUserViewModel GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return new CurrentUserViewModel { IsAuthenticated = false };
            }

            Enum.TryParse<Roles>(user.FindFirstValue(ClaimTypes.Role), out var roleEnum);

            return new CurrentUserViewModel
            {
                Id = int.Parse(user.FindFirstValue("Id")),
                IsAuthenticated = user.Identity.IsAuthenticated,
                Email = user.FindFirstValue(ClaimTypes.Email),
                Username = user.FindFirstValue("Username"),
                Role = (int)roleEnum,
            };

        }
    }
}
