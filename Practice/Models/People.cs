using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public partial class People
{
    [Key]
    public int PersonId { get; set; }
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;
    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = null!;
    [Required]
    [StringLength(100)]
    [UIHint("EmailAddress")]
    public string Email { get; set; } = null!;
    [Required]
    [StringLength(15, MinimumLength = 3)]
    [UIHint("Password")]
    public string Password { get; set; } = null!;
    [Required]
    public int Sex { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public DateTime Birthday { get; set; }
    [Required]
    public string AboutMe { get; set; } = null!;
    [Required]
    public int Likes { get; set; }
    [Required]
    public int Dislikes { get; set; }

    public virtual ICollection<Pair>? PairFirstPeople { get; set; } = new List<Pair>();

    public virtual ICollection<Pair>? PairSecondPeople { get; set; } = new List<Pair>();
}
