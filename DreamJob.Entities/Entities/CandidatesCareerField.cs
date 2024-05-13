using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class CandidatesCareerField
{
    public int Id { get; set; }

    public int? CareerFieldId { get; set; }

    public int? CandidateId { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual CareerField? CareerField { get; set; }
}
