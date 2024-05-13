using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class EmployersCareerField
{
    public int Id { get; set; }

    public int? CareerFieldId { get; set; }

    public int? EmployerId { get; set; }

    public virtual CareerField? CareerField { get; set; }

    public virtual Employer? Employer { get; set; }
}
