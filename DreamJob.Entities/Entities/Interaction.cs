using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Interaction
{
    public int Id { get; set; }

    public int? CandidateId { get; set; }

    public int? JobOfferId { get; set; }

    public DateTime InteractionDate { get; set; }

    public bool FeedbackCandidate { get; set; }

    public bool FeedbackEmployer { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual JobOffer? JobOffer { get; set; }
}
