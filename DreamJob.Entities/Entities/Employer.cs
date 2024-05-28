using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Employer
{
    public int Id { get; set; }

    public string EmployerName { get; set; } = null!;

    public string OfficeLocation { get; set; } = null!;

    public string EmployerDescription { get; set; } = null!;

    public string EmployerLinkedin { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<EmployersCareerField> EmployersCareerFields { get; } = new List<EmployersCareerField>();

    public virtual ICollection<JobOffer> JobOffers { get; } = new List<JobOffer>();

    public virtual User User { get; set; } = null!;
}
