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

        [HttpPost]
        [Authorize(Roles = "Employer, Admin")]

        public IActionResult Delete(int id) {

            _jobOfferService.DeleteJobOffer(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public IActionResult ViewJobOffer(int id) {
            var jobOffer = _jobOfferService.GetJobOffer(id);
            return View(jobOffer);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllJobOffersForAdmin() {

            var jobOffers = _jobOfferService.GetAllJobOffers();
            return View(
                new DisplayJobOffersViewModel
            {
                JobOffersViewModel= jobOffers
            });
        }

        [HttpGet]
        [Authorize(Roles = "Candidate")]
        public IActionResult GetAllJobOffers()
        {

            var jobOffers = _jobOfferService.GetAllJobOffers();
            return View(new DisplayJobOffersViewModel
            {
                JobOffersViewModel = jobOffers
            });
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        public IActionResult GetMyJobOffers(int? pageNumber)
        {

            var jobOffers = _jobOfferService.GetMyJobOffers(pageNumber ?? 1, 10);
            var jobOffersCount = _jobOfferService.GetJobOffersCount();

            var model = new DisplayJobOffersViewModel
            {
                JobOffersViewModel = jobOffers,
                PageIndex = pageNumber.GetValueOrDefault(1),
                TotalPages = (int)Math.Ceiling(jobOffersCount / (double)10)
            };
            return View(model);
        }
    }
}
