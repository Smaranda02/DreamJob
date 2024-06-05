using DreamJob.BusinessLogic.Studies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamJob.Controllers
{
    [Authorize]
    public class StudyController : Controller
    {
        private readonly ILogger<StudyController> _logger;
        private readonly StudyService _studyService;

        public StudyController(ILogger<StudyController> logger, StudyService studyService)
        {
            _logger = logger;
            _studyService = studyService;
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var study = _studyService.GetStudyById(id);
            return Ok(study);
        }
    }
}
