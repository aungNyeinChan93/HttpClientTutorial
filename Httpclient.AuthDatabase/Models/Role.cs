using System;
using System.Collections.Generic;

namespace Httpclient.AuthDatabase.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
