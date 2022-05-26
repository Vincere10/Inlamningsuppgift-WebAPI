namespace Inlamningsuppgift.Models
{
    public class UpdateOrderModel
    {
        public string CustomerName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

       // public DateTime OrderDate { get; set; }

        //public string OrderStatus { get; set; } = string.Empty;

        public List<UpdateOrderRowModel> OrderRows { get; set; } = new();

    }

    public class UpdateOrderRowModel
    {
        public int ProductNumber { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal ProductPrice { get; set; }

    }
}

