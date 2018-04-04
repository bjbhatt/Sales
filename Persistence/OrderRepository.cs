using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Sales.Models;

namespace Sales.Persistence
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(SalesDbContext context)
            : base(context)
        {
        }
        public IEnumerable<Order> GetOrdersByDate(DateTime date)
        {
            return Find(c => c.OrderDate == date);
        }

        public new IEnumerable<Order> List()
        {
            return _context.Orders
                .Include(o => o.Customer);
        }

        public new Order Get(int id)
        {
            return _context.Orders
                .Include(o => o.Customer)
                .SingleOrDefault(o => o.Id == id);
        }
    }
}