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
        [Route("Interaction/CreateUpdate/{fromEmployer}")]
        public IActionResult CreateUpdate([FromBody] InteractionViewModel interactions, bool fromEmployer)
        {
            _interactionService.UpdateCandidateInteraction(interactions, fromEmployer);
            return Ok();  
        }
    }
}
