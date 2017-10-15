using Angular2Demo.Data.Entities;
using Angular2Demo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2Demo.Data
{
    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        PagedData<Customer> GetPaged(int pageIndex, int pageSize);
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

        public PagedData<Customer> GetPaged(int pageIndex, int pageSize)
        {
            var query = dbContext.Customers;

            var paged = query
                .Skip(pageIndex * pageSize).Take(pageSize);

            var totalCount = query.Count();
            var data = new PagedData<Customer>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = (totalCount / pageSize) + ((totalCount % pageSize > 0) ? 1 : 0),
                TotalItems = totalCount,
                Data = paged.ToList()
            };

            return data;
        }

        public Customer GetById(int id)
        {
            return dbContext.Customers.Find(id);
        }
    }
}
