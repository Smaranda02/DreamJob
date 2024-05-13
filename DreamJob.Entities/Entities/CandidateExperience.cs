using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class CandidateExperience
{
    public int Id { get; set; }

    public int? CandidateId { get; set; }

    public int? ExperienceId { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual Experience? Experience { get; set; }
}
