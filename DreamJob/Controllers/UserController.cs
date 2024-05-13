using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Users.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            if (response)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}
