﻿using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Candidates.ViewModels;
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
                return View("Login", "User");
            }
            else
            {
                return View(model);
            }
        }

    }
}
