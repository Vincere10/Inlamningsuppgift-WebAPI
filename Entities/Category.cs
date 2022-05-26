using System.ComponentModel.DataAnnotations;

namespace Inlamningsuppgift.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;

        public virtual ICollection<Product> Products { get; set; } = null!;
    }
}
