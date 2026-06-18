using System;
using System.Collections.Generic;

namespace Httpclient.AuthDatabase.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? RoleId { get; set; }

    public virtual Role1? Role { get; set; }
}
