using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.Common.Enums;
using DreamJob.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DreamJob.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = _userService.CreateLoginVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var response = _userService.Login(model);
            if (response.IsAuthenticated)
            {
                AddClaimsToUser(response);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(model);
            }
        }

        public void AddClaimsToUser(CurrentUserViewModel model)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Username", model.Username),
                new Claim("Id", model.Id.ToString()),
                //new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(Roles), model.Role)),
            };

        }
    }
}
