using System.ComponentModel.DataAnnotations;

namespace Inlamningsuppgift.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, StringLength(10)]
        public string PostalCode { get; set; } = string.Empty;

    }
}
