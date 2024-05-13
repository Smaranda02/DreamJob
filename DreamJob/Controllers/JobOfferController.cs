using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.JobOffer.ViewModels;
using DreamJob.BusinessLogic.JobOffers;

namespace DreamJob.Controllers
{
    public class JobOfferController : Controller
    {
        private JobOfferService _jobOfferService;
        public JobOfferController(JobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = _jobOfferService.CreateJobOfferVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddJobOffer(CreateJobOfferViewModel model)
        {

            return Ok();
        }
    }
}
