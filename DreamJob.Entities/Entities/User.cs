using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string? Username { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Candidate> Candidates { get; } = new List<Candidate>();

    public virtual Role? Role { get; set; }
}
