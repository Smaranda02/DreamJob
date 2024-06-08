using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace DreamJob.BusinessLogic.Candidates.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field must be completed")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string CandidateDescription { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string Linkedin { get; set; } = null!;

        public int Role { get; set; }

        public List<SelectListItem> Skills { get; set; } = new List<SelectListItem>();
        public List<int> SelectedSkillIds { get; set; } = new List<int>();


    }
}
