using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Users.ViewModels;
using DreamJob.Common.Enums;
using DreamJob.Entities.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DreamJob.Controllers
{
    public class UserController : Controller
    {
        private UserService _userService;
        private CandidateService _candidateService;
        public UserController(UserService userService, CandidateService candidateService)
        {
            _userService = userService;
            _candidateService = candidateService;
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

        public async void AddClaimsToUser(CurrentUserViewModel model)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Username", model.Username),
                new Claim("Id", model.Id.ToString()),
                //new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, model.Role),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "AuthenticationCookie");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            //var x = User.Identity.IsAuthenticated;

        }


        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Index", "Home");
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "User");

        }

    }
}
