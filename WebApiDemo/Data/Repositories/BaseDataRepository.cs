using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiDemo.Data.Dto;
using WebApiDemo.Infrastructure.Errors;
using System;
using System.Linq.Expressions;

namespace WebApiDemo.Data.Repositories
{
    public interface IBaseDataRepository<TEntity> 
        where TEntity: class, IDataEntity
    {
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
        PagedData<TEntity> GetPaged(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
        Task<PagedData<TEntity>> GetPagedAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null);
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        bool GetExists(Expression<Func<TEntity, bool>> filter = null);
        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(int id, TEntity entity);
        Task DeleteAsync(int id);
        int Save();
        Task<int> SaveAsync();
    }

    /* Some of this code is based on or copied from a generic repository that can be found here:  https://cpratt.co/truly-generic-repository/ */
    public class BaseDataRepository<TEntity> : IBaseDataRepository<TEntity> 
        where TEntity: class, IDataEntity
    {
        private readonly DemoDbContext dbContext;

        public BaseDataRepository(DemoDbContext demoDbContext)
        {
            this.dbContext = demoDbContext;
        }

        protected virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            IQueryable<TEntity> query = dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties ?? new string[] { })
            {
                if(!string.IsNullOrWhiteSpace(includeProperty.Trim()))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetQueryable(filter).CountAsync();
        }

        public bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public async Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetQueryable(filter).AnyAsync();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null)
        {
            return GetQueryable(filter: filter, orderBy: orderBy, includeProperties: includeProperties, skip: null, take: null).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null, string[] includeProperties = null)
        {
            return await GetQueryable(filter: filter, orderBy: orderBy, includeProperties: includeProperties, skip: null, take: null).ToListAsync();
        }

        public PagedData<TEntity> GetPaged(int pageIndex, int pageSize,
            Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null)
        {
            var query = GetQueryable(filter: filter, orderBy: orderBy, includeProperties: includeProperties, skip: (pageIndex * pageSize), take: pageSize);

            var totalCount = GetCount(filter);
            var data = new PagedData<TEntity>() //TODO: need constructor
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalCount,
                Items = query.ToList()
            };

            return data;
        }

        public async Task<PagedData<TEntity>> GetPagedAsync(int pageIndex, int pageSize, 
            Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null)
        {
            var query = GetQueryable(filter: filter, orderBy: orderBy, includeProperties: includeProperties, skip: (pageIndex * pageSize), take: pageSize);

            var totalCount = await GetCountAsync(filter);
            var data = new PagedData<TEntity>() //TODO: need constructor
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItems = totalCount,
                Items = await query.ToListAsync()
            };

            return data;
        }

        public TEntity GetById(int id)
        {
            return dbContext.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Id = dbContext.Set<TEntity>().Max(o => o.Id) + 1; //TODO: this is a hack for the in-memory database
            entity.CreatedDate = DateTime.Now;
            entity.LastUpdatedDate = DateTime.Now;

            dbContext.Set<TEntity>().Add(entity);

            return entity;
        }

        public virtual async Task UpdateAsync(int id, TEntity entity)
        {
            var existing = await dbContext.Set<TEntity>().FindAsync(id);
            if (existing == null)
            {
                throw new CrudDataException(CrudStatusCode.UpdateItemNotFound);
            }

            SetDataForUpdate(entity, existing);
            entity.LastUpdatedDate = DateTime.Now;
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await dbContext.Set<TEntity>().FindAsync(id);
            if (existing == null)
            {
                throw new CrudDataException(CrudStatusCode.DeleteItemNotFound);
            }

            dbContext.Set<TEntity>().Remove(existing);
        }

        public virtual int Save()
        {
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
           return await dbContext.SaveChangesAsync();
        }


        public virtual void SetDataForUpdate(TEntity sourceEntity, TEntity destinationEntity)
        {
            //TODO: figure out how to set all properties by default
        }
    }
}
