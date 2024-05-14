using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.Users;
using DreamJob.BusinessLogic.Candidates;
using DreamJob.BusinessLogic.Candidates.ViewModels;

namespace DreamJob.Controllers
{
    public class CandidateController : Controller
    {
        private readonly CandidateService _candidateService;
        public CandidateController(CandidateService candidateService)
        {
            _candidateService = candidateService;
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
            _candidateService.Register(model);
            return View(model);
        }

    }
}
