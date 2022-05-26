namespace Inlamningsuppgift.Models
{
    public class OrderFormModel
    {
            public int UserId { get; set; }
           // public DateTime OrderDate { get; set; }
            //public string OrderStatus { get; set; } = string.Empty;

            public List<CartItem> Cart { get; set; } = new();

    }

   public class CartItem
   {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
   }
 }

