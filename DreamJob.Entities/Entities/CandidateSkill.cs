﻿using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class CandidateSkill
{
    public int Id { get; set; }

    public int? CandidateId { get; set; }

    public int? SkillId { get; set; }

    public virtual Candidate? Candidate { get; set; }

    public virtual Skill? Skill { get; set; }
}
