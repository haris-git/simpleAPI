using SimpleAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Services
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();
        Order GetOrder(int orderId);
        bool Save();
    }
}
