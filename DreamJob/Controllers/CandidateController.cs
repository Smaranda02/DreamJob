using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Candidates.ViewModels;
using DreamJob.BusinessLogic.Users.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

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
            var validationResult = _registerValidator.Validate(model);
            if (validationResult.IsValid)
            {
                _candidateService.Register(model);
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
        [Authorize(Roles = "Candidate")]
        public IActionResult Update()
        {
            var model = _candidateService.GetUpdateCandidateVM();
            return View(model);
        }


        [HttpGet]
        [Authorize(Roles = "Candidate")]

        public IActionResult GetJsonForUpdate()
        {
            var model = _candidateService.GetUpdateCandidateVM();
            return Ok(model);
        }

        [HttpPost]
        [Authorize(Roles = "Candidate")]

        public IActionResult Update([FromBody] UpdateCandidateViewModel model)
        {
            //if (ModelState.IsValid)
            {
                _candidateService.Update(model);
                return RedirectToAction("Index", "Home");
            }
            //return View(model);
        }

        [Authorize(Roles = "Employer")]
        [HttpGet]
        public IActionResult GetCandidates()
        {

            return View(new DisplayCandidatesViewModel
            {
                Candidates = _candidateService.GetCandidates()
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllCandidates(int? pageNumber)
        {
            //ViewData["CurrentCategory"] = categoryFilter;

            var candidates = _candidateService.GetAllCandidates(pageNumber ?? 1, 10);
            var candidatesCount = _candidateService.GetCandiatesCount();

            DisplayCandidatesViewModel model = new DisplayCandidatesViewModel()
            {
                Candidates = candidates,
                PageIndex = pageNumber.GetValueOrDefault(1),
                TotalPages = (int)Math.Ceiling(candidatesCount / (double)10)

            };

            return View(model);
        }

       
    }
}
