using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class JobSkill
{
    public int Id { get; set; }

    public int? JobOfferId { get; set; }

    public int? SkillId { get; set; }

    public virtual JobOffer? JobOffer { get; set; }

    public virtual Skill? Skill { get; set; }
}
