using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Sales.Models;

namespace Sales.Persistence
{
    public class CustomerOlderRepository : ICustomerOlderRepository
    {
        private SalesDbContext context;
        public CustomerOlderRepository(SalesDbContext context)
        {
            this.context = context;
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public IEnumerable<Customer> List()
        {
            return context.Customers;
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            return context.Customers.Where(predicate);
        }

        public Customer FindOne(Expression<Func<Customer, bool>> predicate)
        {
            return context.Customers.SingleOrDefault(predicate);
        }

        public void Add(Customer entity)
        {
            context.Add(entity);
        }

        public void AddMultiple(IEnumerable<Customer> entities)
        {
            // TBD
            throw new NotImplementedException();
        }

        public void Remove(Customer entity)
        {
            // TBD
            throw new NotImplementedException();
        }

        public void RemoveMultiple(IEnumerable<Customer> entities)
        {
            // TBD
            throw new NotImplementedException();
        }
        public IEnumerable<Customer> GetCustomersByName(string name)
        {
            return context.Customers.Where(c => c.Name.StartsWith(name));
        }

        public void Save() {
            context.SaveChanges();
        }
    }
}