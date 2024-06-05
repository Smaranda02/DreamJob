using DreamJob.BusinessLogic.Interactions;
using DreamJob.BusinessLogic.Interactions.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamJob.Controllers
{
    [Authorize]
    public class InteractionController : Controller
    {
        private readonly InteractionService _interactionService;

        public InteractionController(InteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUpdate([FromBody] InteractionViewModel interactions)
        {
            _interactionService.UpdateCandidateInteraction(interactions);
            return Ok();  
        }
    }
}
