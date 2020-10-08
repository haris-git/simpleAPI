using Microsoft.EntityFrameworkCore;
using SimpleAPI.Context;
using SimpleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Services
{
    public class OrderRepository : IOrderRepository
    {
        private SimpleApiContext _context;

        public OrderRepository(SimpleApiContext context)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.OrderBy(o => o.Id).ToList();
        }

        public void DeleteOrder(Order order)
        {
            _context.Remove(order);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.Id == orderId);
        }
    }
}
