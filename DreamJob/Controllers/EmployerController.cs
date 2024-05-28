using Microsoft.AspNetCore.Mvc;
using DreamJob.BusinessLogic.Employers.ViewModels;
using DreamJob.BusinessLogic.Employers;

namespace DreamJob.Controllers
{
    public class EmployerController : Controller
    {
        private EmployerService _employerService;
        public EmployerController(EmployerService employerService)
        {
            _employerService = employerService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = _employerService.CreateRegisterVM();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _employerService.Register(model);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
           
        }


    }
}
