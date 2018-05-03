using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Sales.Models;

namespace Sales.Persistence
{
    public interface ICustomerOlderRepository
    {
        Customer Get(int id);
        IEnumerable<Customer> List(); // GetAll();
        IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate);
        Customer FindOne(Expression<Func<Customer, bool>> predicate);

        void Add(Customer entity);
        void AddMultiple(IEnumerable<Customer> entities); //  AddRange(IEnumerable<Customer> entities);

        void Remove(Customer entity);
        void RemoveMultiple(IEnumerable<Customer> entities); // RemoveRange(IEnumerable<Customer> entities);

        IEnumerable<Customer> GetCustomersByName(string name);

        void Save();
    }
}