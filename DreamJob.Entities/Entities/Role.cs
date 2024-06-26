﻿using System;
using System.Collections.Generic;

namespace DreamJob.Entities.Entities;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
