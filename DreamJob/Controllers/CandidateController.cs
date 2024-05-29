using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.BusinessLogic.Users.ViewModels;
using FluentValidation;

namespace DreamJob.Controllers
{
    public class CandidateController : Controller
    {
        private readonly CandidateService _candidateService;
        private readonly RegisterValidator _registerValidator;
        public CandidateController(CandidateService candidateService, RegisterValidator registerValidator)
        {
            _candidateService = candidateService;
            _registerValidator = registerValidator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = _candidateService.CreateRegisterVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            //var validationResult = _registerValidator.Validate(model, ModelState);
            if (ModelState.IsValid)
            {
                _candidateService.Register(model);
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Update()
        {
            var model = _candidateService.GetUpdateCandidateVM();
            return View(model);
        }

        [HttpPost]  
        public IActionResult Update(UpdateCandidateViewModel model)
        {
            //if (ModelState.IsValid)
            {
                _candidateService.Update(model);
                return RedirectToAction("Index", "Home");
            }
            //return View(model);
        }
    }
}
