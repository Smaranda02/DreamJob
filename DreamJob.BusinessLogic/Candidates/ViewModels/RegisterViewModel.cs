using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Candidates.ViewModels
{
    public class RegisterViewModel
    {
        public string FirstName { get; set; } = null!;

        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string CandidateDescription { get; set; } = null!;

        public string Linkedin { get; set; } = null!;

    }
}
