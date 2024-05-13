using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Study
{
    public int Id { get; set; }

    public string University { get; set; } = null!;

    public string Specialty { get; set; } = null!;

    public DateTime StartYear { get; set; }

    public DateTime EndYear { get; set; }

    public virtual ICollection<CandidateStudy> CandidateStudies { get; } = new List<CandidateStudy>();
}
