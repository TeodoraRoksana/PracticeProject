using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Practice.Models;

public partial class People
{
    public int PeopleId { get; set; }

    [DisplayName("Person Name")]
    public string Name { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public int Gender { get; set; }

    public string Description { get; set; } = null!;
}
