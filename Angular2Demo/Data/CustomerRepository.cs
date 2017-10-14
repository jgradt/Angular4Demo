using Angular2Demo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2Demo.Data
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        Customer GetById(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly DemoDbContext dbContext;

        public CustomerRepository(DemoDbContext demoDbContext)
        {
            this.dbContext = demoDbContext;
        }

        public List<Customer> GetAll()
        {
            return dbContext.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return dbContext.Customers.Find(id);
        }
    }
}
