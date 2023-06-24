using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Practice.Models;

public partial class PairDTO
{
    [Key]
    public int PairsId { get; set; }

    public int FirstPersonId { get; set; }

    public int SecondPersonId { get; set; }

    public DateTime Data { get; set; }

    public string FirstPersonComment { get; set; } = null!;

    public string SecondPersonComment { get; set; } = null!;

}
