using WebApiDemo.Data.Entities;
using WebApiDemo.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiDemo.Data.Dto;
using WebApiDemo.Infrastructure.Errors;
using System;

namespace WebApiDemo.Data.Repositories
{
    public interface IBaseDataRepository<TEntity> 
        where TEntity: class, IDataEntity
    {
        Task<List<TEntity>> GetAllAsync();
        Task<PagedData<TEntity>> GetPagedAsync(int pageIndex, int pageSize);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(int id, TEntity entity);
        Task<bool> DeleteAsync(int id);
    }

    public class BaseDataRepository<TEntity> : IBaseDataRepository<TEntity> 
        where TEntity: class, IDataEntity
    {
        private readonly DemoDbContext dbContext;

        public BaseDataRepository(DemoDbContext demoDbContext)
        {
            this.dbContext = demoDbContext;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<PagedData<TEntity>> GetPagedAsync(int pageIndex, int pageSize)
        {
            var query = dbContext.Set<TEntity>();

            var paged = query
                .Skip(pageIndex * pageSize).Take(pageSize);

            var totalCount = await query.CountAsync();
            var data = new PagedData<TEntity>() //TODO: need constructor
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalCount,
                Items = await paged.ToListAsync()
            };

            return data;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Id = dbContext.Set<TEntity>().Max(o => o.Id) + 1; //TODO: this is a hack for the in-memory database
            entity.CreatedDate = DateTime.Now;
            entity.LastUpdatedDate = DateTime.Now;

            dbContext.Set<TEntity>().Add(entity);
            var result = await dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<bool> UpdateAsync(int id, TEntity entity)
        {
            var existing = await dbContext.Set<TEntity>().FindAsync(id);
            if (existing == null)
            {
                throw new CrudDataException(CrudStatusCode.UpdateItemNotFound);
            }

            SetDataForUpdate(entity, existing);
            entity.LastUpdatedDate = DateTime.Now;

            var result = await dbContext.SaveChangesAsync();

            return (result > 0);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await dbContext.Set<TEntity>().FindAsync(id);
            if (existing == null)
            {
                throw new CrudDataException(CrudStatusCode.DeleteItemNotFound);
            }

            dbContext.Set<TEntity>().Remove(existing);
            var result = await dbContext.SaveChangesAsync();

            return (result > 0);
        }


        public virtual void SetDataForUpdate(TEntity sourceEntity, TEntity destinationEntity)
        {
        }
    }
}
