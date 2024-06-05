using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Interactions.ViewModels
{
    public class MatchViewModel
    {
        public int CandidateId { get; set; }    
        public int EmployerId { get; set; }

        public string EmployerName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Surname { get; set; } = null!;
        public string JobDescription { get; set; } = null!;

    }
}
