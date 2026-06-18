using System;
using System.Collections.Generic;

namespace Httpclient.AuthDatabase.Models;

public partial class Role1
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
