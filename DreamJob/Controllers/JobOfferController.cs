using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.JobOffers.ViewModels;
using DreamJob.BusinessLogic.Employers;
using DreamJob.BusinessLogic.Users.ViewModels;
using Microsoft.AspNetCore.Authorization;
using DreamJob.BusinessLogic.JobOffers;

namespace DreamJob.Controllers
{
    [Authorize]
    public class JobOfferController : Controller
    {
        private JobOfferService _jobOfferService;
        private EmployerService _employerService;

        public JobOfferController(JobOfferService jobOfferService)
        {
            _jobOfferService = jobOfferService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]

        public IActionResult CreateJobOffer()
        {
            var model = _jobOfferService.CreateJobOfferVM();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]

        public IActionResult AddJobOffer(CreateJobOfferViewModel model)
        {
            _jobOfferService.CreateJobOffer(model);
            return Ok();
            
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]

        public IActionResult EditJobOffer(int id, CreateJobOfferViewModel model) {
            _jobOfferService.UpdateJobOffer(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
            [Authorize(Roles = "Employer")]

        public IActionResult DeleteJobOffer(int id) {
            var jobOffer = _jobOfferService.GetJobOffer(id);
            return View(jobOffer);
        }

        [HttpGet]
        public IActionResult ViewJobOffer(int id) {
            var jobOffer = _jobOfferService.GetJobOffer(id);
            return View(jobOffer);
        }

        [HttpGet]
        public IActionResult GetAllJobOffers() {

            var jobOffers = _jobOfferService.GetJobOffers();
            return View(new DisplayJobOffersViewModel
            {
                JobOffersViewModel= jobOffers
            });
        }
    }
}
