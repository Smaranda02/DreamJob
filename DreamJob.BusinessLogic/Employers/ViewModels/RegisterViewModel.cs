using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Employers.ViewModels
{
    public class RegisterViewModel
    {
        public string EmployerName { get; set; } = null!;

        public string OfficeLocation { get; set; } = null!;

        public string EmployerDescription { get; set; } = null!;

        public string EmployerLinkedin { get; set; } = null!;
    }
}
