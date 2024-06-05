using AutoMapper;
using DreamJob.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamJob.BusinessLogic.Interactions.ViewModels
{
    public class InteractionViewModel 
    {
        public int? CandidateId { get; set; }

        public int? JobOfferId { get; set; }

        public DateTime InteractionDate { get; set; }

        public bool FeedbackCandidate { get; set; }

        public bool FeedbackEmployer { get; set; }
    }
}
