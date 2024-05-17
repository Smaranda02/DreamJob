using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class JobOffer
{
    public int Id { get; set; }

    public int EmployerId { get; set; }

    public decimal Salary { get; set; }

    public string JobDescription { get; set; } = null!;

    public virtual Employer Employer { get; set; } = null!;

    // string? Location { get; set; } = null!;

   // public string? JobTitle { get; set; } = null!;

    // public string? JobIndustry { get; set; } = null!;


    public virtual ICollection<Interaction> Interactions { get; } = new List<Interaction>();

    public virtual ICollection<JobSkill> JobSkills { get; } = new List<JobSkill>();
}
