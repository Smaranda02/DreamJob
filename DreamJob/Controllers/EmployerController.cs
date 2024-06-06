using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.Employers.ViewModels;
using DreamJob.BusinessLogic.Employers;
using DreamJob.BusinessLogic.Candidates;

namespace DreamJob.Controllers
{
    public class EmployerController : Controller
    {
        private EmployerService _employerService;
        private readonly RegisterEmployerValidator _registerValidator;
        public EmployerController(EmployerService employerService, RegisterEmployerValidator registerValidator)
        {
            _employerService = employerService;
            _registerValidator = registerValidator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = _employerService.CreateRegisterVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            var validationResult = _registerValidator.Validate(model);
            if (validationResult.IsValid && ModelState.IsValid)
            {
                _employerService.Register(model);
                return RedirectToAction("Login", "User");
            }
            else
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(model);
            }
           
        }

        [HttpGet]
        public IActionResult Update() {
            var model = _employerService.GetUpdateEmployerVM();
            return View(model);
        }

        [HttpGet]
        public IActionResult GetJsonForUpdate() {
            var model = _employerService.GetUpdateEmployerVM();
            return Ok(model);
        }

        [HttpPost]
        public IActionResult Update([FromBody] UpdateEmployerViewModel model) {
            if (ModelState.IsValid) {
                _employerService.Update(model);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        } 


    }
}
