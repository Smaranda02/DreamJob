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

namespace DreamJob.BusinessLogic.Users
{
    public class UserService
    {
        private DreamJobContext _context;
        public UserService(DreamJobContext context)
        {
            _context = context;
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

        public bool Login(LoginViewModel model)
        {
            var user = _context.Users.
                                Where(u =>  u.Email == model.Email &&
                                u.UserPassword == model.Password)
                                .FirstOrDefault();
            if (user != null)
            {
                //add claims 
                return true;
            }
           
            return false;
        }

        public LoginViewModel CreateLoginVM()
        {
            var model = new LoginViewModel();
            return model;
        }

        public User CreateUser(RegisterViewModel model)
        {
            var newUser = new User
            {
                Email = model.Email,
                UserPassword = model.Password,
                RoleId = (int?)Roles.Candidate,
            };

            return newUser;
        }
    }
}
