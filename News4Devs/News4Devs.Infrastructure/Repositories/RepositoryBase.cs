using Microsoft.EntityFrameworkCore;
using News4Devs.Core.Interfaces.Repositories;
using News4Devs.Infrastructure.AppDbContext;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace News4Devs.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ApplicationDbContext Context;

        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<T> RemoveAsync(Guid id)
        {
            var entityToBeDeleted = await Context.Set<T>().FindAsync(id);
            if (entityToBeDeleted == null)
            {
                return entityToBeDeleted;
            }
            Context.Set<T>().Remove(entityToBeDeleted);

            return entityToBeDeleted;
        }

        public Task<T> UpdateAsync(T entity)
        {
            Context.Set<T>().Update(entity);

            return Task.FromResult(entity);
        }

        public Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            var entities = Context.Set<T>().Where(filter);

            return Task.FromResult(entities.Any());
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            var entities = Context.Set<T>().AsNoTracking();

            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            entities = GetEntitiesWithIncludedProperties(entities, includeProperties);

            return Task.FromResult(entities);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            // Not setting AsNoTracking() crashed the app. 
            // TODO investigate why that happenned.
            IQueryable<T> entities = Context.Set<T>().AsNoTracking(); 

            if (filter != null)
            {
               entities = entities.Where(filter);
            }

            entities = GetEntitiesWithIncludedProperties(entities, includeProperties);

            return await entities.FirstOrDefaultAsync();
        }

        private IQueryable<T> GetEntitiesWithIncludedProperties(IQueryable<T> entities, string includeProperties)
        {
            if (includeProperties != null)
            {
                var propertiesToBeIncluded = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var property in propertiesToBeIncluded)
                {
                    entities = entities.Include(property);
                }
            }

            return entities;
        }
    }
}
