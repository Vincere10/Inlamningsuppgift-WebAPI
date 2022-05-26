using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inlamningsuppgift.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Categories { get; set; } = null!;
    }
}
