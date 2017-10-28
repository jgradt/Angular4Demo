using WebApiDemo.Data.Entities;
using WebApiDemo.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace WebApiDemo.Data
{
    //TODO: create base class

    public interface ICustomerRepository
    {
        List<Customer> GetAll();
        PagedData<Customer> GetPaged(int pageIndex, int pageSize);
        Customer GetById(int id);
        Customer Add(Customer entity);
        bool Update(int id, Customer entity);
        bool Delete(int id);
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
            var data = new PagedData<Customer>() //TODO: need constructor
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

        public Customer Add(Customer entity)
        {
            entity.Id = dbContext.Customers.Max(o => o.Id) + 1; //TODO: this is a hack for the in-memory database

            dbContext.Customers.Add(entity);
            var result = dbContext.SaveChanges();

            return entity;
        }

        public bool Update(int id, Customer entity)
        {
            var existing = dbContext.Customers.Find(id);
            if(existing == null)
            {
                return false;
            }

            existing.FirstName = entity.FirstName;
            existing.LastName = entity.LastName;

            var result = dbContext.SaveChanges();

            return (result > 0);
        }

        public bool Delete(int id)
        {
            var existing = dbContext.Customers.Find(id);
            dbContext.Customers.Remove(existing);
            var result = dbContext.SaveChanges();

            return (result > 0);
        }
    }
}
