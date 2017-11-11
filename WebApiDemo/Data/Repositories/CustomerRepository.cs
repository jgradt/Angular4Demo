using WebApiDemo.Data.Entities;
using WebApiDemo.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiDemo.Data.Dto;
using WebApiDemo.Infrastructure.Errors;

namespace WebApiDemo.Data.Repositories
{
    //TODO: create base class

    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<PagedData<Customer>> GetPagedAsync(int pageIndex, int pageSize);
        Task<Customer> GetByIdAsync(int id);
        Task<Customer> AddAsync(Customer entity);
        Task<bool> UpdateAsync(int id, Customer entity);
        Task<bool> DeleteAsync(int id);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly DemoDbContext dbContext;

        public CustomerRepository(DemoDbContext demoDbContext)
        {
            this.dbContext = demoDbContext;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await dbContext.Customers.ToListAsync();
        }

        public async Task<PagedData<Customer>> GetPagedAsync(int pageIndex, int pageSize)
        {
            var query = dbContext.Customers;

            var paged = query
                .Skip(pageIndex * pageSize).Take(pageSize);

            var totalCount = await query.CountAsync();
            var data = new PagedData<Customer>() //TODO: need constructor
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalCount,
                Items = await paged.ToListAsync()
            };

            return data;
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await dbContext.Customers.FindAsync(id);
        }

        public async Task<Customer> AddAsync(Customer entity)
        {
            entity.Id = dbContext.Customers.Max(o => o.Id) + 1; //TODO: this is a hack for the in-memory database

            dbContext.Customers.Add(entity);
            var result = await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, Customer entity)
        {
            var existing = await dbContext.Customers.FindAsync(id);
            if(existing == null)
            {
                throw new CrudDataException(CrudStatusCode.UpdateItemNotFound);
            }

            existing.FirstName = entity.FirstName;
            existing.LastName = entity.LastName;

            var result = await dbContext.SaveChangesAsync();

            return (result > 0);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await dbContext.Customers.FindAsync(id);
            if (existing == null)
            {
                throw new CrudDataException(CrudStatusCode.DeleteItemNotFound);
            }

            dbContext.Customers.Remove(existing);
            var result = await dbContext.SaveChangesAsync();

            return (result > 0);
        }

    }
}
