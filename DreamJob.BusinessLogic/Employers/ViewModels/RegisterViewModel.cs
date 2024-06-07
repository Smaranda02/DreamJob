using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DreamJob.BusinessLogic.Employers.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field must be completed")]
        public string EmployerName { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string OfficeLocation { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string EmployerDescription { get; set; } = null!;

        [Required(ErrorMessage = "Field must be completed")]
        public string EmployerLinkedin { get; set; } = null!;

        public int Role { get; set; }

    }
}
