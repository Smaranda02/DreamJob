using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Candidates.ViewModels
{
    public class DisplayCandidatesViewModel
    {
        public List<CandidateViewModel> Candidates { get; set; }
        public int? Id { get; set; }

    }
}
