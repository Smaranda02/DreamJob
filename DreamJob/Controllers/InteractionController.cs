using DreamJob.BusinessLogic.Interactions;
using DreamJob.BusinessLogic.Interactions.ViewModels;
using DreamJob.BusinessLogic.Users;
using DreamJob.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamJob.Controllers
{
    [Authorize]
    public class InteractionController : Controller
    {
        private readonly InteractionService _interactionService;
        private readonly UserService _userService;

        public InteractionController(InteractionService interactionService, UserService userService)
        {
            _interactionService = interactionService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Interaction/CreateUpdate/{fromEmployer}")]
        public IActionResult CreateUpdate([FromBody] InteractionViewModel interactions, bool fromEmployer)
        {
            _interactionService.UpdateInteraction(interactions, fromEmployer);
            return Ok();  
        }

        [HttpGet]
        public IActionResult Matches()
        {
            var model = _interactionService.GetMatches();
            return View(new DisplayMatchesViewModel
            {
                Matches = model,
                IsCandidate = _userService.GetCurrentUser().Role == (int)Roles.Candidate ? true : false
            });
        }
    }
}
