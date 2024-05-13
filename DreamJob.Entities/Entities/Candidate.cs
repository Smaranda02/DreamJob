using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Candidate
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int UserId { get; set; }

    public string CandidateDescription { get; set; } = null!;

    public string Linkedin { get; set; } = null!;

    public virtual ICollection<CandidateExperience> CandidateExperiences { get; } = new List<CandidateExperience>();

    public virtual ICollection<CandidateSkill> CandidateSkills { get; } = new List<CandidateSkill>();

    public virtual ICollection<CandidateStudy> CandidateStudies { get; } = new List<CandidateStudy>();

    public virtual ICollection<CandidatesCareerField> CandidatesCareerFields { get; } = new List<CandidatesCareerField>();

    public virtual ICollection<Interaction> Interactions { get; } = new List<Interaction>();

    public virtual User User { get; set; } = null!;
}
