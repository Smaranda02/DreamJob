using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Interactions.ViewModels
{
    public class DisplayMatchesViewModel
    {
        public List<MatchViewModel> Matches { get; set; } = new List<MatchViewModel>();

        public bool IsCandidate { get; set; }
    }
}
