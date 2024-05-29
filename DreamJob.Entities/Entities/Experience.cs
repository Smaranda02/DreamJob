using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Experience
{
    public int Id { get; set; }

    public string ExperienceName { get; set; } = null!;

    public DateTime StartYear { get; set; }

    public DateTime EndYear { get; set; }

    public string ExperienceDescription { get; set; } = null!;

    public int? CandidateId { get; set; }

    public virtual Candidate? Candidate { get; set; }
}
