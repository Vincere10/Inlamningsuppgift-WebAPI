using Inlamningsuppgift.Entities;
using Inlamningsuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace Inlamningsuppgift.Services
{
    public interface IOrderService
    {
        Task<Order> CreateAsync(OrderFormModel model);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetAsync(int id);
        Task<Order> UpdateAsync(int id, UpdateOrderModel model);
        Task<bool> DeleteAsync(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly DataContext _dataContext;

        public OrderService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Order> CreateAsync(OrderFormModel model)
        {
            var user = await _dataContext.Users.FindAsync(model.UserId);

            if (user == null) return null!;

            if (model.Cart.Count == 0) return null!;

            var order = new Order
            {
                CustomerName = user.FirstName + " " + user.LastName,
                Address = user.Address + ", " + user.PostalCode + ", " + user.City,
                OrderDate = DateTime.Now,
                OrderStatus = "Received!"
            };

            var orderRows = new List<OrderRow>();

            foreach (var item in model.Cart)
            {
                var product = await _dataContext.Products.FindAsync(item.ProductId);
                if (product == null) return null!;
                orderRows.Add(new OrderRow
                {
                    ProductName = product.Name,
                    ProductNumber = product.ProductId,
                    ProductPrice = product.Price,
                    Quantity = item.Quantity,
                    OrderId = order.OrderId,
                });
            }

            order.OrderRows = orderRows;
            _dataContext.Orders.Add(order);
            await _dataContext.SaveChangesAsync();
            return order;
        }



        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dataContext.Orders.Include(x => x.OrderRows).ToListAsync();
        }

        public async Task<Order> GetAsync(int id)
        {
            var order = await _dataContext.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.OrderId == id);
            if (order != null)
            {
                return order;
            }
            return null!;
        }

        public async Task<Order> UpdateAsync(int id, UpdateOrderModel model)
        {
            if (model.OrderRows == null || model.OrderRows.Count == 0) return null!;

            var order = await _dataContext.Orders.Include(x => x.OrderRows).FirstOrDefaultAsync(x => x.OrderId == id);
            if (order == null)
                return null!;

            order.CustomerName = model.CustomerName;
            order.Address = model.Address;

            var OrderRows = new List<OrderRow>(order.OrderRows);
            foreach (var cartItem in model.OrderRows)
            {
                var orderRow = OrderRows.FirstOrDefault(x => x.ProductNumber == cartItem.ProductNumber);

                if (orderRow == null)
                {
                    OrderRows.Add(new OrderRow
                    {
                        OrderId = order.OrderId,
                        ProductNumber = cartItem.ProductNumber,
                        ProductName = cartItem.ProductName,
                        ProductPrice = cartItem.ProductPrice,
                        Quantity = cartItem.Quantity
                    });
                }
                else
                {
                    orderRow.Quantity = cartItem.Quantity;
                    orderRow.ProductNumber = cartItem.ProductNumber;
                    orderRow.ProductName = cartItem.ProductName;
                    orderRow.ProductPrice = cartItem.ProductPrice;
                    _dataContext.Entry(orderRow).State = EntityState.Modified;
                    if (orderRow.Quantity < 1) _dataContext.OrderRows.Remove(orderRow);
                }
            }

            order.OrderRows = OrderRows;

            _dataContext.Entry(order).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();

            return order;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _dataContext.Orders.FindAsync(id);
            if (order != null)
            {
                var orderRow = await _dataContext.OrderRows.FirstOrDefaultAsync(x => x.OrderId == id);
                if (orderRow != null)
                {
                    _dataContext.OrderRows.Remove(orderRow);
                    _dataContext.Orders.Remove(order);
                    await _dataContext.SaveChangesAsync();
                    return true;
                }
            }

            return false;
        }
    }
}
