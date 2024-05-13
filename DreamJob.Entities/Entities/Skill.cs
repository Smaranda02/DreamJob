using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Skill
{
    public int Id { get; set; }

    public string SkillName { get; set; } = null!;

    public string SkillDescription { get; set; } = null!;

    public virtual ICollection<CandidateSkill> CandidateSkills { get; } = new List<CandidateSkill>();

    public virtual ICollection<JobSkill> JobSkills { get; } = new List<JobSkill>();
}
