using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inlamningsuppgift.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required, StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, StringLength(100)]
        public string OrderStatus { get; set; } = string.Empty;

        public decimal TotalPrice => OrderRows.Sum(x => x.Quantity * x.ProductPrice);

        public virtual ICollection<OrderRow> OrderRows { get; set; }

    }

    public class OrderRow
    {
        [Key]
        public int OrderRowId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductNumber { get; set; }

        [Required, StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal ProductPrice { get; set; }

        public Order Order { get; set; }
    }
}

