using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.JobOffer.ViewModels;
using DreamJob.BusinessLogic.JobOffers;
using DreamJob.BusinessLogic.Employers;

namespace DreamJob.Controllers
{
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
        public IActionResult CreateJobOffer()
        {
            var model = _jobOfferService.CreateJobOfferVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddJobOffer(CreateJobOfferViewModel model)
        {
            _jobOfferService.CreateJobOffer(model);
            return Ok();
        }

        [HttpPost]
        public IActionResult EditJobOffer(int id, CreateJobOfferViewModel model) {
            _jobOfferService.UpdateJobOffer(id, model);
            return RedirectToAction("Index");
        }

        [HttpGet]
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
            return View(jobOffers);
        }
    }
}
