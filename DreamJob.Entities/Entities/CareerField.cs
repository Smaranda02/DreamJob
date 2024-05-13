using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class CareerField
{
    public int Id { get; set; }

    public string? CareerFieldName { get; set; }

    public virtual ICollection<CandidatesCareerField> CandidatesCareerFields { get; } = new List<CandidatesCareerField>();

    public virtual ICollection<EmployersCareerField> EmployersCareerFields { get; } = new List<EmployersCareerField>();
}
