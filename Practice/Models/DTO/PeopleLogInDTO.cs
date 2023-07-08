using System.ComponentModel.DataAnnotations;

namespace Practice.Models.DTO
{
    public class PeopleLogInDTO
    {
        [Required]
        [StringLength(100)]
        [UIHint("EmailAddress")]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(15, MinimumLength = 3)]
        [UIHint("Password")]
        public string Password { get; set; } = null!;
    }
}
