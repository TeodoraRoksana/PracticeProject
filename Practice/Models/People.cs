using System;
using System.Collections.Generic;

namespace Practice.Models;

public partial class People
{
    public int PeopleId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int Gender { get; set; }

    public string Description { get; set; } = null!;
}
