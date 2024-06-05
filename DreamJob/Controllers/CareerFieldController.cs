using DreamJob.BusinessLogic.CareerFields;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace DreamJob.Controllers {
    public class CareerFieldController : Controller {

        private readonly ILogger<CareerFieldController> _logger;
        private readonly CareerFieldsService _careerFieldService;

        public CareerFieldController(ILogger<CareerFieldController> logger, CareerFieldsService careerFieldService) {
            _logger = logger;
            _careerFieldService = careerFieldService;
        }
        public IActionResult Get(int id) {
            var careerField = _careerFieldService.GetCareerFieldById(id);
            return View();
        }
    }
}
