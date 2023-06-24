using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public partial class People
{
    [Key]
    public int PersonId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Sex { get; set; }

    public int Age { get; set; }

    public DateTime Birthday { get; set; }

    public string AboutMe { get; set; } = null!;

    public int Likes { get; set; }

    public int Dislikes { get; set; }

    public virtual ICollection<Pair> PairFirstPeople { get; set; } = new List<Pair>();

    public virtual ICollection<Pair> PairSecondPeople { get; set; } = new List<Pair>();
}
