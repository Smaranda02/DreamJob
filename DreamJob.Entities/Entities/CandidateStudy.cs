using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class CandidateStudy
{
    public int Id { get; set; }

    public int? CandidateId { get; set; }

    public int? StudiesId { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual Study? Studies { get; set; }
}
