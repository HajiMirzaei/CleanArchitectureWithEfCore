using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleProject.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Data
{
    public abstract class RepositoryBase<T> : IAsyncGenericRepository<T> where T : EntityBase
    {
        private readonly DbSet<T> dbSet;
        protected SampleProjectDbContext _db { get; private set; }

        protected RepositoryBase(SampleProjectDbContext db)
        {
            _db = db;
            this.dbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) dbSet.Remove(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}